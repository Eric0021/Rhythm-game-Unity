using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void ButtonMoveScene(string level) {
        SceneManager.LoadScene(level);
    }

}
