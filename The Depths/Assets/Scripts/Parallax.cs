using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	private Transform cam;
	private Vector3 origin;
	public bool ignoreY = true;

	private Vector3 target;
	private float t;
	private float z;

	private void Awake () {
		cam = Camera.main.transform;
		origin = transform.position;
		target = origin;
		t = 0f;
	}

	private void Update () {
		z = transform.position.z;
		t = z / (z + 1);
		transform.position = new Vector3(origin.x + (cam.position.x - origin.x) * t,
										 ignoreY ? origin.y : (origin.y + (cam.position.y - origin.y) * t),
										 origin.z);
	}
}