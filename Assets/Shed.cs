using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shed : MonoBehaviour
{
    [SerializeField] private Animator myAnimation;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimation.SetBool("Shed", true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimation.SetBool("Shed", false);

        }
    }
}