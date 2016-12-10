using UnityEngine;
using System.Collections;

public class Ship2 : MonoBehaviour {
	Vector2 pos;
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		pos.x -= speed * Time.deltaTime;
		transform.position = pos;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Obstacle") {
			Destroy (gameObject);
			Destroy (col.collider.gameObject);
		}
	}
}
