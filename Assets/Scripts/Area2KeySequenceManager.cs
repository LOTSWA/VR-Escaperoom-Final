using UnityEngine;
using System.Collections.Generic;

public class Area2KeySequenceManager : MonoBehaviour
{

    public Rigidbody gunpowderRigidbody;
    private bool gravityActivated = false;
    // A dictionary to track which required keys are currently inserted (false = missing, true = inserted)
    private Dictionary<string, bool> keyStatus2 = new Dictionary<string, bool>()
    {
        { "BlackKey", false },
        { "RedKey", false },
        { "GreenKey", false }
    };

    void OnEnable()
    {
        // Subscribe to the event fired by the individual KeySocket scripts
        KeySocket.OnKeyStatusChanged += UpdateKeyStatus2;
    }

    void OnDisable()
    {
        // Unsubscribe from the event when this manager is disabled or destroyed
        KeySocket.OnKeyStatusChanged -= UpdateKeyStatus2;
    }

    private void UpdateKeyStatus2(string keyType, bool isInserted)
    {
        if (keyStatus2.ContainsKey(keyType))
        {
            keyStatus2[keyType] = isInserted;
            CheckAllKeysInserted2();
        }
    }

    private void CheckAllKeysInserted2()
    {
        bool allPresent = true;
        foreach (bool status in keyStatus2.Values)
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
            UnlockDoor2(); // Call your winning function
        }
        else
        {
            Debug.Log("Not all keys are in place yet.");
        }
    }

    private void UnlockDoor2()
    {
        if (gunpowderRigidbody != null)
        {
            // Enable gravity and disable kinematic status
            gunpowderRigidbody.useGravity = true;
            gunpowderRigidbody.isKinematic = false;
            gravityActivated = true;

            Debug.Log("Keys Insterted! 'Barrel' now has gravity enabled.");
        }
    }
}
