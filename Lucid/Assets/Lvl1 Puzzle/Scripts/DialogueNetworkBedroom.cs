using System.Collections;
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
    private bool isMoving = false;
    public Canvas canvas;
    public Image window;
    public Image sprite;
    public Image enter;
    public Image loading;
    public KeyCode DialogueInput = KeyCode.Return;
    private AnimationClip move1;
    private AnimationClip move2;
    public Animator spriteAnimator;
    public bool SceneSwitch1 = false;
    private bool isStringBeingRevealed = false; 
	private bool isDialoguePlaying = false; 
	private bool isEndOfDialogue = false;
    private Animator animator;
	// Use this for initialization
	void Start () {
       // canvas.enabled = false;
        window.enabled = false;
        sprite.enabled = false;
        loading.enabled = false;
        textComponent = GetComponent<Text>();
		textComponent.text = "";
        animator = chara.GetComponent<Animator>();
        //gets animation clips for cutscene
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "move1")
            {
                move1 = clip;
            }
            if (clip.name == "move2")
            {
                move2 = clip;
            }
        }

		HideSomi();
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
        sprite.enabled = true;
        int dialogueLength = dialogueStrings.Length;
		int currentDialogueIndex = 0;
		while (currentDialogueIndex < dialogueLength || !isStringBeingRevealed) 
		{
			ShowSomi();
            if (!isStringBeingRevealed && !isMoving) {
				isStringBeingRevealed = true;
                if (currentDialogueIndex == 4)
                {
					HideSomi();
                    //wait for string to finish printing
                    canvas.enabled = false;
                    animator.SetInteger("move", 1);
                    isMoving = true;
                    yield return new WaitForSeconds(move1.length);
                    isMoving = false;
                    canvas.enabled = true;
                }
                if (currentDialogueIndex == 8)
                {
					HideSomi();
                    //wait for string to finish printing
                    canvas.enabled = false;
                    animator.SetInteger("move", 2);
                    isMoving = true;
                    yield return new WaitForSeconds(move1.length);
                    isMoving = false;
                    canvas.enabled = true;
                }
                
                spriteAnimator.SetBool("talk",true);
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

        spriteAnimator.SetBool("talk", false);
        
        while (true) 
		{
			if (Input.GetKeyDown (DialogueInput)) {
                if (SceneSwitch1)
                {
                    window.enabled = false;
                    sprite.enabled = false;
                    enter.enabled = false;
                    loading.enabled = true;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("scene");
                }
                break;
			}
			yield return 0;
		}
        
        isStringBeingRevealed = false;
		textComponent.text = ""; 
	}

	private void ShowSomi()
	{
		CharacterName.name = "Somi: ";
	}

	private void HideSomi()
	{
		CharacterName.name = "";
	}
}
