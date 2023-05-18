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
        if (other.tag == "bullet")
        {

            Debug.Log("Collides Collides Collides");
            // Get a reference to the JetplaneShooting component attached to the player object
            JetplaneShooting jetplaneShooting = this.GetComponentInChildren<JetplaneShooting>();
            if (jetplaneShooting != null)
            {
                // Call the ActivatePowerup function on the JetplaneShooting component
                Debug.Log("Triggered");
                jetplaneShooting.ActivatePowerup();

            }
            else
            {
                Debug.LogWarning("JetplaneShooting component not found on the player object.");
            }

            // Destroy this power-up item
            //Destroy(gameObject);
        }
    }

}
