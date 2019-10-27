using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour {
	public Vector3 direction;
	public float speed = 5f;

	private void Update () {
		transform.position += direction.normalized * speed * Time.deltaTime;
	}
}