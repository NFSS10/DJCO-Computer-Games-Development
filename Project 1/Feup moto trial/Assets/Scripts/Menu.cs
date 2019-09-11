using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public TMP_Dropdown resolutionDropdown;
	public TMP_Dropdown qualityDropdown;
	public TMP_Dropdown screenModeDropdown;
	public Slider soundVolumeSlider;
	public Slider musicVolumeSlider;
	public AudioMixer soundMixer;
	public AudioMixer musicMixer;
	public GameObject startButtons;
	
	Resolution[] resolutions;
	
	void Start()
	{
		// Set resolutions
		resolutions = Screen.resolutions;
		
		resolutionDropdown.ClearOptions();

		List<String> options = new List<String>();

		int currentResolutionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			options.Add(resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz");

			if (resolutions[i].width == Screen.currentResolution.width &&
			    resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			}
		}
		
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
		
		// Set default quality
		qualityDropdown.value = QualitySettings.GetQualityLevel();
		
		// Set default screen mode
		screenModeDropdown.value = Screen.fullScreen ? 0 : 1;
		
		// Set default sound volume
		float volume;
		soundMixer.GetFloat("SoundVolume", out volume);
		soundVolumeSlider.value = volume;
		
		// Set default sound volume
		musicMixer.GetFloat("MusicVolume", out volume);
		musicVolumeSlider.value = volume;
	}

	// Changes the display resolution
	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
	}

	// Changes de screen mode
	public void SetFullscreen(int fullscreenIndex)
	{
		Screen.fullScreen = (fullscreenIndex == 0) ? true : false;
	}

	// Sets a new sound effects volume
	public void SetSoundVolume(float volume)
	{
		soundMixer.SetFloat("SoundVolume", volume);
	}
	
	// Sets a new music volume
	public void SetMusicVolume(float volume)
	{
		musicMixer.SetFloat("MusicVolume", volume);
	}

	// Sets a new quality for the application
	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	// Enables the buttons for the game modes
	public void StartGameButton()
	{
		if(!startButtons.activeSelf)
			startButtons.SetActive(true);
		else
			startButtons.SetActive(false);

	}
	
	// Starts the first level of the game
	public void StartFirstLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	// Quits the game
	public void QuitGame()
	{
		Application.Quit();
	}
}
