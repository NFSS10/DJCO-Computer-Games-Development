using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    //Restart level
    public void Retry()
    {
        UISounds.instance.StopAllSounds();
        GameManager.instance.RestartLevel();
    }

    public void Home()
    {
        UISounds.instance.StopAllSounds();
        GameManager.instance.HomeScreen();
    }
}
