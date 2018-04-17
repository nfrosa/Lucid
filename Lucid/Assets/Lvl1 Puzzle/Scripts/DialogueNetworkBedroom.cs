﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogueNetworkBedroom : MonoBehaviour {
	private Text textComponent;
	[TextArea(3, 10)]
	public string[] dialogueStrings;
	public string[] therapistNegativeStrings;

	public float secondsBetweenCharacters = 0.7f;
    public GameObject chara;
	public KeyCode DialogueInput = KeyCode.Return;

	private bool isStringBeingRevealed = false; 
	private bool isDialoguePlaying = false; 
	private bool isEndOfDialogue = false;
    private Animator animator;
	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text>();
		textComponent.text = "";
        animator = chara.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) 
		{
			if(!isDialoguePlaying) 
			{
				isDialoguePlaying = true;
				StartCoroutine(StartDialogue());
			}
		}
	}

	private IEnumerator StartDialogue()
	{
		int dialogueLength = dialogueStrings.Length;
		int currentDialogueIndex = 0;
		while (currentDialogueIndex < dialogueLength || !isStringBeingRevealed) 
		{
			if (!isStringBeingRevealed) {
				isStringBeingRevealed = true;
				StartCoroutine(DisplayString(dialogueStrings[currentDialogueIndex++]));

				if (currentDialogueIndex >= dialogueLength) {
					isEndOfDialogue = true; 
				}
                if (currentDialogueIndex == 4)
                {
                    animator.SetInteger("move", 1);
                }

                if (currentDialogueIndex == 8)
                {
                    animator.SetInteger("move", 2);
                }

            }

			yield return 0;
		}

		while (true) 
		{
			if (Input.GetKeyDown(DialogueInput)) {
				break;
			}

			yield return 0;
		}

		isEndOfDialogue = false;
		isDialoguePlaying = false;
	}

	// Co-Routines
	private IEnumerator DisplayString(string stringToDisplay) 
	{
		int stringLength = stringToDisplay.Length;
		int currentCharIndex = 0; 

		// clear text component whenever co-routine is called
		textComponent.text = "";

		while (currentCharIndex < stringLength) 
		{
			textComponent.text += stringToDisplay[currentCharIndex];
			currentCharIndex++;

			if (currentCharIndex < stringLength) 
			{
				// yield statement
				yield return new WaitForSeconds(secondsBetweenCharacters);
			} 
			else {
				break;
			}
		} // end while loop

		while (true) 
		{
			if (Input.GetKeyDown (DialogueInput)) {
				break;
			}

			yield return 0;
		}

		isStringBeingRevealed = false;
		textComponent.text = ""; 
	}
}
