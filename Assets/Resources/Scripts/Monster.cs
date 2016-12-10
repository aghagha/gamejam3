using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	public float speed;
	Vector2 pos;
	bool dead = false;
	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead)
			pos.x += speed * Time.deltaTime;
		else
			pos.y -= speed * Time.deltaTime;
		transform.position = pos;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Obstacle") {
			Destroy (col.collider.gameObject);
		} else if (col.collider.tag == "MonsterKiller") {
			Destroy (col.collider.gameObject);
			dead = true;
		}
	}
}
