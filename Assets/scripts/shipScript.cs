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
    public static int evadesNotCaptured;
    public static int evadesCaptured;
    public static int timeStepCounter;

    private int cargoSpawnLocation;
    private int pirateSpawnLocation;
    private int patrolSpawnLocation;


    Text cargoCounterLabel;

    Text cargoExitedCounterLabel;
    Text pirateExitedCounterLabel;
    Text patrolExitedCounterLabel;

    Text cargoCapturedCounterLabel;
    Text captureRescuedCounterLabel;
    Text pirateEnteredCounterLabel;
    Text patrolEnteredCounterLabel;

    Text pirateDefeatedCounterLabel;

    Text cargoSpawnProbLabel;
    Text patrolSpawnProbLabel;
    Text pirateSpawnProbLabel;

    Text evadesNotCapturedLabel;
    Text evadesCapturedLabel;

    Text timeStepCounterLabel;

    private int cargoSpawnProbability = 50;
    private int patrolSpawnProbability = 25;
    private int pirateSpawnProbability = 40;

    private bool waitCounter = false;
    private bool waitSpawn = false;

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
        evadesNotCaptured = 0;
        evadesCaptured = 0;
        timeStepCounter = 0;


        //find the UI elements
        cargoCounterLabel = GameObject.Find("CargoCounter").GetComponent<Text>();

        cargoExitedCounterLabel = GameObject.Find("CargoCounter_Exited").GetComponent<Text>();
        pirateExitedCounterLabel = GameObject.Find("PirateCounter_Exited").GetComponent<Text>();
        patrolExitedCounterLabel = GameObject.Find("PatrolCounter_Exited").GetComponent<Text>();

        cargoCapturedCounterLabel = GameObject.Find("CargoCounter_Captured").GetComponent<Text>();
        captureRescuedCounterLabel = GameObject.Find("CargoCounter_CapturedRescued").GetComponent<Text>();
        pirateEnteredCounterLabel = GameObject.Find("PirateCounter_Entered").GetComponent<Text>();
        patrolEnteredCounterLabel = GameObject.Find("PatrolCounter_Entered").GetComponent<Text>();

        evadesCapturedLabel = GameObject.Find("EvadesCapturedCounter").GetComponent<Text>();
        evadesNotCapturedLabel = GameObject.Find("EvadesNotCapturedCounter").GetComponent<Text>();

        cargoSpawnProbLabel = GameObject.Find("CargoSpawnProbability").GetComponent<Text>();
        patrolSpawnProbLabel = GameObject.Find("PatrolSpawnProbability").GetComponent<Text>();
        pirateSpawnProbLabel = GameObject.Find("PirateSpawnProbability").GetComponent<Text>();

        timeStepCounterLabel = GameObject.Find("TimeStepCounter").GetComponent<Text>();

        pirateDefeatedCounterLabel = GameObject.Find("PirateDefeatedCounter").GetComponent<Text>();

        // initialize the UI elements
        cargoCounterLabel.text = "Cargos Entered: " + GameObject.FindGameObjectsWithTag("cargo").Length.ToString();

        cargoExitedCounterLabel.text = "Cargos Exited: " + cargosExit.ToString();
        pirateExitedCounterLabel.text = "Pirates Exited: " + piratesExit.ToString();
        patrolExitedCounterLabel.text = "Patrols Exited: " + patrolsExit.ToString();

        cargoCapturedCounterLabel.text = "Cargos Captured: " + cargosEnter.ToString();
        captureRescuedCounterLabel.text = "Captures Rescued: " + cargosRescue.ToString();
        pirateEnteredCounterLabel.text = "Pirates Entered: " + piratesEnter.ToString();
        patrolEnteredCounterLabel.text = "Patrols Entered: " + patrolsEnter.ToString();

        pirateDefeatedCounterLabel.text = "Pirates Defeated: " + piratesDefeat.ToString();

        evadesCapturedLabel.text = "Evades Captured: " + evadesCaptured.ToString();
        evadesNotCapturedLabel.text = "Evades Not Captured: " + evadesNotCaptured.ToString();

        timeStepCounterLabel.text = "Time Step: " + timeStepCounter.ToString();

        cargoSpawnProbLabel.text = "Cargo Spawn %: " + cargoSpawnProbability.ToString();
        patrolSpawnProbLabel.text = "Patrol Spawn %: " + patrolSpawnProbability.ToString();
        pirateSpawnProbLabel.text = "Pirate Spawn %: " + pirateSpawnProbability.ToString();

    }

    void Update()
    {

        if (!waitCounter && !InvokeUtil.isPaused) {
            waitCounter = true;
            InvokeUtil.Invoke(this, () => updateCounters(), mainButtonControl.currentSpeed);
        }
        if (!waitSpawn && !InvokeUtil.isPaused) {
            waitSpawn = true;
            InvokeUtil.Invoke(this, () => spawnShip(), mainButtonControl.currentSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cargoSpawnProbability = 50; //default -- spawn half of the time
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cargoSpawnProbability = 75; //spawn most of time
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cargoSpawnProbability = 100; //always spawn
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            patrolSpawnProbability = 25;    //default -- spawn some of the time
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            patrolSpawnProbability = 50;    //spawn half of the time
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            patrolSpawnProbability = 100; //always spawn
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            pirateSpawnProbability = 40; //default -- spawn almost half of the time
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            pirateSpawnProbability = 75; //spawn most of the time
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            pirateSpawnProbability = 100; //always spawn
        }

        updateSpawnPerc();
    }

    private void updateCounters()
    {
        // set the text for each counter by getting the length of the array of GameObjects with the corresponding tag
        cargoCounterLabel.text = "Cargos Entered: " + GameObject.FindGameObjectsWithTag("cargo").Length.ToString();
        cargoCapturedCounterLabel.text = "Cargos Captured: " + cargosCapture.ToString();
        cargoExitedCounterLabel.text = "Cargos Exited: " + cargosExit.ToString();
        captureRescuedCounterLabel.text = "Captures Rescued: " + cargosRescue.ToString();

        pirateEnteredCounterLabel.text = "Pirates Entered: " + piratesEnter.ToString();
        pirateExitedCounterLabel.text = "Pirates Exited: " + piratesExit.ToString();

        patrolEnteredCounterLabel.text = "Patrols Entered: " + patrolsEnter.ToString();
        patrolExitedCounterLabel.text = "Patrols Exited: " + patrolsExit.ToString();

        evadesCapturedLabel.text = "Evades Captured: " + evadesCaptured.ToString();
        evadesNotCapturedLabel.text = "Evades Not Captured: " + evadesNotCaptured.ToString();

        pirateDefeatedCounterLabel.text = "Pirates Defeated: " + piratesDefeat.ToString();

        timeStepCounterLabel.text = "Time Step: " + timeStepCounter++.ToString();

        waitCounter = false;
    }

    private void updateSpawnPerc()
    {
        cargoSpawnProbLabel.text = "Cargo Spawn %: " + cargoSpawnProbability.ToString();
        patrolSpawnProbLabel.text = "Patrol Spawn %: " + patrolSpawnProbability.ToString();
        pirateSpawnProbLabel.text = "Pirate Spawn %: " + pirateSpawnProbability.ToString();

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

        waitSpawn = false;
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
