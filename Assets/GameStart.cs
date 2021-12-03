using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
  
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
            Debug.Log("CHANGE");

        }

    }

    //IEnumerator FinishCut()
    //{
        //yield return new WaitForSeconds(15);
        //SceneManager.LoadScene(2);


        
    //}
}
