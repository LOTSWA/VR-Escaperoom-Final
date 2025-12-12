using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections.Generic;

public class SequenceController : MonoBehaviour
{
    // Define the correct sequence of tags
    private List<string> correctSequence = new List<string> { "Red", "Green", "Blue", "Yellow" };
    private int sequenceIndex = 0; // Tracks the current required trigger in the sequence

    // Reference to the player/object that will trigger the events
    public GameObject playerObject;

    void Start()
    {
        if (playerObject == null)
        {
            Debug.LogError("Player object not assigned! Please assign the player in the Inspector.");
        }
        Debug.Log("Sequence required: Red, Green, Blue, Yellow");
    }

    // This method is called by the individual trigger scripts
    public void CheckTriggerOrder(string triggeredTag)
    {
        // Check if the triggered tag matches the next expected tag in the sequence
        if (triggeredTag == correctSequence[sequenceIndex])
        {
            Debug.Log("Correct trigger! Tag: " + triggeredTag);
            sequenceIndex++;

            // Check if the entire sequence is complete
            if (sequenceIndex >= correctSequence.Count)
            {
                Debug.Log("Sequence Complete! Well done!");
                // Add your success logic here (e.g., load next level, open door)
                HandleSequenceSuccess();
            }
        }
        else
        {
            Debug.LogWarning("Wrong trigger! Expected: " + correctSequence[sequenceIndex] + ", Got: " + triggeredTag);
            // Reset the sequence if the order is wrong
            ResetSequence();
        }
    }

    private void ResetSequence()
    {
        sequenceIndex = 0;
        Debug.Log("Sequence reset. Start from Red again.");
        // Add your failure logic here (e.g., play sound, show message)
    }

    private void HandleSequenceSuccess()
    {
        // Example success action
        Debug.Log("You have completed the sequence!");
        // You can disable triggers, enable new objects, etc.
    }
}