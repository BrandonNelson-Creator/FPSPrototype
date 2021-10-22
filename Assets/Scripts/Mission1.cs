using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1 : MonoBehaviour
{
    public GameObject customText;
    public GameObject mission;
    public GameObject door;
    public GameObject shedOpen;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customText.SetActive(true);
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customText.SetActive(false);
            mission.SetActive(true);
            door.SetActive(true);
            shedOpen.SetActive(false);


        }
    }
}
