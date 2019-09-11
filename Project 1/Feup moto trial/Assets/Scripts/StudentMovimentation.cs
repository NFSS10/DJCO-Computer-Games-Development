using System;
using UnityEngine;

public class StudentMovimentation : MonoBehaviour
{
	private Collider2D collider;
	private Animator animation;
	private Vector3 initialPos;
	private SpriteRenderer spriteRenderer;
	private Vector3 startMarker;
	private Vector3 endMarker;
	private float timeElapsed;
	private float journeyLength;
	
	public float speed = 5.0F;
	public Sprite sprite;
	public float detectDistance;
	public float zDistance;
	
	private enum TeacherState
	{
		Start,
		Moving,
		Ended
	}
	private TeacherState teacherState;
		
	private void Start()
	{ 
		teacherState = TeacherState.Start;
		collider = GetComponent<Collider2D>();
		animation = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialPos = this.transform.position;
		
		startMarker = this.transform.position;
		endMarker = new Vector3(startMarker.x, startMarker.y, startMarker.z + zDistance);
		journeyLength = Vector3.Distance(startMarker, endMarker);
	}

	// Update is called once per frame
	void Update()
	{ 
		if (teacherState != TeacherState.Ended)
		{
			Bike bike = GameManager.instance.GetBike();
			if (Math.Abs(bike.GetPosition().x - this.transform.position.x) < detectDistance)
			{      
				if (BikeSounds.instance.HornIsPlaying())
					teacherState = TeacherState.Moving;

				if (teacherState == TeacherState.Moving)
				{
					timeElapsed += Time.deltaTime;
					float distCovered = (timeElapsed) * speed;
					float fracJourney = distCovered / journeyLength;

					this.animation.enabled = true;
					transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
					this.collider.enabled = false;
				}
			}

			if (CameraOverview.EqualVectors(this.transform.position,
				new Vector3(initialPos.x, initialPos.y, initialPos.z + zDistance)))
			{
				this.animation.enabled = false;
				teacherState = TeacherState.Ended;
				spriteRenderer.sprite = sprite;
			}
		}
	}
}
