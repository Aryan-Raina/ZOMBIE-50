using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Rigidbody2D rb;
    private float leftLimit = -31f;
    private float rightLimit = 31.89035f;
    private float bottomLimit = -21f;
    private float upLimit = 21.74012f;
    private Vector2 camBound;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camPos = (rb.position);
        camPos.x = Mathf.Clamp(camPos.x, leftLimit, rightLimit);
        camPos.y = Mathf.Clamp(camPos.y, bottomLimit,  upLimit);

        camPos.z = -10;
        transform.position = camPos;
       
    }
}
