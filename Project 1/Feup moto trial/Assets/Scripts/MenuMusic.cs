using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {

	public static MenuMusic instance;
	
	public AudioClip Music;
	public AudioSource MusicSource;
	
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}
	
	// Use this for initialization
	void Start () 
	{
		MusicSource.clip = Music;
		MusicSource.loop = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!MusicSource.isPlaying)
			MusicSource.Play();
	}
}
