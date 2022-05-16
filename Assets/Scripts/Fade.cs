using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {

    public static void ToggleLevelFade(Animator fadeAnim) {
        fadeAnim.SetTrigger("FadeTrigger");
    }
    
    public void SwapLevel() {
        SceneManager.LoadScene(GameValues.level);
    }

    public void SwapLevelStr(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
