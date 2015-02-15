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
        string text = string.Format ("OnSwipe X={0} Y={1}", e.MovedDistance.x, e.MovedDistance.y);
        this.addLog (text);
    }

    void OnFlick (object sender, FlickEventArgs e)
    {
        string text = string.Format ("OnFlick X={0} Y={1} Time={2}", e.Distance.x, e.Distance.y, e.Time);
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
            GUI.Label (new Rect (20, 20 + 12 * i++, 300, 20), log);
        }
    }
}
