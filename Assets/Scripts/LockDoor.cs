using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockDoor : MonoBehaviour
{
    public GameObject Key;
    public GameObject Lock;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Key"))
            {
                gameObject.SetActive(false);

            }

            if (gameObject.CompareTag("Lock"))
            {
                gameObject.SetActive(false);
            }


            

            
        }
    }

   
}
