﻿using UnityEngine;

public class CameraOverview : MonoBehaviour
{
	public int frontVelocity;
	public int backVelocity;
	public float zoomTime;
	public float timeOnFinishLine;
	public FinishLine finishLine;
	public Camera gameCamera;
	
	private const float PRECISION = 9.99999944E-3f;
	
	private enum CameraState
	{
		Forward,
		Backward,
		Stopped,
		ZoomIn,
		Ended
	}
	private CameraState cameraState;

	private float timeElapsedOnStopped;
	
	// Use this for initialization
	void Start ()
	{
		this.timeElapsedOnStopped = 0f;
		this.frontVelocity = 20;
		this.backVelocity = 100;
		this.zoomTime = 4f;
		this.timeOnFinishLine = 0.5f;
		cameraState = CameraState.Forward;
	}
	
	// Checks if a vector is equal then another in a certain precision
	public static bool EqualVectors(Vector3 lhs, Vector3 rhs)
	{
		return Vector3.SqrMagnitude(lhs - rhs) < PRECISION;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.instance.getState().Equals("MapOverview"))
		{
			if ((cameraState == CameraState.ZoomIn && EqualVectors(this.transform.position, gameCamera.transform.position))
			    || Input.anyKeyDown)
			{
				this.gameObject.SetActive(false);
				GameManager.instance.SetGameState(GameManager.State.ReadyToStart);
				GameManager.instance.SetStartUIActive();
				cameraState = CameraState.Ended;
			}
			else if (cameraState == CameraState.ZoomIn && !EqualVectors(this.transform.position, gameCamera.transform.position))
			{
				this.transform.position = Vector3.Lerp(this.transform.position, gameCamera.transform.position,
					timeElapsedOnStopped / zoomTime);
				timeElapsedOnStopped += Time.deltaTime;
			}
			else
			{
				if (cameraState == CameraState.Backward && this.transform.position.x <= gameCamera.transform.position.x)
				{
					cameraState = CameraState.ZoomIn;
				}
				else
				{
					if (cameraState == CameraState.Forward &&
					    frontVelocity * Time.deltaTime + this.transform.position.x < finishLine.transform.position.x)
					{
						this.transform.position = new Vector3(this.transform.position.x + frontVelocity * Time.deltaTime,
							this.transform.position.y, this.transform.position.z);
					}
					else if (cameraState == CameraState.Forward && frontVelocity * Time.deltaTime + this.transform.position.x >=
					         finishLine.transform.position.x)
					{
						cameraState = CameraState.Stopped;
						timeElapsedOnStopped += Time.deltaTime;
					}
					else
					{
						// Wait 1 second on the finish line
						if (timeElapsedOnStopped < this.timeOnFinishLine && cameraState == CameraState.Stopped)
						{
							timeElapsedOnStopped += Time.deltaTime;
						}
						else
						{
							if (this.transform.position.x - backVelocity * Time.deltaTime > gameCamera.transform.position.x)
								this.transform.position = new Vector3(this.transform.position.x - backVelocity * Time.deltaTime,
									this.transform.position.y, this.transform.position.z);
							else
								this.transform.position = new Vector3(gameCamera.transform.position.x, this.transform.position.y,
									this.transform.position.z);

							cameraState = CameraState.Backward;
							timeElapsedOnStopped = 0f;
						}
					}
				}
			}
		}
	}
}
