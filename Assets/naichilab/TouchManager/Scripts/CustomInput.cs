using UnityEngine;
using System.Collections;

namespace naichilab.InputEvents
{
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

				public bool IsFlicking{ get; set; }

				/// <summary>
				/// 速度、加速度を計算するための平準化時間（FlickDetector.LevelingFrameCountにより決定）
				/// </summary>
				public float LevelingTime{ get; set; }

				/// <summary>
				/// このフレームの直近LevelingTimeでの移動距離
				/// </summary>
				public Vector3 MovedDistance{ get; set; }

				/// <summary>
				/// このフレームの直近LevelingTimeでの速度ベクトル
				/// </summary>
				public Vector3 SpeedVector {
						get {
								if (this.LevelingTime < 0.0001f)
										return Vector3.zero;
								return this.MovedDistance / this.LevelingTime; 
						}
				}

				/// <summary>
				/// LevelingTime前のフレームの速度ベクトル
				/// </summary>
				public Vector3 LevelingOriginSpeedVector{ get; set; }

				/// <summary>
				/// このフレームの直近LevelingTimeでの加速度ベクトル
				/// </summary>
				public Vector3 AccelerationVector {
						get {
								if (this.LevelingTime < 0.0001f)
										return Vector3.zero;
								return (this.SpeedVector - this.LevelingOriginSpeedVector) / this.LevelingTime;
						}
				}
		}
}
