using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {	
	public float speed;
	public float jumpHeight;
	public int maxTeleports;
	public Text teletext;
	private Transform player; 
	private bool jumping = false;
	private Rigidbody2D rb2d;

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		player = this.transform;
		teletext.text = "Teleports left: " + maxTeleports;
	}
	
	void FixedUpdate()
	{
		float x = Input.GetAxis("Horizontal");
		Vector2 pos = new Vector2 (x, 0f) * speed;
		rb2d.AddForce (pos);
	}



	//Jump checks
	void OnCollisionExit2D(Collision2D other){
		jumping = true;
		Debug.Log ("Touching is false");
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Terrain"){
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

	IEnumerator alert(float timeToWait = 0.1f){
		teletext.color =  new Color(255,0,0);
		yield return new WaitForSeconds(timeToWait);
		teletext.color = new Color(255,255,255);
		yield return new WaitForSeconds(timeToWait);
		teletext.color =  new Color(255,0,0);
		yield return new WaitForSeconds(timeToWait);
		teletext.color = new Color(255,255,255);
		yield return new WaitForSeconds(timeToWait);
		teletext.color =  new Color(255,0,0);
		yield return new WaitForSeconds(timeToWait);
		teletext.color = new Color(255,255,255);
		yield return new WaitForSeconds(timeToWait);
	}
	
	void Update()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(player.position,mousePos);
		if (Input.GetMouseButtonDown (0)) {
			if(maxTeleports > 0){
				if (hit.collider != null){
					print("Hit: " + hit.collider.gameObject.name);
				}else{
					player.position = new Vector3(mousePos.x,mousePos.y,player.position.z);
					maxTeleports--;
					if(maxTeleports == 0){
						teletext.text = "You've got no teleports left!";
					} else {
						teletext.text = "Teleports left: " + maxTeleports;
					}
				}
			} else {
				Debug.Log ("hi");
				StartCoroutine(alert());
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
		if (Input.GetKeyDown ("space") && jumping == false) {
			Vector2 jump = new Vector2(0f,jumpHeight);
			rb2d.AddForce(jump, ForceMode2D.Impulse);
		}
	}
}