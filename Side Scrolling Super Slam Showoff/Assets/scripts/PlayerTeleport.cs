using UnityEngine;
using System.Collections;


public class PlayerTeleport : MonoBehaviour
	{
		private Transform player; 
		
		void Awake()
		{
		player = this.transform;
		GetComponent<Rigidbody2D>().fixedAngle = true;
		}
		
		void Update()
		{
		if (Input.GetMouseButtonDown (0)) {
			player.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y,player.position.z);
			Debug.Log(player.position);
		}
		}
	}

