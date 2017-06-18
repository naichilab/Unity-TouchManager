using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using naichilab.InputEvents;

public class TouchListenerSample : MonoBehaviour
{
		private List<string> logs = new List<string> ();
		const int MAX_LOGS = 30;
		public Circle circle;

		void OnEnable ()
		{
				TouchManager.Instance.Drag += OnSwipe;
				TouchManager.Instance.TouchStart += OnTouchStart;
				TouchManager.Instance.TouchEnd += OnTouchEnd;
				TouchManager.Instance.FlickStart += OnFlickStart;
				TouchManager.Instance.FlickComplete += OnFlickComplete;
		}

		void OnDisable ()
		{
				if (TouchManager.Instance != null) {
						TouchManager.Instance.Drag -= OnSwipe;
						TouchManager.Instance.TouchStart -= OnTouchStart;
						TouchManager.Instance.TouchEnd -= OnTouchEnd;
						TouchManager.Instance.FlickStart -= OnFlickStart;
						TouchManager.Instance.FlickComplete -= OnFlickComplete;
				}
		}

		void OnTouchStart (object sender, CustomInputEventArgs e)
		{
				string text = string.Format ("OnTouchStart X={0} Y={1}", e.Input.ScreenPosition.x, e.Input.ScreenPosition.y);
				Debug.Log (text);

				this.circle.Speed = Vector3.zero;
				this.circle.SetPosition (e.Input.ScreenPosition);
		}

		void OnTouchEnd (object sender, CustomInputEventArgs e)
		{
				string text = string.Format ("OnTouchEnd X={0} Y={1}", e.Input.ScreenPosition.x, e.Input.ScreenPosition.y);
				Debug.Log (text);

				this.circle.Speed = Vector3.zero;
		}

		void OnSwipe (object sender, CustomInputEventArgs e)
		{
				string text = string.Format ("OnSwipe Pos[{0},{1}] Move[{2},{3}]", new object[] {
						e.Input.ScreenPosition.x.ToString ("0"),
						e.Input.ScreenPosition.y.ToString ("0"),
						e.Input.DeltaPosition.x.ToString ("0"),
						e.Input.DeltaPosition.y.ToString ("0")
				});

				this.circle.MovePosition (e.Input.DeltaPosition);
		}

		void OnFlickStart (object sender, FlickEventArgs e)
		{
				string text = string.Format ("OnFlickStart [{0}] Speed[{1}] Accel[{2}] ElapseTime[{3}]", new object[] {
						e.Direction.ToString (),
						e.Speed.ToString ("0.000"),
						e.Acceleration.ToString ("0.000"),
						e.ElapsedTime.ToString ("0.000"),
				});
				Debug.Log (text);
		}

		void OnFlickComplete (object sender, FlickEventArgs e)
		{
				string text = string.Format ("OnFlickComplete [{0}] Speed[{1}] Accel[{2}] ElapseTime[{3}]", new object[] {
						e.Direction.ToString (),
						e.Speed.ToString ("0.000"),
						e.Acceleration.ToString ("0.000"),
						e.ElapsedTime.ToString ("0.000")
				});
				Debug.Log (text);
				text = string.Format ("[{0}]->[{1}]", new object[]{ e.StartInput.ScreenPosition, e.EndInput.ScreenPosition });
				Debug.Log (text);


				this.circle.Speed = (e.MovedDistance / e.ElapsedTime);

		}
}
