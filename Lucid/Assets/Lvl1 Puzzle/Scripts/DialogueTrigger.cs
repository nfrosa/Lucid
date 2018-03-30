using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	// create Dialogue class
	public Dialogue dialogue;

	public void TriggerDialogue () {
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}
