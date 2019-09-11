using System.Collections;
using UnityEngine;


public class PlayerCollision : MonoBehaviour
{
    [SerializeField] HingeJoint2D handJoint;
    [SerializeField] HingeJoint2D footJoint;
    [SerializeField] SpringJoint2D footSpringJoint;


    private bool enabledCollision = true;
    
    //This function runs when we hit another object.
    public void CollisionFromChild(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle" && enabledCollision)
        {
            handJoint.enabled = false;
            footJoint.enabled = false;
            footSpringJoint.enabled = false;
            enabledCollision = false;
            
            if (SplitScreenGameManager.instance == null)
                GameManager.instance.SetGameState(GameManager.State.Over);
            BikeSounds.instance.StopAccelerationSound();
            StartCoroutine(WaitFor(1.5f));
        }
    }

    IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (SplitScreenGameManager.instance != null)
        {
            if (GetComponentInParent<Bike>().playerNumber == 1)
                SplitScreenGameManager.instance.RetryPlayer1();
            else if (GetComponentInParent<Bike>().playerNumber == 2)
                SplitScreenGameManager.instance.RetryPlayer2();
            else Debug.LogError("Error retrying - Multiplayer");
        }
        else
        {
            GameManager.instance.ReduceTries();   
        
            if (GameManager.instance.GetTries() >= 1)
                enabledCollision = true;
        }
    }
}