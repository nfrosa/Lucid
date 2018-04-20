using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogueNetworkThera : MonoBehaviour {
	private Text textComponent;
	[TextArea(3, 10)]
	public string[] dialogueStrings;
	public string[] therapistNegativeStrings;

	public float secondsBetweenCharacters = 0.01f;
    public GameObject somi;
    public GameObject thera;
    private bool isMoving = false;
	public KeyCode DialogueInput = KeyCode.Return;
    private AnimationClip move1;
    private AnimationClip move2;
    public bool SceneSwitch1 = false;
    private bool isStringBeingRevealed = false; 
	private bool isDialoguePlaying = false; 
	private bool isEndOfDialogue = false;
    public Canvas canvas;
    public Image window;
    public Image enter;
    public Image loading;
    public Image somisprite;
    public Image therasprite;
    private Animator somimove;
    private Animator theramove; 
    public Animator theraanimator;
    public Animator somianimator;
    // Use this for initialization
    void Start () {
       // canvas.enabled = false;
        window.enabled = false;
        loading.enabled = false; 
        somisprite.enabled = false;
        therasprite.enabled = false;
        textComponent = GetComponent<Text>();
		textComponent.text = "";
        somimove = somi.GetComponent<Animator>();
        theramove = thera.GetComponent<Animator>();
        foreach (AnimationClip clip in somimove.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "WalkChara1")
            {
                move1 = clip;
            }

        }
        foreach (AnimationClip clip in theramove.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "therawalk")
            {
                move2 = clip;
            }

        }
    }


    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Return)&& !isMoving) 
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
        window.enabled = true;
        therasprite.enabled = true;
        somisprite.enabled = true;
        int dialogueLength = dialogueStrings.Length;
		int currentDialogueIndex = 0;
		while (currentDialogueIndex < dialogueLength || !isStringBeingRevealed) 
		{

            if (!isStringBeingRevealed && !isMoving) {
				isStringBeingRevealed = true;
				if (currentDialogueIndex == 0) 
				{
					Speaker (therasprite);
					ShowTherapist();
				}

                if (currentDialogueIndex == 1)
                {
					ShowSomi();
                    Speaker(somisprite);
                    somianimator.SetInteger("mood", 1); //happy
                }

                if (currentDialogueIndex == 2)
                {
                    Speaker(therasprite);
                    theraanimator.SetInteger("mood", 1);
					ShowTherapist();
                }

                if (currentDialogueIndex == 3)
                {
                    canvas.enabled = false;
                    somimove.SetInteger("move", 1);
                    theramove.SetInteger("move", 1);
                    isMoving = true;
                    float waitlength = Mathf.Max(move1.length, move2.length);
                    yield return new WaitForSeconds(waitlength);
                    isMoving = false;
                    canvas.enabled = true;
                    Speaker(therasprite);
                    theraanimator.SetInteger("mood", 0);
                }
                if (currentDialogueIndex == 4)
                {
                    Speaker(somisprite);
                    somianimator.SetInteger("mood", 2); //sad
					ShowSomi();
                }
                if (currentDialogueIndex == 5)
                    Speaker(somisprite);
                if (currentDialogueIndex == 6)
                    Speaker(somisprite);
				if (currentDialogueIndex == 7) {
					Speaker (therasprite);
					ShowTherapist();
				}
					
                somianimator.SetBool("talk",true);
                theraanimator.SetBool("talk", true);
                StartCoroutine(DisplayString(dialogueStrings[currentDialogueIndex++]));

				if (currentDialogueIndex >= dialogueLength) {
					isEndOfDialogue = true;
                    SceneSwitch1 = true;
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
        canvas.enabled = true;
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

        somianimator.SetBool("talk", false);
        theraanimator.SetBool("talk", false);
        
        while (true) 
		{
			if (Input.GetKeyDown (DialogueInput)) {
                if (SceneSwitch1)
                {
                    canvas.enabled = false;
                    enter.enabled = false;
                    loading.enabled = true;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Puzzle");
                }
                break;
			}
			yield return 0;
		}
        
        isStringBeingRevealed = false;
		textComponent.text = ""; 
	}

    void Speaker(Image speak)
    {
        if(speak == somisprite)
        {
            therasprite.enabled = false;
            somisprite.enabled = true;
        }
        else
        {
            therasprite.enabled = true;
            somisprite.enabled = false;
        }
    }

	private void ShowSomi()
	{
		CharacterName.name = "Somi: ";
	}

	private void HideSomi()
	{
		CharacterName.name = "";
	}

	private void ShowTherapist()
	{
		CharacterName.name = "Dr. Helen: ";
	}
}
