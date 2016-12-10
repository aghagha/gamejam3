using UnityEngine;
using System.Collections;

public class Asteroid2 : MonoBehaviour {	
	float rot;
	Vector2 pos;
	public float speed;
	// Use this for initialization
	void Start () {
		pos = transform.position;
		rot = Random.Range (-50, 50);
	}
	
	// Update is called once per frame
	void Update () {
		pos.y -= speed * Time.deltaTime;
		transform.position = pos;
		transform.Rotate (new Vector3(0,0,rot*Time.deltaTime));
	}
}
