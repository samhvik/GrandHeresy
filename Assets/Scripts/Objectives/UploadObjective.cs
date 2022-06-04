/*
    UploadObjective.cs

    Handles everything related to the "Upload Data" objective type.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadObjective : MonoBehaviour
{
    private float amountUploaded;
    public float speedOfUpload;
    private Renderer mat;

    public GameObject loadingBar;
    public Slider slider;
    public GameObject loadingBarText;
    public Canvas ourCanvas;
    public GameObject interactText;

    private GameObject bar;
    private GameObject barText;
    private GameObject barInteractText;

    private bool lockUpload;
    public float uploadRange;

    public GameObject ringRadius;
    private GameObject uploadRing;

    private ParticleSystem particleSystem;

    void Start()
    {
        ourCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        barInteractText = Instantiate(interactText, this.transform);

        amountUploaded = 0;
        lockUpload = false;
        mat = gameObject.GetComponent<Renderer>();
        particleSystem = GetComponent<ParticleSystem>();
        mat.material.color = Color.black;
    }

    void Update()
    {
        if (amountUploaded >= 100 && lockUpload == false)
        {
            mat.material.color = Color.yellow;

            lockUpload = true;
            amountUploaded = -1;

            bar = Instantiate(loadingBar);
            barText = Instantiate(loadingBarText);

            bar.transform.SetParent(ourCanvas.transform);
            bar.transform.transform.localPosition = new Vector3(0, 165, 0);
            barText.transform.SetParent(ourCanvas.transform);
            barText.transform.transform.localPosition = new Vector3(0, 165, 0);

            // Enable our Loading Bar and the Text
            bar.SetActive(true);
            barText.SetActive(true);
        }
        else if(amountUploaded == -1 && inRange())
        {
            currentlyUploading();
        }
    }

    // Begin uploading
    public void StartUpload()
    {
        if (lockUpload == false)
        {
            // Spawn our Ring with our given range
            ParticleSystem.ShapeModule ringShape = ringRadius.GetComponent<ParticleSystem>().shape;
            ringShape.radius = uploadRange;
            uploadRing = Instantiate(ringRadius, new Vector3(this.transform.position.x, this.transform.position.y - 0.95f, this.transform.position.z), this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));

            barInteractText.SetActive(false);

            // Fix this
            amountUploaded = 100;

            // enable combat for enemy spawning
            GameValues.instance.EnemyDirector.GetComponent<EnemyDirector>().StartObjectiveWave();
        }
    }

    // Update the loading bar
    public void currentlyUploading()
    {
        bar.GetComponent<Slider>().value += speedOfUpload;

        if(bar.GetComponent<Slider>().value >= 1)
        {
            doneUploading();
        }
    }

    public void doneUploading()
    {
        bar.SetActive(false);
        barText.SetActive(false);

        amountUploaded = -2;

        GameValues.instance.objectivesCompleted++;

        if (GameValues.instance.objectivesCompleted == GameValues.instance.objectivesTotal)
        {
            GameValues.instance.GameCompleted();
        }

        particleSystem.Stop();
        Destroy(uploadRing);
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponentInChildren<Light>().enabled = false;
        mat.material.color = Color.green;
    }

    // Checks to see if all players are in range of object
    public bool inRange()
    {
        if (GameValues.instance.numAlive == 1)
        {
            float dist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position);

            if (dist <= uploadRange)
            {
                barText.GetComponent<Text>().text = "Uploading... Stay Nearby";
                return true;
            }
        }
        else
        {
            for (int i = 0; i < GameValues.instance.getNumPlayers(); i++)
            {
                if(GameValues.instance.playerAlive[i] == true)
                {
                    float dist = Vector3.Distance(GameValues.instance.players[i].transform.position, this.transform.position);

                    if (dist >= uploadRange)
                    {
                        barText.GetComponent<Text>().text = "Out of Range, Return to Continue";
                        return false;
                    }
                }
            }

            barText.GetComponent<Text>().text = "Uploading... Stay Nearby";
            return true;
        }

        barText.GetComponent<Text>().text = "Out of Range, Return to Continue";
        return false;
    }
}
