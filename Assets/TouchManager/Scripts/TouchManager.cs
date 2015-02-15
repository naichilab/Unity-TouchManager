using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchManager :MonoBehaviour
{
		/// <summary>
		/// タッチ開始イベント
		/// </summary>
		public event TouchEventHandler TouchStart;
		/// <summary>
		/// タッチ終了イベント
		/// </summary>
		public event TouchEventHandler TouchEnd;
		/// <summary>
		/// スワイプイベント
		/// </summary>
		public event SwipeEventHandler Swipe;
		/// <summary>
		/// フリックイベント
		/// </summary>
		public event FlickEventHandler Flick;

		/// <summary>
		/// TouchStartイベントを発生させます。
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void OnTouchStart (Vector3 screenPosition)
		{
				if (this.TouchStart != null)
						this.TouchStart (this, new TouchEventArgs (screenPosition));
		}

		/// <summary>
		/// TouchEndイベントを発生させます。
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void OnTouchEnd (Vector3 screenPosition)
		{
				if (this.TouchEnd != null)
						this.TouchEnd (this, new TouchEventArgs (screenPosition));
		}

		/// <summary>
		/// Swipeイベントを発生させます。
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void OnSwipe (Vector3 screenPosition, Vector3 movedScreenDistance)
		{
				if (this.Swipe != null)
						this.Swipe (this, new SwipeEventArgs (screenPosition, movedScreenDistance));
		}

		/// <summary>
		/// Flickイベントを発生させます。
		/// </summary>
		private void OnFlick (Vector3 flickStartScreenPosition, Vector3 flickEndScreenPosition, float elapsedTime)
		{
				if (this.Flick != null)
						this.Flick (this, new FlickEventArgs (flickStartScreenPosition, flickEndScreenPosition, elapsedTime));
		}

		#region Singleton

		private static TouchManager instance;

		public static TouchManager Instance {
				get {
						if (instance == null) {
								instance = (TouchManager)FindObjectOfType (typeof(TouchManager));
								instance.MinFlickDistance = 50.0f;

								if (instance == null) {
										Debug.LogError (typeof(TouchManager) + "is nothing");
								}
						}
						return instance;
				}
		}

		#endregion Singleton

		private const int TRACE_QUEUE_COUNT = 20;

		public float MinFlickDistance{ get; set; }

		private  List<CustomInput> inputList = new List<CustomInput> ();

		private CustomInput firstInput {
				get {
						if (this.inputList.Count == 0)
								return null;
						return this.inputList [0];
				}
		}

		private CustomInput lastInput { 
				get {
						if (this.inputList.Count == 0)
								return null;
						return this.inputList [this.inputList.Count - 1];
				}
		}

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

		public void Awake ()
		{
				if (this != Instance) {
						Destroy (this);
						return;
				}
				DontDestroyOnLoad (this.gameObject);
		}

		public void Update ()
		{
				CustomInput currentInput = IsTouchPlatform ? InputOfTouch : InputOfMouse;
        
				this.inputList.Add (currentInput);
				if (this.inputList.Count > TRACE_QUEUE_COUNT) { 
						this.inputList.RemoveAt (0);
				}
        
				if (currentInput.IsDown) {
						this.OnTouchStart (currentInput.ScreenPosition);
				}
				if (currentInput.IsDrag) {
						this.OnSwipe (currentInput.ScreenPosition, currentInput.DeltaPosition);
				}
				if (currentInput.IsUp) {
						this.OnTouchEnd (currentInput.ScreenPosition);

						// Flick判定
						var v = this.lastInput.ScreenPosition - this.firstInput.ScreenPosition;
						if (Mathf.Pow (MinFlickDistance, 2) < Mathf.Pow (v.x, 2) + Mathf.Pow (v.y, 2)) {
								float t = 0f;
								foreach (var input in inputList) {
										t += input.DeltaTime;
								}
								this.OnFlick (this.firstInput.ScreenPosition, this.lastInput.ScreenPosition, t);
						} else {
								Debug.Log ("距離足りない" + v.ToString ());
						}

						this.inputList.Clear ();
				}
		}
}


    