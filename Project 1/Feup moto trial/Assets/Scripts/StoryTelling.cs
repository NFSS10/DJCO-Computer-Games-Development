using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTelling : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;

	public String name;
	[TextArea(3, 10)] public string[] sentences;
	private int index;

	// Use this for initialization
	void Start()
	{
		nameText.text = name;
		index = 0;

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (index == sentences.Length)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences[index];
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
		index++;
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	private void EndDialogue()
	{
		GameManager.instance.SetGameState(GameManager.State.MapOverview);
		GameManager.instance.SetStoryTellingActive(false);
	}

}
