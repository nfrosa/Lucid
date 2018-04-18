using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour {
	public Button myButton;

	// Use this for initialization
	void Start () {
		Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(switchScene);
	}

	void switchScene () {
		SceneManager.LoadScene("scene");
	}
}
