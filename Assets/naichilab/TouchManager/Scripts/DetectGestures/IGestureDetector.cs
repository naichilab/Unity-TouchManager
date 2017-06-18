using UnityEngine;
using System.Collections;

namespace naichilab
{
		public interface IGestureDetector
		{
				void Enqueue (CustomInput currentInput);
		}
}
