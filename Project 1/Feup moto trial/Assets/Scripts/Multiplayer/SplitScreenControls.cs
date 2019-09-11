using UnityEngine;

public class SplitScreenControls : MonoBehaviour
{
    [Header("Botões Jogador 1")]
    public string moveForward_1;
    public string moveBackwards_1;
    public string rotateLeft_1;
    public string rotateRight_1;

    [Space(10)]
    [Header("Botões Jogador 2")]
    public string moveForward_2;
    public string moveBackwards_2;
    public string rotateLeft_2;
    public string rotateRight_2;



    Bike bikePlayer1;
    Bike bikePlayer2;



    // Checks for control inputs
    void Update()
    {
        Player1Controls();
        Player2Controls();
    }

    //Handles player 1 controls
    void Player1Controls()
    {
        if (Input.GetKey(moveBackwards_1))
            bikePlayer1.MoveBackwards();
        else if (Input.GetKey(moveForward_1))
            bikePlayer1.MoveForward();
        else
            bikePlayer1.StopMovement();

        if (Input.GetKey(rotateLeft_1))
            bikePlayer1.RotateCounterclockwise();
        else if (Input.GetKey(rotateRight_1))
            bikePlayer1.RotateClockwise();
    }

    //Handles player 2 controls
    void Player2Controls()
    {
        if (Input.GetKey(moveBackwards_2))
            bikePlayer2.MoveBackwards();
        else if (Input.GetKey(moveForward_2))
            bikePlayer2.MoveForward();
        else
            bikePlayer2.StopMovement();

        if (Input.GetKey(rotateLeft_2))
            bikePlayer2.RotateCounterclockwise();
        else if (Input.GetKey(rotateRight_2))
            bikePlayer2.RotateClockwise();
    }



    public void SetPlayer1(Bike bike)
    {
        bikePlayer1 = bike;
    }
    public void SetPlayer2(Bike bike)
    {
        bikePlayer2 = bike;
    }

}