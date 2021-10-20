using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

{
    public Transform player;

    float XMouse = 0;

    float YMouse = 0;

    float Sensitivity = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        XMouse += Input.GetAxis("Mouse X") * Sensitivity;
        YMouse -= Input.GetAxis("Mouse Y") * Sensitivity;

        YMouse = Mathf.Clamp(YMouse, -90f, 90f);

        transform.localEulerAngles = new Vector3(YMouse, 0, 0);
        player.localEulerAngles = new Vector3(0, XMouse, 0);
    }
}
