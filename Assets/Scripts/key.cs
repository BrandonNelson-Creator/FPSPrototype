using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class key : MonoBehaviour
{
    public GameObject door;
    public GameObject Key;
    

    private void OnTriggerEnter(Collider other)
    {
       if (gameObject.CompareTag("Player"))
        {
            Key.SetActive(false);
            door.SetActive(true);


        } 

    }


}
