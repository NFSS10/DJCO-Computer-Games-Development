using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

	private bool _enabled = true;

	//Function triggered when player passes through the finish line
	void OnTriggerEnter2D(Collider2D col)
	{
		//If collided with motorbike and its the back wheel then player successfully passed the level
		if (col.gameObject.CompareTag("Motorbike") && GameManager.instance.getState().Equals("Play"))
		{
			//Trigger only once
			if (_enabled)
			{
				GameManager.instance.setNewCheckPoint(transform.position);
				GetComponent<Animator>().enabled = true;
				_enabled = false;
			}
		}

	}

}
