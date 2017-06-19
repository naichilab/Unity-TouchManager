using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace naichilab.InputEvents
{
		[RequireComponent (typeof(TouchDetector))]
		[RequireComponent (typeof(DragDetector))]
		[RequireComponent (typeof(FlickDetector))]
		public class TouchManager :MonoBehaviour
		{
				public event EventHandler<CustomInputEventArgs> TouchStart;
				public event EventHandler<CustomInputEventArgs> TouchEnd;
				public event EventHandler<CustomInputEventArgs> Drag;
				public event EventHandler<FlickEventArgs> FlickComplete;
				public event EventHandler<FlickEventArgs> FlickStart;

				public void OnTouchStart (CustomInput input)
				{
						if (this.TouchStart != null)
								this.TouchStart (this.gameObject, new CustomInputEventArgs (input));
				}

				public void OnTouchEnd (CustomInput input)
				{
						if (this.TouchEnd != null)
								this.TouchEnd (this.gameObject, new CustomInputEventArgs (input));
				}

				public void OnDrag (CustomInput input)
				{
						if (this.Drag != null)
								this.Drag (this.gameObject, new CustomInputEventArgs (input));
				}

				public void OnFlickComplete (FlickEventArgs e)
				{
						if (this.FlickComplete != null)
								this.FlickComplete (this.gameObject, e);
				}

				public void OnFlickStart (FlickEventArgs e)
				{
						if (this.FlickStart != null)
								this.FlickStart (this.gameObject, e);
				}

				#region Singleton

				private static TouchManager instance;

				public static TouchManager Instance {
						get {
								if (instance == null) {
										instance = (TouchManager)FindObjectOfType (typeof(TouchManager));

										if (instance == null) {
												Debug.LogWarning (typeof(TouchManager) + "is nothing");
										}
								}
								return instance;
						}
				}

				#endregion Singleton

				private List<IGestureDetector> Detectors = new List<IGestureDetector> ();
				private CustomInput lastInput = null;
				public bool DebugMode = false;

				private static bool IsTouchPlatform { 
						get {
								switch (Application.platform) {
								case RuntimePlatform.Android:
								case RuntimePlatform.IPhonePlayer:
										return true;
								default:
										return false;
								}
						}
				}

				private Touch? CurrentTouch {
						get {
								if (Input.touchCount <= 0) {
										return null;
								}
								if (this.lastInput == null) {
										foreach (var touch in Input.touches) {
												if (touch.phase == TouchPhase.Began) {
														return touch;
												}
										}
								} else {
										foreach (var touch in Input.touches) {
												if (touch.fingerId == this.lastInput.TouchId) {
														return touch;
												}
										}
								}
								return null;
						}
				}

				private CustomInput InputOfTouch {
						get {
								CustomInput input = new CustomInput ();
								input.Time = Time.time;
		            
								Touch? touch = this.CurrentTouch;

								if (!touch.HasValue) {
										return input;
								}
								input.ScreenPosition = touch.Value.position;
								input.DeltaPosition = touch.Value.deltaPosition;
								input.TouchId = touch.Value.fingerId;
								if (this.lastInput != null) {
										input.DeltaTime = Time.time - this.lastInput.Time;
								}
								switch (touch.Value.phase) {
								case TouchPhase.Began:
										input.IsDown = true;
										break;
								case TouchPhase.Moved:
								case TouchPhase.Stationary:
										input.IsDrag = true;
										break;
								case TouchPhase.Ended:
								case TouchPhase.Canceled:
										input.IsUp = true;
										break;
								}
								return input;
						}
				}

				private  CustomInput InputOfMouse {
						get {
								CustomInput input = new CustomInput ();            
								input.ScreenPosition = Input.mousePosition;
								input.Time = Time.time;
								if (this.lastInput != null) {
										input.DeltaPosition = Input.mousePosition - lastInput.ScreenPosition;
										input.DeltaTime = Time.time - this.lastInput.Time;
								}
								if (Input.GetMouseButtonDown (0)) {
										input.IsDown = true;
										input.DeltaPosition = new Vector3 ();
								} else if (Input.GetMouseButtonUp (0)) {
										input.IsUp = true;
								} else if (Input.GetMouseButton (0)) {
										input.IsDrag = true;
								}
								return input;
						}
				}

				private void Awake ()
				{
						if (this != Instance) {
								Destroy (this);
								return;
						}
						DontDestroyOnLoad (this.gameObject);
				}

				private void Start ()
				{
						this.Detectors.Add (this.GetComponent<TouchDetector> ());
						this.Detectors.Add (this.GetComponent<DragDetector> ());
						this.Detectors.Add (this.GetComponent<FlickDetector> ());
				}

				private void Update ()
				{
						CustomInput currentInput = IsTouchPlatform ? InputOfTouch : InputOfMouse;

						foreach (var detector in this.Detectors) {
								detector.Enqueue (currentInput);
						}

						this.lastInput = currentInput;
				}

				public void OnGUI ()
				{
						if (!this.DebugMode)
								return;
						CustomInput input = this.lastInput;
						if (input == null) {
								input = new CustomInput ();
						}

						int i = 0;
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("ScreenPosition : X:{0} Y:{1}", input.ScreenPosition.x.ToString ("0"), input.ScreenPosition.y.ToString ("0")));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("DeltaPosition : X:{0} Y:{1}", input.DeltaPosition.x.ToString ("0"), input.DeltaPosition.y.ToString ("0")));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("DeltaTime : {0}sec", input.DeltaTime.ToString ("0.000")));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("Time : {0}sec", input.Time.ToString ("0.000")));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("IsDown : {0}", input.IsDown.ToString ()));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("IsUp : {0}", input.IsUp.ToString ()));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("IsDrag : {0}", input.IsDrag.ToString ()));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("IsFlicking : {0}", input.IsFlicking.ToString ()));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("TouchID : {0}", input.TouchId));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("", ""));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("LevelingTime : {0}sec", input.LevelingTime.ToString ("0.000")));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("MovedDistance : {0}", input.MovedDistance.magnitude));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("Speed : {0} [X:{1},Y:{2}]", input.SpeedVector.magnitude, input.SpeedVector.x.ToString ("0"), input.SpeedVector.y.ToString ("0")));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("LevelingOriginSpeed : {0} [X:{1},Y:{2}]", input.LevelingOriginSpeedVector.magnitude, input.LevelingOriginSpeedVector.x.ToString ("0"), input.LevelingOriginSpeedVector.y.ToString ("0")));
						GUI.Label (new Rect (20, 20 + i++ * 20, 400, 20), string.Format ("Acceleration : {0} [X:{1},Y:{2}]", input.AccelerationVector.magnitude, input.AccelerationVector.x.ToString ("0"), input.AccelerationVector.y.ToString ("0")));


				}
		}
}
