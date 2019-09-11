using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(WheelJoint2D))]
public class Bike : MonoBehaviour
{
    [SerializeField] float bikeRotationSpeed;
    [SerializeField] float torsoRotationSpeed;
    [SerializeField] float movementSpeed;
    float normalMovementSpeed;

    [Space(10)]
    [Header("Componentes necessários")]
    [SerializeField]
    HingeJoint2D backWheel;
    [SerializeField] Rigidbody2D bikeBody_rigidbody2D;

    [SerializeField] Rigidbody2D torso_rigidbody2D; 


    [SerializeField] Rigidbody2D backWheelBody2D;
    [SerializeField] Rigidbody2D frontWheelBody2D;
    [SerializeField] Rigidbody2D swingArmBody2D;


    [SerializeField] ParticleSystem backWheelParticles;
    [SerializeField] ParticleSystem frontWheelParticles;

    [SerializeField] ParticleSystem bikeExhaustParticles;

    JointMotor2D backWheel_motor;


    public int playerNumber;

    [SerializeField] float gravity;


    int levelLayerMask = 1 << 17; //Layer 17: Level
    [SerializeField] Color normalExhaustColor;
    [SerializeField] Color boostExhaustColor;

    enum Direction { FORWARD = 1, BACKWARDS = -1 };
    enum Rotation { CLOCKWISE = -1, COUNTER_CLOCKWISE = 1 };

    void Awake()
    {
        backWheel_motor = backWheel.motor;
        if (SplitScreenGameManager.instance == null)
            GameManager.instance.SetBike(this);
    }
    void Start()
    {
        normalMovementSpeed = movementSpeed;
    }


    void LateUpdate()
    {
        CheckBikeMovement();
    }




    /* Acoes da mota */
    public void StopMovement()
    {
        StopBackWheelMovement();
    }
    public void MoveForward()
    {
        bikeExhaustParticles.Play();
        BackWheelMovement(Direction.FORWARD);
    }
    public void MoveBackwards()
    {
        if (bikeBody_rigidbody2D.velocity.x > 0.7f)
        {
            if (frontWheelBody2D.IsTouchingLayers(levelLayerMask) || backWheelBody2D.IsTouchingLayers(levelLayerMask))
            {
                frontWheelBody2D.freezeRotation = true;
                bikeBody_rigidbody2D.velocity = new Vector2(bikeBody_rigidbody2D.velocity.x * 0.8f, bikeBody_rigidbody2D.velocity.y);
                if (frontWheelBody2D.IsTouchingLayers(levelLayerMask))
                    frontWheelParticles.Play();
                if (backWheelBody2D.IsTouchingLayers(levelLayerMask))
                    backWheelParticles.Play();
            }
        }
        else
        {
            if (frontWheelParticles.isPlaying)
                frontWheelParticles.Stop();
            if (backWheelParticles.isPlaying)
                backWheelParticles.Stop();

            frontWheelBody2D.freezeRotation = false;
            BackWheelMovement(Direction.BACKWARDS);
        }

    }
    public void RotateClockwise()
    {
        BikeRotation(Rotation.CLOCKWISE);
    }
    public void RotateCounterclockwise()
    {
        BikeRotation(Rotation.COUNTER_CLOCKWISE);
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
    public Vector2 GetPosition()
    {
        return bikeBody_rigidbody2D.transform.position;
    }

    /*Controla o movimento da roda traseira da mota
     * Argumento: dir -> dita direção do movimento da mota (frente ou para trás) 
     */
    void BackWheelMovement(Direction dir)
    {
        if (dir == Direction.FORWARD && bikeBody_rigidbody2D.velocity.x < -3)
            backWheel_motor.motorSpeed = backWheel_motor.motorSpeed * 0.8f;
        else if (dir == Direction.FORWARD && bikeBody_rigidbody2D.velocity.x < 0)
            backWheel_motor.motorSpeed = backWheel_motor.motorSpeed * 0.4f;
        else if (dir == Direction.FORWARD && bikeBody_rigidbody2D.velocity.x < 3f)
            backWheel_motor.motorSpeed = movementSpeed * (int)dir * 0.3f;
        else if (dir == Direction.FORWARD && bikeBody_rigidbody2D.velocity.x < 10f)
            backWheel_motor.motorSpeed = movementSpeed * (int)dir * bikeBody_rigidbody2D.velocity.x * 0.1f;
        else
            backWheel_motor.motorSpeed = movementSpeed * (int)dir;

        backWheel.useMotor = true;
        backWheel.motor = backWheel_motor;
    }

    /*Desliga o motor da roda traseira impedindo que fique sempre a acelerar*/
    void StopBackWheelMovement()
    {
        backWheel.useMotor = false;
    }

    /* Aplica torque na mota para a fazer rodar
     * Argumentos: rot -> dita o sentido da rotacao (horário ou anti-horário)
     */
    void BikeRotation(Rotation rot)
    {
        bikeBody_rigidbody2D.AddTorque((int)rot * bikeRotationSpeed * Time.deltaTime);
        torso_rigidbody2D.AddTorque((int)rot * torsoRotationSpeed * Time.deltaTime);
    }

    public void StopBikeInstantly()
    {
        StopBackWheelMovement();
        bikeBody_rigidbody2D.velocity = Vector2.zero;
        bikeBody_rigidbody2D.angularVelocity = 0.0f;

        backWheelBody2D.velocity = Vector2.zero;
        backWheelBody2D.angularVelocity = 0.0f;

        frontWheelBody2D.velocity = Vector2.zero;
        frontWheelBody2D.angularVelocity = 0.0f;

        swingArmBody2D.velocity = Vector2.zero;
        swingArmBody2D.angularVelocity = 0.0f;
    }


    //Fixes particles and front wheel freeze
    void CheckBikeMovement()
    {
        if (bikeExhaustParticles.isPlaying)
            bikeExhaustParticles.Stop();

        if (frontWheelParticles.isPlaying)
            frontWheelParticles.Stop();
        if (backWheelParticles.isPlaying)
            backWheelParticles.Stop();

        if (bikeBody_rigidbody2D.velocity.x > 0)
            frontWheelBody2D.freezeRotation = false;
    }

    //Activates Speed boost powerup
    public void PowerupSpeedBoost(float durationTime, float speedBoostVal)
    {
        SetMovementSpeed(movementSpeed + speedBoostVal);

        ParticleSystem.MainModule settings = bikeExhaustParticles.main;
        settings.startColor = boostExhaustColor;

        StartCoroutine(StopPowerupSpeedBoost(durationTime));
    }
    
    IEnumerator StopPowerupSpeedBoost(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);
        SetMovementSpeed(normalMovementSpeed);

        ParticleSystem.MainModule settings = bikeExhaustParticles.main;
        settings.startColor = normalExhaustColor;
    }

    public float GetNormalSpeed()
    {
        return normalMovementSpeed;
    }
    
    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public void SetNormalGravity()
    {
        bikeBody_rigidbody2D.gravityScale = 1;
        backWheelBody2D.gravityScale = 1;
         torso_rigidbody2D.gravityScale = 1;
         backWheelBody2D.gravityScale = 1;
         frontWheelBody2D.gravityScale = 1;
         swingArmBody2D.gravityScale = 1;
        
    }

    public void SetLowGravity()
    {
        bikeBody_rigidbody2D.gravityScale = gravity;
        backWheelBody2D.gravityScale = gravity;
        torso_rigidbody2D.gravityScale = gravity;
        backWheelBody2D.gravityScale = gravity;
        frontWheelBody2D.gravityScale = gravity;
        swingArmBody2D.gravityScale = gravity;
    }
    
}
