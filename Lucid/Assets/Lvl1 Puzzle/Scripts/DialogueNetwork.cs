using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogueNetwork : MonoBehaviour {
	private Text textComponent;
	public string[] dialogueStrings;

	public float secondsBetweenCharacters = 0.13f;

	public KeyCode DialogueInput = KeyCode.Return;

	private bool isStringBeingRevealed = false; 
	private bool isDialoguePlaying = false; 
	private bool isEndOfDialogue = false; 

	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text>();
		textComponent.text = "";
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
