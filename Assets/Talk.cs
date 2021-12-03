using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{
    public GameObject canvis;

    public void OnTriggerEnter(Collider other)
    {
        canvis.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        canvis.SetActive(false);
    }
}
    
    
       
    

