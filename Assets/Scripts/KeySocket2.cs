using UnityEngine;

public class KeySocket2 : MonoBehaviour
{
    [Tooltip("The specific Unity Tag this socket accepts (e.g., BlackKey)")]
    public string correctKeyTag;

    // An event that notifies the GameManager when a valid action happens
    public delegate void KeyInsertAction(string keyType, bool isInserted);
    public static event KeyInsertAction OnKeyStatusChanged;

    private bool isKeyInserted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isKeyInserted) return;

        if (other.CompareTag(correctKeyTag))
        {
            isKeyInserted = true;
            Debug.Log(correctKeyTag + " socket filled.");
            HandleKeyPhysics(other.gameObject, true);

            // Notify the GameManager that a correct key was inserted
            OnKeyStatusChanged?.Invoke(correctKeyTag, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isKeyInserted && other.CompareTag(correctKeyTag))
        {
            isKeyInserted = false;
            Debug.Log(correctKeyTag + " socket emptied.");
            HandleKeyPhysics(other.gameObject, false);

            // Notify the GameManager that a correct key was removed
            OnKeyStatusChanged?.Invoke(correctKeyTag, false);
        }
    }

    private void HandleKeyPhysics(GameObject key, bool snapInPlace)
    {
        Rigidbody rb = key.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = snapInPlace; // Make key static when in socket
        }

        if (snapInPlace)
        {
            key.transform.position = transform.position;
            key.transform.rotation = transform.rotation;
        }
    }
}
