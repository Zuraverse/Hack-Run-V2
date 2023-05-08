using UnityEngine;

public class Powerup : MonoBehaviour
{
    public GameObject bulletUIActivator;
    private GameObject UIActivate;

    public void Start(){
      //  UIActivate = GameObject.FindGameObjectWithTag("bulletImageUI");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collides Collides Collides");
            // Get a reference to the JetplaneShooting component attached to the player object
            JetplaneShooting jetplaneShooting = other.GetComponentInChildren<JetplaneShooting>();
            if (jetplaneShooting != null)
            {
                // Call the ActivatePowerup function on the JetplaneShooting component
                jetplaneShooting.ActivatePowerup();

            }
            else
            {
                Debug.LogWarning("JetplaneShooting component not found on the player object.");
            }

            // Destroy this power-up item
            Destroy(gameObject);
        }
    }

}
