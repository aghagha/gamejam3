using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Controller : MonoBehaviour {
	public float speed;
	public float force;
	public String level;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		if(level=="Gravity") rb.AddForce (new Vector2 (force*Time.deltaTime,0f),ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.R)) {
			die ();
		}


		switch (level) {
		case "Berhenti":
			berhenti ();
			break;
		case "Gravity":
			gravity ();
			break;
		case "Tap":
			tap ();
			break;
		case "Jalan":
			jalan ();
			break;
		case "Jet":
			jet ();
			break;
		case "Teleport":
			jet ();
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Finish") {
			finish ();
		} else if (col.collider.tag == "Portal") {
			teleport (col.collider.name);
		} else if (col.collider.tag == "Obstacle" || col.collider.tag == "MonsterKiller") {
			die();
		}
	}

	public void gravity(){
		Vector2 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		float distance = 0;
		GameObject wormHole = GameObject.Find ("Worm Hole");
		if (wormHole != null) {
			distance = Vector2.Distance (wormHole.transform.position, transform.position);
			if (distance <= 3f) {
				Debug.Log (distance);
				rb.AddRelativeForce (wormHole.transform.position);
			}
			if (Input.GetKey (KeyCode.Mouse0)) {
				Vector3 wormPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				wormPos.z = 0.12f;	
				wormHole.transform.position = wormPos;
			}
		}
	}

	public void  berhenti(){
		Vector2 pos = transform.position;
		if (!Input.GetKey (KeyCode.Mouse0)) {
			pos.x += speed * Time.deltaTime;	
		}
		transform.position = pos;
	}

	public void tap(){
		Vector2 pos = new Vector2 (speed * 6, 0);
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			transform.Translate (pos * Time.deltaTime);
		}
	}

	public void jalan(){
		Vector2 pos = transform.position;
		if (Input.GetKey (KeyCode.Mouse0)) {
			pos.x += speed * Time.deltaTime;	
		}
		transform.position = pos;
	}

	public void teleport(string portal){
		int destination = Int32.Parse (portal) + 1;
		GameObject point = GameObject.Find (destination.ToString());
		Vector2 teleportTo = point.transform.position;
		teleportTo.x += 0.5f;
		transform.position = teleportTo;
	}

	public void jet(){
		float jetSpeed = speed / 1.5f;
		Vector2 pos = transform.position;
		pos.x += jetSpeed * Time.deltaTime;
		if (!Input.GetKey (KeyCode.Mouse0)) {
			pos.y -= speed * Time.deltaTime;
		} else {
			pos.y += jetSpeed * Time.deltaTime;
		}
		transform.position = pos;
	}

	public void die(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void finish(){
			SceneManager.LoadScene ((Int32.Parse(SceneManager.GetActiveScene().name)+1).ToString());

	}
}
