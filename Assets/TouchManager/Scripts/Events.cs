using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public delegate void TouchEventHandler (object sender,TouchEventArgs e);
public class TouchEventArgs:EventArgs
{
		public   Vector3 ScreenPosition;

		public TouchEventArgs (Vector3 screenPosition)
		{
				this.ScreenPosition = screenPosition;
		}
}
public delegate void SwipeEventHandler (object sender,SwipeEventArgs e);
public class SwipeEventArgs:EventArgs
{
		public   Vector2 ScreenPosition;
		public    Vector2 MovedScreenDistance;

		public SwipeEventArgs (Vector3 screenPosition, Vector3 movedScreenDistance)
		{
				this.ScreenPosition = screenPosition;
				this.MovedScreenDistance = movedScreenDistance;
		}
}
public delegate void FlickEventHandler (object sender,FlickEventArgs e);
public class FlickEventArgs:EventArgs
{
		public Vector3 FlickStartScreenPosition;
		public Vector3 FlickEndScreenPosition;
		public float ElapsedTime;

		public FlickEventArgs (Vector3 flickStartScreenPosition, Vector3 flickEndScreenPosition, float elapsedTime)
		{
				this.FlickStartScreenPosition = flickStartScreenPosition;
				this.FlickEndScreenPosition = flickEndScreenPosition;
				this.ElapsedTime = elapsedTime;
		}
}

