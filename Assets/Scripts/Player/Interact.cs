﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 人物交互控制
 * */

public class Interact : MonoBehaviour
{
    GameObject chooseObj;
    GameObject player;
    ChooseObj chooseComponent;
    public bool isInCar = false;

    // Start is called before the first frame update
    void Start()
    {
        chooseComponent = GetComponent<ChooseObj>();
        player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        chooseObj = chooseComponent.chooseObj;

        if(chooseObj != null)
        {
            if (chooseObj.tag.Equals("Car"))
            {
                // 按下F键上车,切换控制物体为车辆
                if (Input.GetKeyDown(KeyCode.F) && !isInCar)
                {
                    HidePlayer();
                    ChangeControllToCar();
                    isInCar = true;
                }
                // 按下F键下车,切换控制物体为人物
                else if (Input.GetKeyDown(KeyCode.F) && isInCar)
                {
                    ShowPlayer();
                    ChangeControllToPlayer();
                    isInCar = false;
                }
            }
            // 交流
            else if (chooseObj.tag.Equals("Npc"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    TalkManager talkManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<TalkManager>();
                    talkManager.Show();
                }
            }
            // 启动火箭
            else if (chooseObj.tag.Equals("Launchpad"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    HidePlayer();
                    ChangeControllToRocket();
                }
            }
        }
        
    }

    /**
     * 隐藏人物
     * */
    void HidePlayer()
    {
        foreach (Transform trans in player.transform)
        {
            trans.gameObject.SetActive(false);
        }
    }

    /**
     * 显示人物
     * */
    void ShowPlayer()
    {
        foreach (Transform trans in player.transform)
        {
            trans.gameObject.SetActive(true);
        }
        GameObject set = GameObject.FindGameObjectWithTag("Set");
        player.transform.position = set.transform.position;
    }

    /**
     * 切换控制为车辆
     * */
    void ChangeControllToCar()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<SoundsControl>().enabled = false;
        player.GetComponent<AudioSource>().enabled = false;
        chooseObj.GetComponent<MoveCar>().enabled = true;
        chooseObj.transform.Find("FollowObjs").gameObject.SetActive(true);
    }

    /**
     * 切换控制为人物
     * */
    void ChangeControllToPlayer()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<AudioSource>().enabled = false;
        player.GetComponent<SoundsControl>().enabled = true;
        chooseObj.GetComponent<MoveCar>().enabled = false;
        chooseObj.transform.Find("FollowObjs").gameObject.SetActive(false);
    }

    /**
     * 切换控制为火箭
     * */
    void ChangeControllToRocket()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<SoundsControl>().enabled = false;
        player.GetComponent<AudioSource>().enabled = false;

        GameObject rocket = GameObject.Find("rocket");
        rocket.transform.Find("FollowObjs").gameObject.SetActive(true);
        // 激活火箭启动脚本
        rocket.GetComponent<RocketStart>().enabled = true;
    }
}
