using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class TrackingSystem : MonoBehaviour
{
	public Transform spawnPoint;
	public Transform finishLine;

	public Transform checkPoint1;
	public Transform checkPoint2;
	public Transform checkPoint3;

	public Canvas canvas;

	private Slider _slider;

	void Start()
	{
		_slider = transform.GetChild(0).gameObject.GetComponent<Slider>();
		
		calculateCheckpointImgPosition(1);
		calculateCheckpointImgPosition(2);
		calculateCheckpointImgPosition(3);
		
		_slider.maxValue = finishLine.position.x - spawnPoint.position.x;
	}

	void calculateCheckpointImgPosition(int checkpoint)
	{
		Transform checkpointImg = _slider.transform.Find("CheckPoint" + checkpoint);
		checkpointImg.position = new Vector2((_slider.transform.position.x + checkpointImg.GetComponent<RectTransform>().sizeDelta.x/4 + (_slider.GetComponent<RectTransform>().sizeDelta.x) * (((getCheckPointByID(checkpoint).position.x - spawnPoint.position.x) / (finishLine.position.x - spawnPoint.position.x)) - 0.5f) * canvas.scaleFactor), checkpointImg.position.y);
	}

	Transform getCheckPointByID(int checkPoint)
	{
		if (checkPoint == 1)
			return checkPoint1;
		
		if (checkPoint == 2)
			return checkPoint2;
		
		return checkPoint3;
	}

	void LateUpdate()
	{
		
		_slider.value = GameManager.instance.GetBike().GetPosition().x;
	}
}
