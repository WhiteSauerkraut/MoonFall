using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 火箭启动发射脚本
/// </summary>
public class RocketStart : MonoBehaviour
{
    // 启动时间
    public float launch_time = 10f;
    // 推射器脱离时间
    public float drop_time = 10f;
    // 飞行时间
    public float fly_time = 20f;
    // 发射速度
    public float speed = 10;
    // 加速后速度
    public float second_speed = 40;
    // 发射状态
    public bool isLaunch = false;
    // 推射器
    GameObject rocket_Launcher;
    // 喷气粒子特效
    GameObject fog_paticle;
    // 音源
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        rocket_Launcher = transform.Find("rocket_Launcher").gameObject;
        aud = GetComponent<AudioSource>();
        fog_paticle = GameObject.Find("fog_paticle");

        StartCoroutine(RocketBooter());
    }

    // Update is called once per frame
    void Update()
    {
        if(isLaunch)
        {
            RocketLiftUp();
        }
    }

    /**
     * 火箭发射过程
     * */
    IEnumerator RocketBooter()
    {
        StartCoroutine(StartFogEffect());
        // 准备发射
        aud.clip = Resources.Load<AudioClip>("Sounds/" + "rocket_gas_flow") as AudioClip;
        if (aud.isPlaying == false)
        {
            aud.Play();
        }
        yield return new WaitForSeconds(launch_time);

        // 开始发射
        StartFireEffect();
        isLaunch = true;
        aud.clip = Resources.Load<AudioClip>("Sounds/" + "rocket_fly") as AudioClip;
        aud.time = 1f;
        if (aud.isPlaying == false)
        {
            aud.Play();
        }
        yield return new WaitForSeconds(4f);
        
        // 推射器脱落
        yield return new WaitForSeconds(drop_time);
        DropLauncher();

        yield return new WaitForSeconds(fly_time);
        // 场景跳转到太空
        StartCoroutine(LoadScene());
    }

    /**
     * 火箭升空
     * */
    void RocketLiftUp()
    {
        transform.Translate(new Vector3(0f, speed, 0f) * Time.deltaTime, Space.World);
    }

    /**
     * 推射器脱落
     * */
    void DropLauncher()
    {
        rocket_Launcher.transform.parent = null;
        rocket_Launcher.GetComponent<Rigidbody>().useGravity = true;
        rocket_Launcher.GetComponent<Rigidbody>().isKinematic = false;
        foreach (Transform child in rocket_Launcher.transform)
        {
            child.gameObject.SetActive(false);
        }

        // 加速
        speed = second_speed;
    }

    /**
     * 初始化烟雾粒子系统
     * */
    IEnumerator StartFogEffect()
    {
        foreach(Transform child in fog_paticle.transform)
        {
            child.gameObject.SetActive(true);
            Destroy(child.gameObject, 15f);
            yield return new WaitForSeconds(1f);
        }
    }

    /**
     * 初始化火焰粒子系统
     * */
    void StartFireEffect()
    {
        GameObject[] fireSets = GameObject.FindGameObjectsWithTag("FireSet");
        foreach(GameObject fireset in fireSets)
        {
            fireset.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    /**
     * 切换场景
     * */
    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("UniverseScene");
        yield return async;
    }
}
