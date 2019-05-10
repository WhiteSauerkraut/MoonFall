using UnityEngine;
using System.Collections;
public class WheelRotate : MonoBehaviour
{
    // 声明游戏对象变量，用于获取挂有车轮碰撞器的对象
    public GameObject wheel;
    // 声明float类型的变量，用于设置车轮旋转角
    private float wheelAngle;
    // 声明车轮碰撞器变量
    private WheelCollider wheelCollider;   		
    
    void Awake()
    {
        wheelCollider = wheel.transform.GetComponent<WheelCollider>();  
    }

    void Update()
    {
        // 修改当前车轮对象的旋转角度，仅绕x轴旋转
        this.transform.rotation = wheelCollider.transform.rotation * Quaternion.Euler(wheelAngle, 0, 0);
        // 计算车轮每秒旋转多少度
        wheelAngle += wheelCollider.rpm * 360 / 60 * Time.deltaTime;    
    }
}

