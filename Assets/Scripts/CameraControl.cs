using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public float mouseSens;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX < 0.0)
        {
            transform.Rotate(0.0f, -1.0f*mouseSens, 0.0f);
        }

        if (mouseX > 0.0)
        {
            transform.Rotate(0.0f, 1.0f*mouseSens, 0.0f);
        }

    }
}
