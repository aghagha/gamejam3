using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Controller : MonoBehaviour {
	public float speed;
	public float force;
	public String level;
    public bool dead = false ;
	Rigidbody2D rb;
    Animator boost;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		if(level=="Gravity") rb.AddForce (new Vector2 (force*Time.deltaTime,0f),ForceMode2D.Impulse);
	    boost = GameObject.Find("Ship/Boost").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    void LateUpdate()
    {
        if(dead)boostMati();
    }

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Finish") {
			finish ();
		} else if (col.collider.tag == "Portal") {
			teleport (col.collider.name);
		} else if (col.collider.tag == "Obstacle" || col.collider.tag == "MonsterKiller")
		{
            if(col.collider.name != "Monster") Destroy(col.collider.gameObject);
		    dead = true;
		    GameObject explosion = Instantiate(Resources.Load("Prefabs/Explosion")) as GameObject;
		    explosion.transform.position = transform.position;
		    gameObject.GetComponent<BoxCollider2D>().enabled = false;
		    gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //die();
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

    public void boostMati(){
        boost.SetBool("boost",false);
    }

    public void boostHidup()
    {
        boost.SetBool("boost", true);
    }

	public void  berhenti(){
		Vector2 pos = transform.position;
	    if (!Input.GetKey(KeyCode.Mouse0) && !dead)
	    {
	        boostHidup();
	        pos.x += speed*Time.deltaTime;
	    }
	    else
	    {
	        boostMati();
	    }
		transform.position = pos;
	}

	public void tap(){
		Vector2 pos = new Vector2 (speed * 6, 0);
	    if (Input.GetKeyDown(KeyCode.Mouse0) && !dead)
	    {
	        transform.Translate(pos*Time.deltaTime);
	        boostHidup();
	    }
	    else
	    {
	        boostMati();
	    }
	}

	public void jalan(){
		Vector2 pos = transform.position;
	    if (Input.GetKey(KeyCode.Mouse0) && !dead)
	    {
	        pos.x += speed*Time.deltaTime;
	        boostHidup();
	    }
	    else
	    {
	        boostMati();
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
            boostMati();
			pos.y -= speed * Time.deltaTime;
		} else {
            if(!dead)boostHidup();
			pos.y += jetSpeed * Time.deltaTime;
		}
		transform.position = pos;
	}

	public void die(){
        if(GameObject.Find("Ship").GetComponent<Controller>().dead)
		    SceneManager.LoadScene (SceneManager.GetActiveScene().name);
        else Destroy(gameObject);
	}

	public void finish(){
			SceneManager.LoadScene ((Int32.Parse(SceneManager.GetActiveScene().name)+1).ToString());

	}
}
