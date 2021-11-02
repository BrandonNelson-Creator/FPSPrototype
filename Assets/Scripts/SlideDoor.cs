using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedDoor : MonoBehaviour
{

    [SerializeField] private Animator myAnimation;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimation.SetBool("SlideOpen", true);
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimation.SetBool("SlideClose", false);
           
        }


    }
} 

