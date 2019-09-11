using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Bike))]
public class Controls : MonoBehaviour
{
    public static Controls instance;

    [Header("Botões")]
    public string moveForward;
    public string moveBackwards;
    public string rotateLeft;
    public string rotateRight;
    public string horn;

    public string startGame;

    Bike bike;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        InitControls();
    }



    // Checks for control inputs
    void Update()
    {
        if (GameManager.instance.getState().Equals("Play"))
        {
            if (Input.GetKey(moveBackwards))
                bike.MoveBackwards();
            else if (Input.GetKey(moveForward))
                bike.MoveForward();
            else
                bike.StopMovement();
    
            if (Input.GetKey(rotateLeft))
                bike.RotateCounterclockwise();
            else if (Input.GetKey(rotateRight))
                bike.RotateClockwise();
        }
        else if (GameManager.instance.getState().Equals("ReadyToStart"))
        {
            if (Input.GetKey(startGame))
                GameManager.instance.StartCountdown();
        }
    }


    //Associate controls to the respective players
    public void InitControls()
    {
        bike = GameManager.instance.GetBike();
    }


}