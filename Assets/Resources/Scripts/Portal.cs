using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	Vector2 pos;
	public float speed;
	// Use this for initialization
	void Start () {
		pos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		pos.y -= speed * Time.deltaTime;
		transform.position = pos;
	}
}
