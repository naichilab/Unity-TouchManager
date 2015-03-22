using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
		public Vector3 Speed;
		// Use this for initialization
		void Start ()
		{
	
		}
		// Update is called once per frame
		void Update ()
		{
				var offset = new Vector3 (this.Speed.x * Time.deltaTime, this.Speed.y * Time.deltaTime, 0);
				this.MovePosition (offset);
		}

		public void SetPosition (Vector3 pos)
		{
				this.transform.position = pos;
		}

		public void MovePosition (Vector3 offset)
		{
				var p = this.transform.position;
				this.transform.position = new Vector3 (p.x + offset.x, p.y + offset.y, p.z);
		}
}
