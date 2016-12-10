using UnityEngine;
using System.Collections;

public class Warning : MonoBehaviour {
	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		sprite.color = new Color(sprite.color.a, sprite.color.g, sprite.color.b, Mathf.PingPong(Time.time * 2f, 1));
	}
}
