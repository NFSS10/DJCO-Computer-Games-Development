using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class UISounds : MonoBehaviour {
	
	public static UISounds instance;
	
	public AudioClip CountdownSound;
	public AudioSource CountdownSource;
	
	public AudioClip GameoverSound;
	public AudioSource GameoverSource;

	public AudioClip GamewinSound;
	public AudioSource GamewinSource;
	
	public AudioClip JumpSound;
	public AudioSource JumpSource;
	
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}
	
	// Use this for initialization
	void Start () {
		GameoverSource.clip = GameoverSound;
		GameoverSource.volume = 0.2f;
		CountdownSource.clip = CountdownSound;

		GamewinSource.clip = GamewinSound;
		GamewinSource.volume = 0.2f;
		
		JumpSource.clip = JumpSound;
		JumpSource.volume = 0.5f;
	}

	public void PlayGameover()
	{
		if (!GameoverSource.isPlaying)
			GameoverSource.Play();
	}

	public void PlayGamewin()
	{
		if (!GamewinSource.isPlaying)
			GamewinSource.Play();
	}

	public void PlayCountdown()
	{
		if (!CountdownSource.isPlaying)
			CountdownSource.Play();
	}
	
	public void PlayJumpPad()
	{
		if (!JumpSource.isPlaying)
			JumpSource.Play();
	}

	public void StopAllSounds()
	{
		if (CountdownSource.isPlaying)
			CountdownSource.Stop();
		
		if (GameoverSource.isPlaying)
			GameoverSource.Stop();

		if (GamewinSource.isPlaying)
			GamewinSource.Stop();
		
		if (JumpSource.isPlaying)
			JumpSource.Stop();
	}
}
