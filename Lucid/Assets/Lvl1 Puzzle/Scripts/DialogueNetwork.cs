using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class DialogueNetwork : MonoBehaviour {
	private Text textComponent;
	[TextArea(3, 10)]
	public string[] dialogueStrings;

	// therapist neg/pos dialogues
	public string[] therapistNegative;
	public string[] therapistPositive;

	// game objects
	public GameObject Fu;
	public GameObject Therapist;

	public float secondsBetweenCharacters = 0.03f;

	public KeyCode DialogueInput = KeyCode.Return;

	// boolean variables to handle dialogue flow
	private bool isStringBeingRevealed = false; 
	private bool isDialoguePlaying = false; 
	private bool isEndOfDialogue = false; 
	private bool isPlayingPuzzle = false;
	private int counterToEndGame = 0;

	// keep track of time
	private float timeLeft = 15.0f; 

	// score
	public Text score; // actual score = Int32.Parse(score.text.substring(7));
	int currentScore = 0;

	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text>();
		textComponent.text = "";
//		HideFu();
//		HideTherapist();
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayingPuzzle) {
			timeLeft -= Time.deltaTime;
			// Print Negative Dialogue if Score isn't changed in 15 seconds
			if(timeLeft <= 0) {
				timeLeft = 15.0f;
				StartCoroutine(DisplayString(therapistNegative[counterToEndGame++]));

				if (counterToEndGame >= 5) {
					// restart scene
					restartCurrentScene();
				}
			}

			if(CreateGame.Score > currentScore) {
				System.Random random = new System.Random();
				int randomIndex = random.Next(0, 5);

				currentScore = CreateGame.Score;
				timeLeft = 15.0f;
				StartCoroutine(DisplayString(therapistPositive[randomIndex]));
			}
		}

		if(Input.GetKeyDown(KeyCode.Return)) 
		{
			if(!isDialoguePlaying) 
			{
				isDialoguePlaying = true;
				StartCoroutine(StartDialogue());
			}
		}
	}

	public void restartCurrentScene()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	private IEnumerator StartDialogue()
	{
		int dialogueLength = dialogueStrings.Length;
		int currentDialogueIndex = 0;

		while (currentDialogueIndex < dialogueLength || !isStringBeingRevealed) 
		{
			if (!isStringBeingRevealed) {
				isStringBeingRevealed = true;

				if (currentDialogueIndex == 1 || currentDialogueIndex == 5) {
//					ShowTherapist();
//					HideFu();
				}

				if (currentDialogueIndex == 3) {
//					HideTherapist();
//					ShowFu();
				}

				if (currentDialogueIndex == 8) {
					// Intro Dialogue is Done; Puzzle begins
					isPlayingPuzzle = true;
				}

				// Print Normal Dialogue
				if (!isPlayingPuzzle) {
					StartCoroutine (DisplayString(dialogueStrings[currentDialogueIndex++]));
				}

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

	// Show/Hide Character Fu
	private void HideFu()
	{
		Fu.SetActive(false);
	}

	private void ShowFu()
	{
		Fu.SetActive(true);
	}

	// Show/Hide Character Therapist
	private void HideTherapist()
	{
		Therapist.SetActive(false);
	}

	private void ShowTherapist()
	{
		Therapist.SetActive(true);
	}
}