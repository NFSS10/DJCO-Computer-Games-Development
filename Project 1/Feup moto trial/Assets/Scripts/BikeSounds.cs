using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSounds : MonoBehaviour
{
	public static BikeSounds instance;
	
	public AudioClip IdleSound;
	public AudioSource IdleSource;
	
	public AudioClip AccelerationSound;
	public AudioSource AccelerationSource;
	
	public AudioClip HornSound;
	public AudioSource HornSource;

	
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	// Starts the sound class
	void Start ()
	{
		IdleSource.volume = 0.5f;
		IdleSource.clip = IdleSound;
		IdleSource.loop = true;
		
		AccelerationSource.clip = AccelerationSound;
		AccelerationSource.volume = 0.3f;
		AccelerationSource.loop = true;
		
		HornSource.clip = HornSound;
		HornSource.volume = 0.5f;
	}
	
	// Updates the sound
	void Update ()
	{
		if (GameManager.instance.getState().Equals("Play") || GameManager.instance.getState().Equals("Countdown"))
		{
			if (!IdleSource.isPlaying)
				IdleSource.Play();

			if (Input.GetKeyDown(Controls.instance.moveForward))
			{
				AccelerationSource.Play();
			}
			
			if (Input.GetKeyUp(Controls.instance.moveForward))
			{
				AccelerationSource.Stop();
			}
			
			if (Input.GetKeyUp(Controls.instance.horn))
			{
				HornSource.Stop();
				HornSource.Play();
			}	
		}
	}

	// Stops all the sounds of the bike
	public void StopSounds()
	{
		StopIdleSound();
		StopAccelerationSound();
		StopHornSound();
	}

	// Stops the acceleration sound of the bike
	public void StopAccelerationSound()
	{
		if (AccelerationSource.isPlaying)
			AccelerationSource.Stop();
	}
	
	// Stops the idle sound of the bike
	public void StopIdleSound()
	{
		if (IdleSource.isPlaying)
			IdleSource.Stop();
	}

	public void StopHornSound()
	{
		if (HornSource.isPlaying)
			HornSource.Stop();
	}

	public bool HornIsPlaying()
	{
		return this.HornSource.isPlaying;
	}
}
