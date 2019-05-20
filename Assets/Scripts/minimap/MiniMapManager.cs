using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 动态绘制小地图
 * */

public class MiniMapManager : MonoBehaviour
{
    // 小地形图  
    public Texture map1;
    // 标识角色的图片   
    public Texture jueseTexture; 

    float juesePosX = 0;
    float juesePosY = 0;

    // 角色  
    public GameObject player;
    // 车辆
    public GameObject car;
    // 交互控制
    private Interact interact;
    // 地形  
    public GameObject plane;
    // 地形的宽  
    float planeWidth;
    // 地形的高   
    float planeHeight; 

    float angle = 0; //人物旋转的角度  

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        car = GameObject.FindGameObjectWithTag("Car");
        interact = player.GetComponent<Interact>();
        // 获取地形的宽高    
        planeWidth = plane.GetComponent<MeshFilter>().mesh.bounds.size.x * plane.transform.localScale.x;
        planeHeight = plane.GetComponent<MeshFilter>().mesh.bounds.size.z * plane.transform.localScale.z;
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - map1.width, 0, map1.width, map1.height), map1);

        GUIUtility.RotateAroundPivot(angle, new Vector2((Screen.width - map1.width) + juesePosX + 5, juesePosY + 5));

        GUI.DrawTexture(new Rect((Screen.width - map1.width) + juesePosX, juesePosY, 10, 10), jueseTexture);
    }


    void Update()
    {
        // 根据对象在plane的比例关系，映射到对应地图位置
        if (interact.isInCar)
        {
            juesePosX = map1.width * car.transform.position.x / planeWidth + map1.width / 2;
            juesePosY = map1.height * (-car.transform.position.z) / planeHeight + map1.height / 2;
            angle = car.transform.eulerAngles.y - 90;
        }
        else
        {
            juesePosX = map1.width * player.transform.position.x / planeWidth + map1.width / 2;
            juesePosY = map1.height * (-player.transform.position.z) / planeHeight + map1.height / 2;
            angle = player.transform.eulerAngles.y - 90;
        }
    }
}
