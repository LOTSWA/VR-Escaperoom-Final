using UnityEngine;
using System.Collections.Generic;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance { get; private set; }

    // Define the exact required order of tags here
    private List<string> requiredSequence = new List<string> { "Red", "Green", "Blue", "Yellow" };

    private int currentSequenceIndex = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // This method is called by the Dart script when it hits a goblin
    public void CheckSequence(string goblinTag)
    {
        if (goblinTag == requiredSequence[currentSequenceIndex])
        {
            Debug.Log("Correct shot! Hit the " + goblinTag + " goblin.");
            currentSequenceIndex++;

            if (currentSequenceIndex >= requiredSequence.Count)
            {
                Debug.Log("Sequence Complete! Puzzle solved.");
                // Add win/completion logic here
                HandlePuzzleWin();
            }
        }
        else
        {
            Debug.LogWarning("Incorrect shot! Expected: " + requiredSequence[currentSequenceIndex] +
                             ", but hit: " + goblinTag + ". Resetting sequence.");
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        currentSequenceIndex = 0;
        // Optionally: Trigger an event to respawn goblins or give a penalty
    }

    private void HandlePuzzleWin()
    {
        // Add code to open a door, spawn an item, or move to the next level
    }
}