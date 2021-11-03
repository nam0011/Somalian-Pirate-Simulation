using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is attached to background sprite
public class shipScript : MonoBehaviour
{
    public GameObject cargo;    // drag prefab into this in inspector
    public GameObject pirate;   // ^^
    public GameObject captured;
    public GameObject patrol;

    public List<Transform> cargoSpawn;
    public List<Transform> pirateSpawn;
    public List<Transform> patrolSpawn;

    public static int cargosEnter;
    public static int cargosExit;
    public static int cargosCapture;
    public static int cargosRescue;
    public static int patrolsEnter;
    public static int patrolsExit;
    public static int piratesEnter;
    public static int piratesExit;
    public static int piratesDefeat;
    public static int evadesNotCapture;
    public static int evadesCapture;
    public static int timeStepCounter;

    private int cargoSpawnLocation;
    private int pirateSpawnLocation;
    private int patrolSpawnLocation;

    Text cargoCounterLabel;
    Text pirateCounterLabel;
    Text patrolCounterLabel;

    Text cargoExitedCounterLabel;
    Text pirateExitedCounterLabel;
    Text patrolExitedCounterLabel;

    Text cargoEnteredCounterLabel;
    Text pirateEnteredCounterLabel;
    Text patrolEnteredCounterLabel;

    Text timeStepCounterLabel;


    private int cargoSpawnProbability = 50;
    private int patrolSpawnProbability = 25;
    private int pirateSpawnProbability = 40;

    // Start is called before the first frame update
    void Start()
    {
        cargosEnter = 0;
        cargosExit = 0;
        cargosCapture = 0;
        cargosRescue = 0;
        patrolsEnter = 0;
        patrolsExit = 0;
        piratesEnter = 0;
        piratesExit = 0;
        piratesDefeat = 0;
        evadesNotCapture = 0;
        evadesCapture = 0;
        timeStepCounter = 0;

        //find the UI elements
        cargoCounterLabel = GameObject.Find("CargoCounter").GetComponent<Text>();
        pirateCounterLabel = GameObject.Find("PirateCounter").GetComponent<Text>();
        patrolCounterLabel = GameObject.Find("PatrolCounter").GetComponent<Text>();

        cargoExitedCounterLabel = GameObject.Find("CargoCounter_Exited").GetComponent<Text>();
        pirateExitedCounterLabel = GameObject.Find("PirateCounter_Exited").GetComponent<Text>();
        patrolExitedCounterLabel = GameObject.Find("PatrolCounter_Exited").GetComponent<Text>();

        cargoEnteredCounterLabel = GameObject.Find("CargoCounter_Entered").GetComponent<Text>();
        pirateEnteredCounterLabel = GameObject.Find("PirateCounter_Entered").GetComponent<Text>();
        patrolEnteredCounterLabel = GameObject.Find("PatrolCounter_Entered").GetComponent<Text>();

        timeStepCounterLabel = GameObject.Find("TimeStepCounter").GetComponent<Text>();

        // initialize the UI elements
        cargoCounterLabel.text = "Cargos Current: " + GameObject.FindGameObjectsWithTag("cargo").Length.ToString();
        pirateCounterLabel.text = "Pirates Current: " + GameObject.FindGameObjectsWithTag("pirate").Length.ToString();
        patrolCounterLabel.text = "Patrols Current: " + GameObject.FindGameObjectsWithTag("patrol").Length.ToString();

        cargoExitedCounterLabel.text = "Cargos Exited: " + cargosExit.ToString();
        pirateExitedCounterLabel.text = "Pirates Exited: " + piratesExit.ToString();
        patrolExitedCounterLabel.text = "Patrols Exited: " + patrolsExit.ToString();

        cargoEnteredCounterLabel.text = "Cargos Entered: " + cargosEnter.ToString();
        pirateEnteredCounterLabel.text = "Pirates Entered: " + piratesEnter.ToString();
        patrolEnteredCounterLabel.text = "Patrols Entered: " + patrolsEnter.ToString();

        timeStepCounterLabel.text = "Time Step: " + timeStepCounter.ToString();

        InvokeRepeating("spawnShip", 0.0f, 1.0f);
        InvokeRepeating("updateCounters", 0.0f, 1.0f);
    }
    void Update()
    {
        if (Input.GetKeyDown("" + 1))
        {
            cargoSpawnProbability = 50;
        }
        else if (Input.GetKeyDown("" + 2))
        {
            cargoSpawnProbability = 75;
        }
        else if (Input.GetKeyDown("" + 3))
        {
            cargoSpawnProbability = 90;
        }

        if (Input.GetKeyDown("" + 4))
        {
            patrolSpawnProbability = 25;
        }
        else if (Input.GetKeyDown("" + 5))
        {
            patrolSpawnProbability = 50;
        }
        else if (Input.GetKeyDown("" + 6))
        {
            patrolSpawnProbability = 70;
        }

        if (Input.GetKeyDown("" + 7))
        {
            pirateSpawnProbability = 40;
        }
        else if (Input.GetKeyDown("" + 8))
        {
            pirateSpawnProbability = 60;
        }
        else if (Input.GetKeyDown("" + 9))
        {
            pirateSpawnProbability = 80;
        }
    }

    private void updateCounters()
    {
        // set the text for each counter by getting the length of the array of GameObjects with the corresponding tag
        cargoCounterLabel.text = "Cargos Current: " + GameObject.FindGameObjectsWithTag("cargo").Length.ToString();
        pirateCounterLabel.text = "Pirates Current: " + GameObject.FindGameObjectsWithTag("pirate").Length.ToString();
        patrolCounterLabel.text = "Patrols Current: " + GameObject.FindGameObjectsWithTag("patrol").Length.ToString();

        cargoExitedCounterLabel.text = "Cargos Exited: " + cargosExit.ToString();
        pirateExitedCounterLabel.text = "Pirates Exited: " + piratesExit.ToString();
        patrolExitedCounterLabel.text = "Patrols Exited: " + patrolsExit.ToString();

        cargoEnteredCounterLabel.text = "Cargos Entered: " + cargosEnter.ToString();
        pirateEnteredCounterLabel.text = "Pirates Entered: " + piratesEnter.ToString();
        patrolEnteredCounterLabel.text = "Patrols Entered: " + patrolsEnter.ToString();


        timeStepCounterLabel.text = "Time Step: " + timeStepCounter++.ToString();
    }

    private void spawnShip()
    {
        // Roll to spawn cargo ship. Probability of 0.5
        if (Random.Range(0, 100) <= cargoSpawnProbability)
        {
            cargoSpawnLocation = Random.Range(0, 100);
            Instantiate(cargo, cargoSpawn[cargoSpawnLocation].position, Quaternion.identity);
            // update cargo entered counts
            cargosEnter++;
        }

        // Roll to spawn pirate ship. Probability of 0.4
        if (Random.Range(0, 100) <= pirateSpawnProbability)
        {
            pirateSpawnLocation = Random.Range(0, 400);
            Instantiate(pirate, pirateSpawn[pirateSpawnLocation].position, Quaternion.identity);
            // update pirate entered counts
            piratesEnter++;
        }

        //Roll to spawn patrol ship. Probability of .25
        if (Random.Range(0, 100) <= patrolSpawnProbability)
        {
            patrolSpawnLocation = Random.Range(0, 100);
            Instantiate(patrol, patrolSpawn[patrolSpawnLocation].position, Quaternion.identity);
            // update patrol entered counts
            patrolsEnter++;
        }
    }

    public int getTimeStep()
    {
        return timeStepCounter;
    }

    public void incrementTimeStep()
    {
        timeStepCounter++;
    }

}
