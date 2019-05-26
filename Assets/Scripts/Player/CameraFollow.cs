using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 控制摄像机注视人物
 * */

public class CameraFollow : MonoBehaviour
{
    public float mouseSpeedX = 10;
    public float mouseSpeedY = -10;
    public float maxAngleY = 50;
    public float minAngleY = -20;
    public float distance = 5;
    
    float x = 0;
    float y = 0;

    void LateUpdate()
    {
        Camera.main.transform.LookAt(this.transform);

        x += Input.GetAxis("Mouse X") * mouseSpeedX * Time.deltaTime;
        y += Input.GetAxis("Mouse Y") * mouseSpeedY * Time.deltaTime;
        if (y >= maxAngleY)
            y = maxAngleY;
        else if (y <= minAngleY)
            y = minAngleY;
        Quaternion q = Quaternion.Euler(y, x, 0);
        Vector3 direction = q * Vector3.forward;

        this.transform.position = this.transform.parent.position - direction * distance;
        this.transform.LookAt(this.transform.parent);
    }
}
