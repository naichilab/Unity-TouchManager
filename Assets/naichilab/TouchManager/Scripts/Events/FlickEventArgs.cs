using UnityEngine;
using System.Collections;
using System;

namespace naichilab.InputEvents
{
		public class FlickEventArgs : EventArgs
		{
				public enum Direction4
				{
						None = -1,
						Up = 0,
						Left = 1,
						Down = 2,
						Right = 3
				}

				public CustomInput StartInput{ get; private set; }

				public CustomInput EndInput{ get; private set; }

				public Vector3 MovedDistance{ get { return EndInput.ScreenPosition - StartInput.ScreenPosition; } }

				public float ElapsedTime{ get { return EndInput.Time - StartInput.Time; } }

				public float Speed {
						get {
								if (this.ElapsedTime < 0.0001f)
										return 0;
								return this.MovedDistance.magnitude / this.ElapsedTime;
						}
				}

				public Direction4 Direction{ get { return VectorDirection4 (this.MovedDistance.x, this.MovedDistance.y); } }

				public float Acceleration {
						get {
								if (this.ElapsedTime < 0.0001f)
										return 0;
								return (this.EndInput.AccelerationVector - this.StartInput.AccelerationVector).magnitude / this.ElapsedTime;
						}
				}

				public FlickEventArgs (CustomInput startInput, CustomInput endInput)
				{
						this.StartInput = startInput;
						this.EndInput = endInput;
				}

				private static Direction4 VectorDirection4 (float x, float y)
				{
						if (y == 0 && x == 0)
								return Direction4.None;

						if (y >= Mathf.Abs (x))
								return Direction4.Up;
						if (y <= -Mathf.Abs (x))
								return Direction4.Down;

						if (x < -Mathf.Abs (y))
								return Direction4.Left;
						if (x > Mathf.Abs (y))
								return Direction4.Right;

						return Direction4.None;
				}
		}
}
