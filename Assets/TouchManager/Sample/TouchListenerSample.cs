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
				TouchManager.Instance.Swipe += OnSwipe;
				TouchManager.Instance.TouchStart += OnTouchStart;
				TouchManager.Instance.TouchEnd += OnTouchEnd;
				TouchManager.Instance.Flick += OnFlick;
		}

		void OnDisable ()
		{
				TouchManager.Instance.Swipe -= OnSwipe;
				TouchManager.Instance.TouchStart -= OnTouchStart;
				TouchManager.Instance.TouchEnd -= OnTouchEnd;
				TouchManager.Instance.Flick -= OnFlick;
		}

		void OnTouchStart (object sender, TouchEventArgs e)
		{
				string text = string.Format ("OnTouchStart X={0} Y={1}", e.ScreenPosition.x, e.ScreenPosition.y);
				this.addLog (text);
		}

		void OnTouchEnd (object sender, TouchEventArgs e)
		{
				string text = string.Format ("OnTouchEnd X={0} Y={1}", e.ScreenPosition.x, e.ScreenPosition.y);
				this.addLog (text);
		}

		void OnSwipe (object sender, SwipeEventArgs e)
		{
				string text = string.Format ("OnSwipe X={0}[{1}] Y={2}[{3}]", new object[] {
						e.ScreenPosition.x,
						e.MovedScreenDistance.x,
						e.ScreenPosition.y,
						e.MovedScreenDistance.y
				});
				this.addLog (text);
		}

		void OnFlick (object sender, FlickEventArgs e)
		{
				string text = string.Format ("OnFlick From[{0},{1}] To[{2}{3}] Time[{4}]", new object[] {
						e.FlickStartScreenPosition.x,
						e.FlickStartScreenPosition.y,
						e.FlickEndScreenPosition.x,
						e.FlickEndScreenPosition.y,
						e.ElapsedTime
				});
				this.addLog (text);
		}

		void addLog (string log)
		{
				this.logs.Add (log);
				if (logs.Count > MAX_LOGS) {
						this.logs.RemoveAt (0);
				}
		}

		void OnGUI ()
		{
				GUI.Label (new Rect (20, 20, 300, 20), "タッチしてね。");
				int i = 1;
				foreach (var log in this.logs) {
						GUI.Label (new Rect (20, 20 + 12 * i++, 800, 20), log);
				}
		}
}
