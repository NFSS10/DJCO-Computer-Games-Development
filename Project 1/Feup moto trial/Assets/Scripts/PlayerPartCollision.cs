using UnityEngine;

public class PlayerPartCollision : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {

        // Get parent script
        PlayerCollision parentScript = transform.parent.GetComponent<PlayerCollision>();

        // Let it know a collision happened
        parentScript.CollisionFromChild(collision);

    }
}