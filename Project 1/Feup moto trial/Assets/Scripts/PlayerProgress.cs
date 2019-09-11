using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress
{
	private int _recordTime;

	public PlayerProgress(int recordTime)
	{
		_recordTime = recordTime;
	}

	public int getRecordTime()
	{
		return _recordTime;
	}

	public void setRecordTime(int newRecord)
	{
		_recordTime = newRecord;
	}
}
