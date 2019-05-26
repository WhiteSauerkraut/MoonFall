using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞船控制
/// </summary>
public class ShipControl : MonoBehaviour
{
    public float moveSpeed = 100;
    public float rotateSpeed = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");

        // 方向键控制旋转
        if (V != 0 || H != 0)
        {
            transform.Rotate(new Vector3(-V, H, 0) * Time.deltaTime * rotateSpeed, Space.Self);
            
        }
        // 空格键控制飞行
        if(Input.GetKey(KeyCode.Space))
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed, Space.Self);
        }
    }
}
