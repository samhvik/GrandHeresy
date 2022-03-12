using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Recap : MonoBehaviour
{
    public TextMeshProUGUI objectivesNum;
    public TextMeshProUGUI downsNum;
    public TextMeshProUGUI P1Kills;
    public TextMeshProUGUI P2Kills;
    public TextMeshProUGUI P3Kills;
    public TextMeshProUGUI P4Kills;
    public TextMeshProUGUI MVP;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // closes the game application
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    void Start()
    {
        objectivesNum.SetText($"{GameValues.instance.objectivesCompleted}");

        // to add onto later
        //downsNum.SetText($"{GameValues.instance.downsNumber}");
        //P1Kills.SetText($"{GameValues.instance.P1Kills}");
        //P2Kills.SetText($"{GameValues.instance.P2Kills}");
        //P3Kills.SetText($"{GameValues.instance.P3Kills}");
        //P4Kills.SetText($"{GameValues.instance.P4Kills}");

        //int p1p2 = max(GameValues.instance.P1Kills, GameValues.instance.P2Kills);
        //int p3p4 = max(GameValues.instance.P3Kills, GameValues.instance.P4Kills);
        //int p = max(p1p2, p3p4);

        //MVMP.SetText($"{p}");
    }
}
