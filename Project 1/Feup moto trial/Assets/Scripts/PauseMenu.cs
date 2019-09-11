using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public static bool gamePaused = false;
	public GameObject pauseMenuUI;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (gamePaused)
				ResumeGame();
			else
				PauseGame();
		}
	}

	// Resumes the game
	public void ResumeGame()
	{
		pauseMenuUI.SetActive(false);
		ResumeTimeScale();
	}

	public void ResumeTimeScale()
	{
		Time.timeScale = 1f;
		GameManager.instance.ResumeGame();
		gamePaused = false;
	}

	public void PauseGame()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameManager.instance.PauseGame();
		gamePaused = true;
		BikeSounds.instance.StopSounds();
	}

	// Loads the main menu scene
	public void LoadMenu()
	{
		ResumeTimeScale();
		GameManager.instance.HomeScreen();
	}
	
	// Restarts the level
	public void RestartLevel()
	{
		ResumeTimeScale();
		GameManager.instance.RestartLevel();
	}
}
