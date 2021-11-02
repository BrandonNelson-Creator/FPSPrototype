using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baropen : MonoBehaviour
{
    [SerializeField] private Animator myAnimation;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimation.SetBool("barOpen", true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimation.SetBool("barOpen", false);

        }
    }
}
