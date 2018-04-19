using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterName : MonoBehaviour {
	public Text nameTextBox;

	public static string name = "";

	// Use this for initialization
	void Start () {
		nameTextBox = GetComponent<Text>();
		nameTextBox.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		nameTextBox.text = name;
	}
}
