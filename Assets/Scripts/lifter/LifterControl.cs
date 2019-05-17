using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 升降台控制
 * */

public class LifterControl : MonoBehaviour
{
    public float min_height = 58.5f;
    public float max_height = 64.3f;

    public GameObject chooseObj;

    public float speed = 0.5f;

    public bool isMove = false;

    void OnTriggerEnter(Collider collider)
    {
        chooseObj = collider.gameObject;
        if (collider.gameObject != null)
        {
            if (chooseObj.tag.Equals("Player") && !isMove)
            {
                isMove = true;
                StartCoroutine(LifterUp());
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (chooseObj != null)
        {
            if (chooseObj.tag.Equals("Player") && !isMove)
            {
                isMove = true;
                StartCoroutine(LifterDown());
            }
            chooseObj = null;
        }
    }

    /**
     * 升降台上升
     * */
    IEnumerator LifterUp()
    {
        // 人物站上升降台3s后开始上升
        yield return new WaitForSeconds(3);

        CharacterController player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        while (transform.position.y < max_height)
        {
            yield return null;
            transform.Translate(new Vector3(0f, speed, 0f) * Time.deltaTime, Space.World);
            player.Move(new Vector3(0f, speed, 0f) * Time.deltaTime);
        }
        isMove = false;

        // 调整最高位置
        if (transform.position.y > max_height)
        {
            transform.position = new Vector3(transform.position.x, max_height, transform.position.z);
        }
    }

    /**
     * 升降台下降
     * */
    IEnumerator LifterDown()
    {
        // 人物离开升降台后3s开始下降
        yield return new WaitForSeconds(3);

        while (transform.position.y > min_height)
        {
            yield return null;
            transform.Translate(new Vector3(0f, -speed, 0f) * Time.deltaTime, Space.World);
        }
        isMove = false;

        // 调整最低位置
        if (transform.position.y < min_height)
        {
            transform.position = new Vector3(transform.position.x, min_height, transform.position.z);
        }
    }
}
