using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 谈话窗口控制
 * */

public class TalkWindow : MonoBehaviour
{
    GameObject talk_window;
    Text text_talk;
    Text text_talker;
    Text text_speaker;
    Button btn;
    Text hint_text;

    void Init()
    {
        talk_window = transform.Find("talk_window").gameObject;
        text_talk = talk_window.transform.Find("Text_talk").gameObject.GetComponent<Text>();
        text_talker = talk_window.transform.Find("Text_talker").gameObject.GetComponent<Text>();
        text_speaker = talk_window.transform.Find("Text_speaker").gameObject.GetComponent<Text>();
        btn = talk_window.transform.Find("Button").gameObject.GetComponent<Button>();
        hint_text = btn.transform.GetComponentInChildren<Text>();
    }

    void Show()
    {

    }

    void Hide()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
