using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherBehaviour : MonoBehaviour
{
	public float Duration;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		//If collided with motorbike and its the back wheel then player successfully passed the level
		if (col.gameObject.CompareTag("Motorbike"))
		{
			GameManager.instance.SetLowGravity(Duration);
			Destroy(gameObject);	
		}
	}
}
