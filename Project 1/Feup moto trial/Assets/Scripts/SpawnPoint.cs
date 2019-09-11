using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	// Sets the spawn point for the bike
	void Awake()
    {
        if (SplitScreenGameManager.instance != null)
            SplitScreenGameManager.instance.SetSpawnPoint(transform.position);
        else
            GameManager.instance.SetSpawnPoint(transform.position);
	}
}
