using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
    public gunPickup gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunComtainer, MainCamera;

    public float pickUpRange;
    public float dropForwardforce, dropUpwardForce;

    public bool equipped;
    public bool slotFull;

    private void Start()
    {
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }
    private void Update()
    {
        //Check for range and keypress
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) Pickup();

        //Drop amd equip if "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void Pickup()
    {
        equipped = true;
        slotFull = true;
        //make rb kinematic and mesh collider true

        //weapon is child of camera
        transform.SetParent(gunComtainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;
        //enable script
        gunScript.enabled = true;

    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;
        //set parent to null
        transform.SetParent(null);
        //make rb kinematic and mesh collider true
        rb.isKinematic = false;
        coll.isTrigger = false;
        //Gun carries momentum
        rb.velocity = player.GetComponent<Rigidbody>().velocity;
        //Addforce
        rb.AddForce(MainCamera.forward * dropForwardforce, ForceMode.Impulse);
        rb.AddForce(MainCamera.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //enable script
        gunScript.enabled = false;

    }
}
