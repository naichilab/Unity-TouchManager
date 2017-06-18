Unity-TouchManager
====

マウス/タッチ操作による一般的なジェスチャーをシンプルなコードで利用可能にするアセット（になる予定）

* WebPlayerでのマウス操作およびiPhone,Androidでのタッチ操作を等しく扱えます。
* タッチ操作はシングルタッチのみ対応（必要になったらマルチタッチ対応します）
* 扱える操作
    * タッチ開始（マウスクリックを開始）
    * タッチ終了（マウスクリックを離す）
    * ドラッグ（スワイプ）
    * フリック開始（マウスドラッグ開始）
    * フリック（マウスドラッグしながら離す）

## デモ

## インストール
1. TouchManager.unitypackageをインポート

## 使用方法
1. TouchManager.prefabをシーンに配置
2. タッチイベントを受け取りたいスクリプト上で下記を記述

下記ソースは TouchListener.prefab にサンプルとして書かれています。

### モジュールの読み込み
```csharp
using naichilab
```

### タッチ開始を取得
```csharp
void OnEnable ()
{
		TouchManager.Instance.TouchStart += OnTouchStart;
}
void OnDisable ()
{
		TouchManager.Instance.TouchStart -= OnTouchStart;
}
void OnTouchStart (object sender, CustomInputEventArgs e)
{
		string text = string.Format ("OnTouchStart X={0} Y={1}", e.Input.ScreenPosition.x, e.Input.ScreenPosition.y);
		Debug.Log (text);
}
```
### タッチ終了を取得
```csharp
void OnEnable ()
{
		TouchManager.Instance.TouchEnd += OnTouchEnd;
}
void OnDisable ()
{
		TouchManager.Instance.TouchEnd -= OnTouchEnd;
}
void OnTouchEnd (object sender, CustomInputEventArgs e)
{
		string text = string.Format ("OnTouchEnd X={0} Y={1}", e.Input.ScreenPosition.x, e.Input.ScreenPosition.y);
		Debug.Log (text);
}
```
### ドラッグを取得
```csharp
void OnEnable ()
{
		TouchManager.Instance.Drag += OnDrag;
}
void OnDisable ()
{
		TouchManager.Instance.Drag -= OnDrag;
}
void OnDrag (object sender, CustomInputEventArgs e)
{
		string text = string.Format ("OnSwipe Pos[{0},{1}] Move[{2},{3}]", new object[] {
				e.Input.ScreenPosition.x.ToString ("0"),
				e.Input.ScreenPosition.y.ToString ("0"),
				e.Input.DeltaPosition.x.ToString ("0"),
				e.Input.DeltaPosition.y.ToString ("0")
		});
		Debug.Log (text);
}
```
### フリック開始を取得
```csharp
void OnEnable ()
{
		TouchManager.Instance.FlickStart += OnFlickStart;
}
void OnDisable ()
{
		TouchManager.Instance.FlickStart -= OnFlickStart;
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
```
### フリックを取得
```csharp
void OnEnable ()
{
		TouchManager.Instance.FlickComplete += OnFlickComplete;
}
void OnDisable ()
{
		TouchManager.Instance.FlickComplete -= OnFlickComplete;
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
}
```







## Contribution

1. Fork it ( https://github.com/naichilab/Unity-TouchManager/fork )
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create new Pull Request

## Licence
This software is released under the MIT License, see LICENSE.txt.
[MIT](https://github.com/naichilab/Unity-TouchManager/blob/master/LICENSE)

## 作者
[@naichilab](https://github.com/naichilab)
