using UnityEngine;
using System.Collections;

namespace naichilab
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
