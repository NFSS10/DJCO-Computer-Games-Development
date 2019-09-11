using UnityEngine;

public class SplitScreenGameManager : MonoBehaviour
{
    public static SplitScreenGameManager instance;

    [SerializeField] GameObject bikePrefabPlayer1;
    [SerializeField] GameObject bikePrefabPlayer2;

    [SerializeField] CameraController cameraControllerPlayer1;
    [SerializeField] CameraController cameraControllerPlayer2;

    [SerializeField] SplitScreenControls gameControls;

    Bike bikePlayer1;
    Bike bikePlayer2;

    Vector2 spawnPoint;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        PlacePlayer1();
        PlacePlayer2();
    }



    // Set level spawn point
    public void SetSpawnPoint(Vector2 spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }



    void GameOver()
    {

    }

    public void RetryPlayer1()
    {
        Destroy(bikePlayer1.gameObject);

        PlacePlayer1();

    }

    public void RetryPlayer2()
    {

        Destroy(bikePlayer2.gameObject);

        PlacePlayer2();
    }

    void PlacePlayer1()
    {
        GameObject newBike = Instantiate(bikePrefabPlayer1, spawnPoint, Quaternion.identity);
        bikePlayer1 = newBike.GetComponent<Bike>();
        bikePlayer1.playerNumber = 1;


        cameraControllerPlayer1.SetBike(bikePlayer1);
        cameraControllerPlayer1.ResetCameraPosition();
        gameControls.SetPlayer1(bikePlayer1);
        bikePlayer1.SetPosition(spawnPoint);

    }
    void PlacePlayer2()
    {
        GameObject newBike = Instantiate(bikePrefabPlayer2, spawnPoint, Quaternion.identity);
        bikePlayer2 = newBike.GetComponent<Bike>();
        bikePlayer2.playerNumber = 2;


        cameraControllerPlayer2.SetBike(bikePlayer2);
        cameraControllerPlayer2.ResetCameraPosition();
        gameControls.SetPlayer2(bikePlayer2);
        bikePlayer2.SetPosition(spawnPoint);
    }

}
