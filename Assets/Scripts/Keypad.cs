using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Keypad : MonoBehaviour
{
    public List<int> Password = new List<int>();
    private List<int> Entered = new List<int>();
    [SerializeField] private TextMeshProUGUI codeDisplay;
    [SerializeField] private string successText;
    [Space(5f)]
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectPassword;
    public UnityEvent onIncorrectPassword;

    public bool hasUsedCorrectCode = false;

    [Space(5f)]
    [Header("Cannon Ball Information")]
    public Rigidbody cannonBallRigidbody;
    private bool gravityActivated = false;

    void Start()
    {
        // Find the specific GameObject named "Cannon" in the scene when the game starts
        GameObject cannonBallObject = GameObject.Find("Core");

        if (cannonBallObject != null)
        {
            // Get the Rigidbody component from the Cannon object
            cannonBallRigidbody = cannonBallObject.GetComponent<Rigidbody>();
            if (cannonBallRigidbody == null)
            {
                Debug.LogError("The 'Core' object found but has no Rigidbody component!");
            }
        }
        else
        {
            Debug.LogError("GameObject named 'Core' not found in the scene! Check spelling.");
        }
    }

    public void UserNumberEntry(int selectedNum)
    {
        if(Entered.Count >= 4)
        {
            return;
        }
        Entered.Add(selectedNum);

        updateDisplay();
    }

    public void updateDisplay()
    {
        codeDisplay.text = null;
        for(int i = 0; i < Entered.Count; i++)
        {
            codeDisplay.text += Entered[i];
        }

    }

    public void DeleteEntry()
    {
        if (Entered.Count <= 0)
        {
            return;
        }
        var listposition = Entered.Count - 1;
        Entered.RemoveAt(listposition);
        updateDisplay();
    }

    public void checkEntered()
    {
        for(int i = 0; i < Password.Count; i++)
        {
            if (Entered[i] != Password[i])
            {
                IncorrectPassword();
                return;
            }
        }
        correctPasswordGiven();
    }

    public void IncorrectPassword()
    {
        onIncorrectPassword.Invoke();
        StartCoroutine(ResetKeycode());
    }
    public void correctPasswordGiven()
    {
        if (!hasUsedCorrectCode)
        {
            onCorrectPassword.Invoke();
            hasUsedCorrectCode = true;
            codeDisplay.text = successText;
        }
    }

IEnumerator ResetKeycode()
    {
        yield return new WaitForSeconds(1f);
        Entered.Clear();
        codeDisplay.text = "Enter Code...";
    }

    public void dropDemonCore()
    {
        if (cannonBallRigidbody != null)
        {
            // Enable gravity and disable kinematic status
            cannonBallRigidbody.useGravity = true;
            cannonBallRigidbody.isKinematic = false;
            gravityActivated = true;

            Debug.Log("Password Successful! 'Core' now has gravity enabled.");
        }
    }
}



