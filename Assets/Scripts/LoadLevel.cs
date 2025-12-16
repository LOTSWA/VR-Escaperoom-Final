using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
  public void LoadLevel1()
    {
        SceneManager.LoadScene("Area 1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Area 2");
    }

}
