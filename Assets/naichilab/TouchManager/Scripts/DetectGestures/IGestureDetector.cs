using UnityEngine;
using System.Collections;

namespace naichilab.InputEvents
{
		public interface IGestureDetector
		{
				void Enqueue (CustomInput currentInput);
		}
}
