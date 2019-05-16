using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 谈话窗口控制
 * */

public class TalkManager : MonoBehaviour
{
    GameObject chooseObj;
    GameObject talk_window;
    Text text_talk;
    Text text_roleA;
    Text text_roleB;
    // 对话索引
    int dialogue_index = 0;
    // 对话数量
    int dialogue_count = 0;


    // 存储从文件读取的谈话内容
    List<TalkCompoent> talk_list;

    /**
     * 显示谈话窗口
     * */
    public void Show()
    {
        ParseTalkJson();
        dialogue_count = talk_list.Capacity;
        talk_window.SetActive(true);
    }

    /**
     * 隐藏谈话窗口
     * */
    public void Hide()
    {
        talk_window.SetActive(false);
        text_roleA.gameObject.SetActive(false);
        text_roleB.gameObject.SetActive(false);
        text_talk.text = "";
        text_roleA.text = "";
        text_roleB.text = "";
        dialogue_index = 0;
        dialogue_count = 0;
        chooseObj = null;
    }

    /**
     * 解析当前选择对象的Json文件
     * */
    void ParseTalkJson()
    {
        chooseObj = GameObject.FindGameObjectWithTag("Player").GetComponent<ChooseObj>().chooseObj;

        talk_list = new List<TalkCompoent>();
        Debug.Log(chooseObj.name);
        TextAsset itemText = Resources.Load<TextAsset>(chooseObj.name);
        string itemJson = itemText.text;
        JSONObject j = new JSONObject(itemJson);
        Debug.Log("itemJson " + itemJson);
        Debug.Log("itemText " + itemText);
        Debug.Log("JSONObject " + j);
        foreach (var temp in j.list)
        {
            string talker_name = temp["name"].str;
            string talk_content = temp["talk_content"].str;
            
            TalkCompoent talk = new TalkCompoent();
            talk.talker_name = talker_name;
            talk.talk_content = talk_content;

            talk_list.Add(talk);
        }
        dialogues_handle(0);
    }

    /**
     * 处理每条消息
     * */
    private void dialogues_handle(int dialogue_index)
    {
        string role = talk_list[dialogue_index].talker_name;
        string role_detail = talk_list[dialogue_index].talk_content;

        if(role.Equals("我"))
        {
            text_roleA.gameObject.SetActive(true);
            text_roleB.gameObject.SetActive(false);
            text_roleA.text = role;
        }
        else
        {
            text_roleA.gameObject.SetActive(false);
            text_roleB.gameObject.SetActive(true);
            text_roleB.text = role;
        }
        text_talk.text = role_detail;
    }


    // Start is called before the first frame update
    void Start()
    {
        talk_window = transform.Find("talk_window").gameObject;
        text_talk = talk_window.transform.Find("Text_talk").gameObject.GetComponent<Text>();
        text_roleA = talk_window.transform.Find("Text_talker").gameObject.GetComponent<Text>();
        text_roleB = talk_window.transform.Find("Text_speaker").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // 如果点击了鼠标左键，进入下一条谈话
        if (Input.GetMouseButtonDown(0))
        {
            dialogue_index++;
            if (dialogue_index < dialogue_count)
            {
                dialogues_handle(dialogue_index);
            }
            else
            {
                Hide();
            }
        }
    }
}
