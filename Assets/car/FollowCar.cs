using UnityEngine;
using System.Collections;
public class FollowCar : MonoBehaviour
{
    public GameObject Car;  				//声明游戏对象变量，用于获取汽车对象
    private float y;        					//声明float类型变量，用于设置摄像机y轴坐标
    private float z;        					//声明float类型变量，用于设置摄像机z轴坐标
    void Awake()
    {
        z = this.transform.position.z;  			//获取车辆的z轴坐标并赋值给变量z
        y = this.transform.position.y;  			//获取车辆的x轴坐标并赋值给变量y
    }
    void FixedUpdate()
    {
        this.transform.position = new Vector3(Car.transform.position.x, y, z); 			//通过三维向量来实时更新位置
    }
}

