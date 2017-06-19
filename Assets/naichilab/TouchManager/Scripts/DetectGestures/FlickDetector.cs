using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace naichilab.InputEvents
{
		public class FlickDetector : MonoBehaviour,IGestureDetector
		{
				/// <summary>
				/// フリックの検出を行うか
				/// </summary>
				public bool Enabled = true;
				/// <summary>
				/// フリック速度、加速度を計算するためのフレーム数
				/// 例）１を指定した場合、直前フレームと現フレームの１フレーム時間で速度、加速度を計算する。
				/// </summary>
				[Range (1f, 60f)]
				public int LevelingFrameCount = 5;
				[Range (1f, 100000f)]
				public float DetectAcceleration = 1000f;
				[Range (1f, 10000f)]
				public float DefeatSpeed = 100f;
				[Range (0f, 1000f)]
				public float MinFlickDistance = 0f;
				/// <summary>
				/// 指を離さなくても連続で検知するか
				/// </summary>
				/// <value><c>true</c> if continuous detect; otherwise, <c>false</c>.</value>
				public bool ContinuousDetect = true;
				/// <summary>
				/// 過去LevelingFrameCount分のInputを保持
				/// </summary>
				private  List<CustomInput> pastInputs = new List<CustomInput> ();
				/// <summary>
				/// フリック開始時のInput
				/// </summary>
				public CustomInput FlickStartInput = null;
				private bool IsDetected = false;

				public void Enqueue (CustomInput currentInput)
				{
						if (!(currentInput.IsDown || currentInput.IsDrag || currentInput.IsUp))
								return;

						this.pastInputs.Add (currentInput);

						if (this.pastInputs.Count == 1) {
								//First Input
								currentInput.MovedDistance = Vector3.zero;
								currentInput.LevelingTime = 0;
								currentInput.LevelingOriginSpeedVector = Vector3.zero;
						} else {
								//currentInputからLevelingFrame数だけ古いフレームのInput
								CustomInput levelingOriginInput = this.pastInputs [0];
								currentInput.MovedDistance = currentInput.ScreenPosition - levelingOriginInput.ScreenPosition;
								currentInput.LevelingTime = currentInput.Time - levelingOriginInput.Time;// this.LevelingFrameCount;
								currentInput.LevelingOriginSpeedVector = levelingOriginInput.SpeedVector;

								//フリック開始＆継続判定
								var lastInput = this.pastInputs [this.pastInputs.Count - 2];
								if (lastInput.IsFlicking) {
										//継続判定
										if (currentInput.SpeedVector.magnitude > this.DefeatSpeed) {
												currentInput.IsFlicking = true;
										} else {
												//フリック中止
												this.FlickStartInput = null;

												currentInput.IsFlicking = false;
												this.FlickStartInput = null;
										}
								} else {
										//フリック開始判定
										if (currentInput.AccelerationVector.magnitude > this.DetectAcceleration) {
												if (currentInput.SpeedVector.magnitude > 0.0001f) {
														if (!this.ContinuousDetect && this.IsDetected) {
																//指を離すまで再検知しない
														} else {
																currentInput.IsFlicking = true;
																this.FlickStartInput = currentInput;
																this.IsDetected = true;
																//フリック開始イベント
																TouchManager.Instance.OnFlickStart (new FlickEventArgs (levelingOriginInput, currentInput));
														}
												}
										}
								}

								//フリック完了判定
								if (currentInput.IsFlicking && currentInput.IsUp) {

										Vector3 flickDistance = currentInput.ScreenPosition - this.FlickStartInput.ScreenPosition;
										if (flickDistance.magnitude > this.MinFlickDistance) {

												//フリック成立
												TouchManager.Instance.OnFlickComplete (new FlickEventArgs (this.pastInputs [this.pastInputs.Count - 2], currentInput));
												//TouchManager.Instance.OnFlickComplete (new FlickEventArgs (this.FlickStartInput, currentInput));

												currentInput.IsFlicking = false;
												this.FlickStartInput = null;

										}
								}

								//指が離れた
								if (currentInput.IsUp) {
										this.IsDetected = false;
										this.pastInputs.Clear();
								}
						}

						while (this.pastInputs.Count > this.LevelingFrameCount) {
								this.pastInputs.RemoveAt (0);
						}

				}
		}
}
