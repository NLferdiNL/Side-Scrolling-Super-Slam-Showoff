using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float Max;
	public Transform me;
	public int speed;
	private Rigidbody2D rb2d;

	void Awake(){
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.fixedAngle = true;
	}

	void FixedUpdate() {
		float x = Input.GetAxis("Horizontal");
		Vector2 pos = new Vector2 (x, 0f) * speed;
		rb2d.AddForce (pos);
		me.position = new Vector3(Mathf.PingPong(Time.time, Max), transform.position.y, transform.position.z);
	}
}