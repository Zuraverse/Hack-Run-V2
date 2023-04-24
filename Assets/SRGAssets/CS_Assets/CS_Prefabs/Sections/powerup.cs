using UnityEngine;

public class powerup : MonoBehaviour
{
    public JetplaneShooting jetplaneShooting;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            JetplaneShooting jetplaneShooting = other.gameObject.GetComponent<JetplaneShooting>();
            if (jetplaneShooting != null)
            {
                jetplaneShooting.ActivatePowerup();
            }
            Destroy(gameObject);
        }
    }
}
