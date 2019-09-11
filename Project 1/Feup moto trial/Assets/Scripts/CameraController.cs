using UnityEngine;

public class CameraController : MonoBehaviour
{
	Bike player;
    Vector3 startPos;
	
	public SpawnPoint playerSpawnPoint;

	// Sets the initial position of the camera
	void Awake()
	{
		startPos = this.transform.position = new Vector3(this.transform.position.x, playerSpawnPoint.transform.position.y, this.transform.position.z);
	}
	
	// Updates the camera position
	void LateUpdate ()
	{
        if (SplitScreenGameManager.instance == null)
        {
            if (GameManager.instance.getState().Equals("Play"))
            {
                //camera follows motorbike in case it isnt the starting area
                if (player.GetPosition().x >= startPos.x)
                    this.transform.position = new Vector3(player.GetPosition().x, player.GetPosition().y + 1, startPos.z);
                else
                    this.transform.position = new Vector3(startPos.x, player.GetPosition().y + 1, startPos.z);
            }
        }
        else
        {
            //camera follows motorbike in case it isnt the starting area
            if (player.GetPosition().x >= startPos.x)
                this.transform.position = new Vector3(player.GetPosition().x, player.GetPosition().y + 1, startPos.z);
            else
                this.transform.position = new Vector3(startPos.x, player.GetPosition().y + 1, startPos.z);
        }
	}

    //Set the player that the camera will follow
    public void SetBike(Bike bike)
    {
        player = bike;
    }

    //Set camera to the start position
    public void ResetCameraPosition()
    {
        this.transform.position = startPos;
    }
}
