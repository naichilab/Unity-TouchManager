using UnityEngine;
using System.Collections;

namespace naichilab.InputEvents
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
