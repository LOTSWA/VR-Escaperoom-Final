using UnityEngine;

public class CannonGravityActivator : MonoBehaviour
{
    [Tooltip("The tag of the projectile that hits this target (e.g., Throwable)")]
    public string requiredThrowerTag = "Throwable";

    public Rigidbody cannonRigidbody;
    private bool gravityActivated = false;

    void Start()
    {
        // Find the specific GameObject named "Cannon" in the scene when the game starts
        GameObject cannonObject = GameObject.Find("Cannon");

        if (cannonObject != null)
        {
            // Get the Rigidbody component from the Cannon object
            cannonRigidbody = cannonObject.GetComponent<Rigidbody>();
            if (cannonRigidbody == null)
            {
                Debug.LogError("The 'Cannon' object found but has no Rigidbody component!");
            }
        }
        else
        {
            Debug.LogError("GameObject named 'Cannon' not found in the scene! Check spelling.");
        }
    }

    // Use OnTriggerEnter if the Target's collider is set to "Is Trigger"
    private void OnTriggerEnter(Collider other)
    {
        CheckForHit(other.gameObject);
    }

    // Or use OnCollisionEnter for physical collisions
    /*
    private void OnCollisionEnter(Collision collision)
    {
        CheckForHit(collision.gameObject);
    }
    */

    private void CheckForHit(GameObject hittingObject)
    {
        if (gravityActivated) return;

        // Check if the hitting object has the correct tag
        if (hittingObject.CompareTag(requiredThrowerTag))
        {
            if (cannonRigidbody != null)
            {
                // Enable gravity and disable kinematic status
                cannonRigidbody.useGravity = true;
                cannonRigidbody.isKinematic = false;
                gravityActivated = true;

                Debug.Log("Hit detected! 'Cannon' now has gravity enabled.");

                // Optional: Destroy the projectile on impact
                Destroy(hittingObject);
            }
        }
    }
}
