using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneOn : MonoBehaviour
{
    public GameObject mainPlayer;
    public GameObject sceneCam;
    public GameObject npc;

    private void OnTriggerEnter(Collider other)
    {

        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        sceneCam.SetActive(true);
        mainPlayer.SetActive(false);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(6);
        mainPlayer.SetActive(true);
        sceneCam.SetActive(false);
        npc.SetActive(true);
    }
    


}
