using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [Header("Door Object")]
    [SerializeField] private Animator myDoor = null;

    [Header("Trigger Type")]
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [Header("Animation Name")]
    [SerializeField] private string doorOpen = "Door-Open";
    [SerializeField] private string doorClose = "Door-Close";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play(doorOpen, 0, 0.0f);
                gameObject.SetActive(true);
            }

            else if (closeTrigger)
            {
                myDoor.Play(doorClose, 0, 0.0f);
                gameObject.SetActive(true);
            }
        }
    }
}
