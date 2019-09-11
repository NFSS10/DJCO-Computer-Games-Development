using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] CameraController cameraController;
    [SerializeField] int tries;
    [SerializeField] LevelCompletedUI levelCompletedUI;
    [SerializeField] GameOverUI gameOverdUI;
    [SerializeField] GameObject bikePrefab;


    public GameObject startMenuUI;
    public GameObject countdownUI;

    public GameObject _trackingSystem;

    private Vector2 spawnPoint;
    private Bike bike;
    private GameUI _gameUI;
    private PlayerProgress _playerProgress;

    public GameObject storyTeller;

    private Vector2 actualCheckPoint;

    private ArrayList powerups = new ArrayList();

    public GameObject featherPrefab;

    public enum State
    {
        StoryTelling,
        MapOverview,
        ReadyToStart,
        Countdown,
        Paused,
        Play,
        Over
    }
    private State gameState;
    private State previousGameState;

    // Checkes if there already exists one GameManager instance (See Singleton)
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Starts the GameManager
    void Start()
    {
        tries = 5;
        gameState = State.StoryTelling;

        SetStoryTellingActive(true);

        actualCheckPoint = spawnPoint;
        InitBike();

        //Load Record Time
        int recordTime = LoadPlayerProgress();

        if (recordTime > 0)
        {
            _playerProgress = new PlayerProgress(recordTime);
            _gameUI.setRecordTime(recordTime);
        }
        else
            _playerProgress = new PlayerProgress(Int32.MaxValue);

        _gameUI.SetNumberOfTries();
    }

    // Removes one try
    public void ReduceTries()
    {
        tries--;
        SetNormalGravity();
        bike.SetMovementSpeed(bike.GetNormalSpeed());

        if (tries < 1)
        {
            UISounds.instance.PlayGameover();
            GameOver();
        }
        else
            Retry();

        _gameUI.SetNumberOfTries();
    }

    //After the player fails the level
    //This function show the gameover UI
    public void GameOver()
    {
        gameOverdUI.gameObject.SetActive(true);
    }

    // Return the number of tries of the game
    public int GetTries()
    {
        return tries;
    }

    //TODO
    public void WinGame()
    {
        if (gameState == State.Play)
        {
            gameState = State.Over;

            int finalTime = _gameUI.getCurrentTime();

            if (finalTime <= _playerProgress.getRecordTime())
            {
                _playerProgress.setRecordTime(finalTime);
                SavePlayerProgress(finalTime);
                levelCompletedUI.SetNewRecordTime(finalTime);
                levelCompletedUI.setActiveRecordUI();
            }
            else
            {
                levelCompletedUI.SetActualTime(finalTime);
                levelCompletedUI.SetRecordText(_playerProgress.getRecordTime());
                levelCompletedUI.setActiveNoRecordUI();
            }

            UISounds.instance.PlayGamewin();
            BikeSounds.instance.StopSounds();
            _gameUI.setActive(false);
            levelCompletedUI.gameObject.SetActive(true);

            bike.StopBikeInstantly();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Go to the Main Menu
    public void HomeScreen()
    {
        gameState = State.Over;
        SceneManager.LoadScene("MainMenu");
    }

    // Pauses the game
    public void PauseGame()
    {
        previousGameState = gameState;
        gameState = State.Paused;
    }

    // Resumes the game
    public void ResumeGame()
    {
        gameState = previousGameState;
    }

    // Set level spawn point
    public void SetSpawnPoint(Vector2 spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    // Sets a new bike
    public void SetBike(Bike bike)
    {
        this.bike = bike;
    }

    public Bike GetBike()
    {
        return bike;
    }

    public void setGameTimerUI(GameUI gameUI)
    {
        _gameUI = gameUI;
    }

    public string getState()
    {
        return gameState.ToString();
    }

    // Resets the bike position
    void ResetBikePosition()
    {
        bike.SetPosition(actualCheckPoint);
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    int LoadPlayerProgress()
    {
        if (PlayerPrefs.HasKey("recordTime"))
            return PlayerPrefs.GetInt("recordTime");

        return 0;
    }

    void SavePlayerProgress(int recordTime)
    {
        PlayerPrefs.SetInt("recordTime", recordTime);
    }

    public void StartCountdown()
    {
        UISounds.instance.PlayCountdown();

        //stops the blinking animation and hides it
        startMenuUI.gameObject.GetComponent<Animator>().enabled = false;
        startMenuUI.SetActive(false);

        countdownUI.SetActive(true);

        gameState = State.Countdown;
    }

    public void StartGame()
    {
        //start game
        gameState = State.Play;

        //Game timer in game set visible
        _gameUI.setActiveRecordTime(true);
        _gameUI.setActiveActualTime(true);
        _gameUI.SetActiveNumberOfTries(true);

        _trackingSystem.SetActive(true);
    }

    //Resets the bike to the beggining and reduce the tries 
    public void Retry()
    {
        gameState = State.Play;
        Destroy(bike.gameObject);
        InitBike();
    }

    void InitBike()
    {
        GameObject newBike = Instantiate(bikePrefab, spawnPoint, Quaternion.identity);
        bike = newBike.GetComponent<Bike>();

        cameraController.SetBike(bike);
        cameraController.ResetCameraPosition();
        Controls.instance.InitControls();
        ResetBikePosition();
    }

    // Sets a new state
    public void SetGameState(State newState)
    {
        gameState = newState;
    }

    // Checkpoint reached so the oldest must be replaced
    public void setNewCheckPoint(Vector2 newCheckPoint)
    {
        actualCheckPoint = newCheckPoint;
    }

    public void SetStartUIActive()
    {
        startMenuUI.SetActive(true);
    }


    public void SetStoryTellingActive(bool active)
    {
        storyTeller.SetActive(active);
    }

    public void SetLowGravity(float duration)
    {
        bike.SetLowGravity();
        StartCoroutine(ExecuteAfterTime(duration));
    }

    private void SetNormalGravity()
    {
        bike.SetNormalGravity();
        
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SetNormalGravity();

    }
    
    public void SpeedBoostPowerUp(float durationTime, float speedBoostVal)
    {
        bike.PowerupSpeedBoost(durationTime, speedBoostVal);
    }

}
