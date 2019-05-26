using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 飞船交互控制
/// </summary>
public class ShipInteract : MonoBehaviour
{
    // 围绕的物体
    public Transform aroundPoint;
    // 角速度
    public float angularSpeed = 5;
    // 半径
    public float aroundRadius;
    // 第一圈绕月半径
    public float aroundRadius1 = 850;
    // 第二圈绕月半径
    public float aroundRadius2 = 700;
    // 第三圈绕月半径
    public float aroundRadius3 = 550;
    // 围绕时间
    public float rotate_time = 10f;

    private float angled;
    // 绕月标志
    private bool isRotate = false;
    // 设置旋转角度标志位（只设置一次）
    private bool isSetRotate = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isRotate)
        {
            // 累加已经转过的角度
            angled += (angularSpeed * Time.deltaTime) % 360;
            // 计算x位置
            float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
            // 计算y位置
            float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);
            // 更新位置
            transform.position = new Vector3(-posX, 0, -posZ) + aroundPoint.position;
            transform.Rotate(new Vector3(0, angularSpeed * Time.deltaTime, 0));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Moon") && !isSetRotate)
        {
            isSetRotate = true;
            StartCoroutine(Rotate_Moon());
            GetComponent<ShipControl>().enabled = false;
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, -90, transform.rotation.z));
        }
    }

    /**
     * 绕月过程：两次变轨
     * */
    IEnumerator Rotate_Moon()
    {
        yield return null;

        aroundRadius = aroundRadius1;
        isRotate = true;
        yield return new WaitForSeconds(rotate_time);

        isRotate = false;
        aroundRadius = aroundRadius2;
        StartCoroutine(Translate_radius());
        yield return new WaitUntil(Get_isRotate);
        yield return new WaitForSeconds(rotate_time);

        isRotate = false;
        
        aroundRadius = aroundRadius3;
        StartCoroutine(Translate_radius());
        yield return new WaitUntil(Get_isRotate);
        yield return new WaitForSeconds(rotate_time);

        StartCoroutine(LoadScene());
    }

    /**
     * 切换场景
     * */
    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("MoonScene");
        yield return async; 
    }

    /**
     * 变轨
     * */
    IEnumerator Translate_radius()
    {
        // 累加已经转过的角度
        angled += (angularSpeed * Time.deltaTime) % 360;
        // 计算x位置
        float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
        // 计算y位置
        float posZ = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);
        // 计算位置
        Vector3 target_position = new Vector3(-posX, 0, -posZ) + aroundPoint.position;
        while (transform.position.x <= target_position.x || transform.position.y <= target_position.y || transform.position.z <= target_position.z)
        {
            yield return null;
            if (transform.position.x <= target_position.x)
            {
                transform.Translate(new Vector3(1, 0, 0), Space.World);
            }
            if (transform.position.y <= target_position.y)
            {
                transform.Translate(new Vector3(0, 1, 0), Space.World);
            }
            if (transform.position.z <= target_position.z)
            {
                transform.Translate(new Vector3(0, 0, 1), Space.World);

            }
        }
        yield return null;
        isRotate = true;
    }

    /**
     * 返回变轨值
     * */
    bool Get_isRotate()
    {
        return isRotate;
    }
}
