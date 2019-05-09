using UnityEngine;
using System.Collections;
public class WheelRotate : MonoBehaviour
{
    public GameObject wheel;            //声明游戏对象变量，用于获取挂有车轮碰撞器的对象
    private float wheelAngle;           	//声明float类型的变量，用于设置车轮旋转角
    private WheelCollider wheelCollider;   							//声明车轮碰撞器变量
    void Awake()
    {
        wheelCollider = wheel.transform.GetComponent<WheelCollider>();  //获取车轮碰撞器
    }
    void Update()
    {
        this.transform.rotation = wheelCollider.transform.rotation * Quaternion.Euler(wheelAngle, 0, 0);    //修改当前车轮对象的旋转角度，仅绕x轴旋转
        wheelAngle += wheelCollider.rpm * 360 / 60 * Time.deltaTime;    //计算车轮每秒旋转多少度
    }
}

