using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
	public float speed;
	private List<string> collisions = new List<string>();

	private void OnCollisionEnter2D(Collision2D other)
	{
		//If collided with motorbike and its the back wheel then player successfully passed the level
		if (other.gameObject.CompareTag("Motorbike"))
		{
			collisions.Add(other.gameObject.name);

			UISounds.instance.PlayJumpPad();
			gameObject.GetComponent<Animator>().SetBool("Jump", true);
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up*speed);
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		collisions.Remove(other.gameObject.name);
	}

	void StopAnimation()
	{
		gameObject.GetComponent<Animator>().SetBool("Jump", false);
	}
}
