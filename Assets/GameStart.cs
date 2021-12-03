using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
  
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {

        }

        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(2);


        
    }
}
