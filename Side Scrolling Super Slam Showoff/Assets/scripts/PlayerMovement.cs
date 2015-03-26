using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {	
	public float speed;
	public float jumpheight;
	private Transform player; 
	private bool jumping = false;

	Rigidbody2D rb2d;

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		player = this.transform;
		rb2d.fixedAngle = true;
	}
	
	void FixedUpdate()
	{
		float x = Input.GetAxis("Horizontal");
		Vector2 pos = new Vector2 (x, 0f) * speed;
		//Debug.Log (pos);
		if (jumping == false) {
			rb2d.AddForce (pos);
		}
	}


	//Jump checks
	void OnCollisionExit2D(Collision2D other){
		jumping = true;
		Debug.Log ("Touching is false");
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Terrain" && other.gameObject.transform.position.y < player.position.y){
			jumping = false;
			Debug.Log ("Touching is true");
		}
	}
	
	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.tag == "Terrain" && other.gameObject.transform.position.y < player.position.y && other.gameObject.transform.position.y + player.localScale.y == player.position.y)
			 {
			jumping = false;
			Debug.Log ("Touching is true");
		}
	}

	void Update()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Debug.DrawLine(player.position, new Vector3(mousePos.x, mousePos.y, player.position.z));
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D rayCast = Physics2D.Raycast(mousePos, Vector2.zero);
			if(rayCast.collider == null) {
				player.position = new Vector3(mousePos.x,mousePos.y,player.position.z);
			} else {
				Debug.Log ("Struck something at: " + rayCast.collider.gameObject.transform.position);
			}
			//Debug.Log("Position " + mousePos);
		}
		if (Input.GetKeyDown ("space") && jumping == false) {
			Vector2 jump = new Vector2(0f,jumpheight);
			rb2d.AddForce(jump, ForceMode2D.Impulse);
		}
		//Debug.Log ("Jumping = " + jumping);
	}
}
