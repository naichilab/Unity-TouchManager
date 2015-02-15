using UnityEngine;
using System.Collections;

public class CustomInput
{
    public Vector3 ScreenPosition { get; set; }

    public Vector3 DeltaPosition { get; set; }

    public float DeltaTime { get; set; }

    public float Time{ get; set; }

    public bool IsDown { get; set; }

    public bool IsUp { get; set; }

    public bool IsDrag { get; set; }

    public int TouchId{ get; set; }
}
