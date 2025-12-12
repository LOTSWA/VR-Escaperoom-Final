using UnityEngine;

public class ColorTrigger : MonoBehaviour
{
    private SequenceController sequenceController;
    public string assignedTag; // Set this in the Inspector to match the GameObject's Tag

    void Start()
    {
        // Find and store a reference to the main SequenceController script
        sequenceController = FindObjectOfType<SequenceController>();
        if (sequenceController == null)
        {
            Debug.LogError("SequenceController not found in the scene!");
        }

        // Ensure the assignedTag variable matches the GameObject's actual tag
        assignedTag = gameObject.tag;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        // You might want to use a specific "Player" tag here instead of checking the playerObject reference
        if (other.gameObject == sequenceController.playerObject)
        {
            // Call the main controller's method, passing this trigger's tag
            sequenceController.CheckTriggerOrder(assignedTag);
        }
    }
}