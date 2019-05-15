using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 谈话组件
 * */

public class TalkCompoent : MonoBehaviour
{
    // 谈话内容
    public string[] talk_strs;


    // Start is called before the first frame update
    void Start()
    {
        talk_strs = new string[] { "去吧，少年！", "带着我们的共同梦想，前进吧！" };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
