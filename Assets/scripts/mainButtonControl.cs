using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainButtonControl : MonoBehaviour
{
    public Button speed1;
    ButtonHandler speed1Script;
    public Button speed2;
    ButtonHandler speed2Script;
    public Button speed10;
    ButtonHandler speed10Script;
    public Button speed20;
    ButtonHandler speed20Script;
    public Button start;
    ButtonHandler startScript;
    public Button pause;
    ButtonHandler pauseScript;
    public Button restart;
    ButtonHandler restartScript;
    public Button singleStep;
    ButtonHandler singleScript;

    public static float currentSpeed = 1.0f;
    public float startSpeed = 0f;

    public GameObject shipSpawn;
    shipScript timeCounter;
    public int currentTimeStep = 0;
    public int testTimeStep = 0;

    Text currentSpeedLabel;

    void Awake()
    {
        //Time.timeScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed1Script = speed1.GetComponent<ButtonHandler>();
        speed2Script = speed2.GetComponent<ButtonHandler>();
        speed10Script = speed1.GetComponent<ButtonHandler>();
        speed20Script = speed20.GetComponent<ButtonHandler>();
        startScript = start.GetComponent<ButtonHandler>();
        pauseScript = pause.GetComponent<ButtonHandler>();
        restartScript = restart.GetComponent<ButtonHandler>();
        timeCounter = shipSpawn.GetComponent<shipScript>();
        singleScript = singleStep.GetComponent<ButtonHandler>();

        speed1.onClick.AddListener(() => ChangeSpeed(1f));
        speed2.onClick.AddListener(() => ChangeSpeed(2f));
        speed10.onClick.AddListener(() => ChangeSpeed(10f));
        speed20.onClick.AddListener(() => ChangeSpeed(20f));
        pause.onClick.AddListener(() => Pause());
        start.onClick.AddListener(() => StartSim());
        restart.onClick.AddListener(() => restartScene());
        singleStep.onClick.AddListener(() => singleStepSim());

        currentSpeedLabel = GameObject.Find("SpeedDisplay").GetComponent<Text>();
        currentSpeedLabel.text = "Speed: " + currentSpeed.ToString() + "X";
    }

    // Update is called once per frame
    void Update()
    {
        //Time.timeScale = startSpeed; //speed tied to start button is all that is ever seen by sim
        Invoke("getTimeStep", 0);
    }

    void ChangeSpeed(float timeStep)
    {
        currentSpeed = 1.0f / timeStep; //buttons change the current speed
        displaySpeed(timeStep);
    }

    void Pause()
    {
        InvokeUtil.isPaused = true; //pause button forces 0 as current speed
    }

    void displaySpeed(float time)
    {
        currentSpeedLabel.text = "Speed: " + time.ToString() + "X";

    }
    void checkSpeed() //check to see if we are paused or not
    {
        if (startSpeed != 0) //if not paused
        {
            startSpeed = currentSpeed; //set the start speed to the currently selected speed
        }
    }

    void getTimeStep()
    {
        currentTimeStep = timeCounter.getTimeStep() - 1; //this counter is 1 high
    }

    void StartSim()
    {
        /*if(startSpeed == 0 && currentSpeed == 0) //if we have never started the simulation and no speed is selected
        {
            currentSpeed = 1; //if start is clicked we update the play speed to start at 1
            startSpeed = currentSpeed; //start the simulation
        }
        else
        {
            startSpeed = currentSpeed; //otherwise spamming start does nothing
        }*/
        InvokeUtil.isPaused = false;
    }

    public void restartScene()
    {
        var currentSceneName = SceneManager.GetActiveScene().name; //get the current active scenes name
        SceneManager.LoadSceneAsync(currentSceneName); //load the scene
        startSpeed = 0; //set the start simulation speed to 1
    }

    public void singleStepSim()
    {
        if (InvokeUtil.isPaused) //if we are paused
        {
            InvokeUtil.isPaused = false;
            InvokeUtil.Invoke(this, () => singleStepPause(), 1.0f);
            timeCounter.incrementTimeStep(); //increment the step counter to display the incremented step
        }
    }

    void singleStepPause() {
        InvokeUtil.isPaused = true;
    }

    IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(10);
        startSpeed = 0; //check the speed
    }
}
