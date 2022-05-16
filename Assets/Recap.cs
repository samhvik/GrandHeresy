using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class 
    Recap : MonoBehaviour
{
    public TextMeshProUGUI objectivesNum;
    public TextMeshProUGUI downsNum;
    public TextMeshProUGUI P1Kills;
    public TextMeshProUGUI P2Kills;
    public TextMeshProUGUI P3Kills;
    public TextMeshProUGUI P4Kills;
    public TextMeshProUGUI MVP;
    public Animator animator;

    public void MainMenu()
    {
        Fade.ToggleLevelFade(animator);
    }

    // closes the game application
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    private string findMVP(int[] kills) {
        var indexMax = -1;
        var max = int.MinValue;
        for (int i = 0; i < kills.Length; i++) {
            if (max < kills[i]) {
                indexMax = i;
                max = kills[i];
            }
        }

        return $"Player {indexMax+1}";
    }

    void Start() {
        var gameValues = GameValues.instance;
        objectivesNum.SetText($"{gameValues.objectivesCompleted}");

        // to add onto later
        //downsNum.SetText($"{GameValues.instance.downsNumber}");
        P1Kills.SetText($"{gameValues.playerKills[0]}");
        P2Kills.SetText($"{gameValues.playerKills[1]}");
        P3Kills.SetText($"{gameValues.playerKills[2]}");
        P4Kills.SetText($"{gameValues.playerKills[3]}");

        //int p1p2 = max(GameValues.instance.P1Kills, GameValues.instance.P2Kills);
        //int p3p4 = max(GameValues.instance.P3Kills, GameValues.instance.P4Kills);
        //int p = max(p1p2, p3p4);

        MVP.SetText(findMVP(gameValues.playerKills));
    }
}
