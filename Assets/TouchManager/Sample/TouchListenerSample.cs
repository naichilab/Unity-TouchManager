using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TouchListenerSample : MonoBehaviour
{
		private List<string> logs = new List<string> ();
		const int MAX_LOGS = 30;

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
		}

		void OnTouchEnd (object sender, CustomInputEventArgs e)
		{
				string text = string.Format ("OnTouchEnd X={0} Y={1}", e.Input.ScreenPosition.x, e.Input.ScreenPosition.y);
				Debug.Log (text);
		}

		void OnSwipe (object sender, CustomInputEventArgs e)
		{
				string text = string.Format ("OnSwipe X={0}[{1}] Y={2}[{3}]", new object[] {
						e.Input.ScreenPosition.x,
						e.Input.DeltaPosition.x,
						e.Input.ScreenPosition.y,
						e.Input.DeltaPosition.y
				});
		}

		void OnFlickStart (object sender, FlickEventArgs e)
		{
				string text = string.Format ("OnFlickStart Speed[{0}] Accel[{3}] Direction[{1}] Time[{2}]", new object[] {
						e.Speed.ToString (),
						e.Direction.ToString (),
						e.ElapsedTime,
						(e.EndInput.SpeedVector - e.StartInput.SpeedVector).magnitude
				});
				Debug.Log (text);
		}

		void OnFlickComplete (object sender, FlickEventArgs e)
		{
				string text = string.Format ("OnFlickComplete Speed[{0}] Direction[{1}] Time[{2}]", new object[] {
						e.Speed.ToString (),
						e.Direction.ToString (),
						e.ElapsedTime
				});
				Debug.Log (text);
		}
}
