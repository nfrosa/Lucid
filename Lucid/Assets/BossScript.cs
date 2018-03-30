using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Puzzle");
    }
}
