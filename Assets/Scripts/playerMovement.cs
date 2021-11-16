using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundDistance;
    Vector3 velocity;
    public bool isGrounded;
    // Update is called once per frame
    public void Update()
    {
        //if (Physics.Raycast(transform.position, Vector3.down, jumpHeight))
        //{
        //    isGrounded = true;
        //}
        //else
        //{
        //    isGrounded = false;
        //}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //rb.AddForce(new Vector3(0, jumpHeight));
           
            
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    //ALTERNATE JUMP CHECK
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
