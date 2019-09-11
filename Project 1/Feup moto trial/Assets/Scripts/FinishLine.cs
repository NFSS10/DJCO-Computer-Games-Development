using UnityEngine;

public class FinishLine : MonoBehaviour
{
	private bool _enabled = true;
	//Function triggered when player passes through the finish line
	void OnTriggerEnter2D(Collider2D col)
	{
			//If collided with motorbike and its the back wheel then player successfully passed the level
			if (col.gameObject.CompareTag("Motorbike"))
			{
				if (col.gameObject.name.Equals("BackWheel"))
				{
					//Trigger only once
					if (_enabled)
					{
						GameManager.instance.WinGame();
						_enabled = false;
					}
				}
			}
		
	}
}
