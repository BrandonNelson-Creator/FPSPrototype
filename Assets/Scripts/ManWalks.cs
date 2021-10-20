using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManWalks : MonoBehaviour
{
    public GameObject text;
    public GameObject animate;
    public GameObject npc;

    private void OnTriggerEnter(Collider other)
    {

        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        animate.SetActive(true);
        text.SetActive(false);
        npc.SetActive(false);
        StartCoroutine(talk());
        StartCoroutine(NPC());
    }

    IEnumerator talk()
    {
        yield return new WaitForSeconds(2);
        text.SetActive(true);
        
    }
    IEnumerator NPC()
    {
        yield return new WaitForSeconds(3);
        animate.SetActive(false);
        npc.SetActive(true);

    }
    


}
