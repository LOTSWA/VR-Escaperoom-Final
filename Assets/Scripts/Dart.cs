using UnityEngine;

public class Dart : MonoBehaviour
{
    // Use OnTriggerEnter if the dart collider is set to "Is Trigger"
    private void OnTriggerEnter(Collider other)
    {
        ProcessHit(other);
    }

    // Use OnCollisionEnter if the dart collider is NOT set to "Is Trigger" (physical collision)
    /*
    private void OnCollisionEnter(Collision collision)
    {
        ProcessHit(collision.collider);
    }
    */

    private void ProcessHit(Collider other)
    {
        // Check if the collided object has a relevant tag
        if (other.CompareTag("Red") || other.CompareTag("Green") ||
            other.CompareTag("Blue") || other.CompareTag("Yellow"))
        {
            // Pass the tag information to the central manager
            SequenceManager.Instance.CheckSequence(other.tag);

            // Destroy the dart upon impact
            Destroy(gameObject);
        }
        else if (other.CompareTag("Environment")) // Example for hitting walls
        {
            Destroy(gameObject, 5f); // Destroy after a delay if it hits something else
        }
    }
}