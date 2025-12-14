using UnityEngine;
using System.Collections.Generic;

public class KeySequenceManager : MonoBehaviour
{
    // A dictionary to track which required keys are currently inserted (false = missing, true = inserted)
    private Dictionary<string, bool> keyStatus = new Dictionary<string, bool>()
    {
        { "BlackKey", false },
        { "RedKey", false },
        { "GreenKey", false }
    };

    void OnEnable()
    {
        // Subscribe to the event fired by the individual KeySocket scripts
        KeySocket.OnKeyStatusChanged += UpdateKeyStatus;
    }

    void OnDisable()
    {
        // Unsubscribe from the event when this manager is disabled or destroyed
        KeySocket.OnKeyStatusChanged -= UpdateKeyStatus;
    }

    private void UpdateKeyStatus(string keyType, bool isInserted)
    {
        if (keyStatus.ContainsKey(keyType))
        {
            keyStatus[keyType] = isInserted;
            CheckAllKeysInserted();
        }
    }

    private void CheckAllKeysInserted()
    {
        bool allPresent = true;
        foreach (bool status in keyStatus.Values)
        {
            if (!status)
            {
                allPresent = false; // As soon as one is missing, break and mark as incomplete
                break;
            }
        }

        if (allPresent)
        {
            Debug.Log("--- PUZZLE SOLVED: ALL 3 KEYS INSERTED CORRECTLY! ---");
            UnlockDoor(); // Call your winning function
        }
        else
        {
            Debug.Log("Not all keys are in place yet.");
        }
    }

    private void UnlockDoor()
    {
        // Add code to open the door, activate a platform, etc.
        // Example: GameObject.Find("Door").SetActive(false);
    }
}
