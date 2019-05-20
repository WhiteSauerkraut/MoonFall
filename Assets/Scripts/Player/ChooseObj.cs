using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 主角碰撞器检测
 * */

public class ChooseObj : MonoBehaviour
{
    public GameObject chooseObj;

    void OnTriggerEnter(Collider collider)
    {
        chooseObj = collider.gameObject;
        if(chooseObj != null)
        {
            if (chooseObj.tag.Equals("Car"))
            {
                chooseObj.GetComponent<Outline>().enabled = true;
                chooseObj.GetComponent<Outline>().eraseRenderer = false;
            }
            else if (chooseObj.tag.Equals("Npc") || chooseObj.tag.Equals("Launchpad"))
            {
                chooseObj.transform.GetChild(1).GetComponent<Outline>().enabled = true;
                chooseObj.transform.GetChild(1).GetComponent<Outline>().eraseRenderer = false;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        bool isInCar = GetComponent<Interact>().isInCar;
        if(chooseObj != null)
        {
            if (chooseObj.tag.Equals("Car"))
            {
                chooseObj.GetComponent<Outline>().enabled = false;
                if (!isInCar)
                {
                    chooseObj = null;
                }
            }
            else if (chooseObj.tag.Equals("Npc") || chooseObj.tag.Equals("Launchpad"))
            {
                chooseObj.transform.GetChild(1).GetComponent<Outline>().enabled = false;
                chooseObj = null;
            }
        }
        
    }
}
