using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public Text timeDisplay;
	public Text recordTimeDisplay;
	public Text triesDisplay;
	
	private float seconds;
	private int minutes;
	
	// Starts the time to zero
	void Awake ()
	{
		seconds = minutes = 0;
		GameManager.instance.setGameTimerUI(this);
	}
	
	// Updates the time of the counter
	void Update ()
	{
		if (GameManager.instance.getState().Equals("Play"))
		{
			// Updates the seconds
			seconds += Time.deltaTime;
			
			if(seconds >= 60){
				minutes++;
				seconds = 0;
			}
			// Updates the UI
			timeDisplay.text = "Time: " + displayTimeFormat(minutes, (int)seconds);
		}	
	}

	public int getCurrentTime()
	{
		return (int)(minutes * 60 + seconds);
	}

	public void setRecordTime(int time)
	{
		recordTimeDisplay.text = "Record: " + displayTimeFormat(time / 60, time % 60);
	}

	String displayTimeFormat(int minutes, int seconds)
	{
		return String.Format("{0:00}", minutes) 
			+ ":" + String.Format("{0:00}", seconds);	
	}

	public void setActive(bool active)
	{
		gameObject.SetActive(active);
	}

	public void setActiveRecordTime(bool active)
	{
		recordTimeDisplay.gameObject.SetActive(active);
	}

	public void setActiveActualTime(bool active)
	{
		timeDisplay.gameObject.SetActive(active);
	}
	
	// Enables or disables the tries UI
	public void SetActiveNumberOfTries(bool active)
	{
		triesDisplay.gameObject.SetActive(active);
	}

	// Sets the lifes UI in the game
	public void SetNumberOfTries()
	{
		triesDisplay.text = "x " + GameManager.instance.GetTries();
	}
}
