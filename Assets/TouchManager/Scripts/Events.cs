using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public delegate void TouchEventHandler (object sender,TouchEventArgs e);
public class TouchEventArgs:EventArgs
{
    public   Vector2 ScreenPosition;

    public TouchEventArgs (float screenPositionX, float screenPositionY)
    {
        this.ScreenPosition = new Vector2 (screenPositionX, screenPositionY);
    }
}
public delegate void SwipeEventHandler (object sender,SwipeEventArgs e);
public class SwipeEventArgs:EventArgs
{
    public    Vector2 MovedDistance;

    public SwipeEventArgs (float movedX, float movedY)
    {
        this.MovedDistance = new Vector2 (movedX, movedY);
    }
}
public delegate void FlickEventHandler (object sender,FlickEventArgs e);
public class FlickEventArgs:EventArgs
{
    public Vector2 Distance;
    public float Time;

    public FlickEventArgs (float distanceX, float distanceY, float time)
    {
        this.Distance = new Vector2 (distanceX, distanceY);
        this.Time = time;
    }
}

