using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedUI : MonoBehaviour
{
    [SerializeField] GameObject noRecordSet;
    [SerializeField] GameObject newRecordTime;


    public Text recordText;
	public Text ActualTimeText;
	public Text newRecordText;
	
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

	public void SetRecordText(int time)
	{
		recordText.text = "Record Time: " + String.Format("{0:00}", time / 60) + ":" + String.Format("{0:00}", time % 60);
	}

	public void SetActualTime(int time)
	{
		ActualTimeText.text = "Actual Time: " + String.Format("{0:00}", time / 60) + ":" + String.Format("{0:00}", time % 60);
	}

	public void SetNewRecordTime(int time)
	{
		newRecordText.text = "New Record: " + String.Format("{0:00}", time / 60) + ":" + String.Format("{0:00}", time % 60);
	}

	public void setActiveNoRecordUI()
	{
        noRecordSet.SetActive(true);
	}

	public void setActiveRecordUI()
	{
        newRecordTime.SetActive(true);
	}
}
