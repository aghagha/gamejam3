using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public float trigger;
	public GameObject warning;
	public string arah;
	float rot, force;
	Rigidbody2D rb;
	GameObject ship;
	// Use this for initialization
	void Start () {
		force = 1200f;
		ship = GameObject.Find ("Ship");
		rb = GetComponent<Rigidbody2D> ();
		rot = Random.Range (-50, 50);
	}
	
	// Update is called once per frame
	void Update () {
		if (ship.transform.position.x >= trigger) {
			if(arah == "turun")
				rb.AddForce(new Vector2(0,-force *Time.deltaTime));
			else if(arah =="naik")
				rb.AddForce(new Vector2(0,force*Time.deltaTime));
			warning.gameObject.SetActive (false);
		}
		transform.Rotate (new Vector3 (0, 0, rot * Time.deltaTime));
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Portal") {
			Destroy (gameObject);
		}
	}
}
