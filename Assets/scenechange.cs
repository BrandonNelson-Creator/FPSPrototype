using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechange : MonoBehaviour
{
    public GameObject gameObject;
    public CutsceneOn GetCutscene;

    public void OnEnable()
    {

        SceneManager.LoadScene("Jumpworks", LoadSceneMode.Additive);
    }

}    
