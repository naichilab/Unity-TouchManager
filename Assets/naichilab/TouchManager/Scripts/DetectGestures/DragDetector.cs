using UnityEngine;
using System.Collections;

namespace naichilab.InputEvents
{
		public class DragDetector : MonoBehaviour,IGestureDetector
		{
				public void Enqueue (CustomInput currentInput)
				{
						if (currentInput.IsDrag) {
								TouchManager.Instance.OnDrag (currentInput);
						}
				}
		}
}
