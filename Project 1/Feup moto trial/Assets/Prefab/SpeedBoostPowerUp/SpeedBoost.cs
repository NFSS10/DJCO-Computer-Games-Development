using UnityEngine;

public class SpeedBoost : MonoBehaviour {

    [SerializeField] float powerDuration;
    [SerializeField] float speedBoostVal;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Motorbike"))
        {
            GameManager.instance.SpeedBoostPowerUp(powerDuration, speedBoostVal);
            Destroy(this.gameObject);
        }
    }
}
