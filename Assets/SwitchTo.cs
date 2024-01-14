using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchTo : MonoBehaviour
{
    // Start is called before the first frame update
    public void SwitchToScene(string scene)
    {
        SceneManager.LoadScene(scene); 
    }
}
