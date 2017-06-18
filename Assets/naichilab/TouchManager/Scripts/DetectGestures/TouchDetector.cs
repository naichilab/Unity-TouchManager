using UnityEngine;
using System.Collections;

namespace naichilab
{
		public class TouchDetector : MonoBehaviour,IGestureDetector
		{
				public void Enqueue (CustomInput currentInput)
				{
						if (currentInput.IsDown) {
								TouchManager.Instance.OnTouchStart (currentInput);
						}
						if (currentInput.IsUp) {
								TouchManager.Instance.OnTouchEnd (currentInput);
						}
				}
		}
}
