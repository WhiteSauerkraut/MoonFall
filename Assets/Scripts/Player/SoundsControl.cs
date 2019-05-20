using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 人物音效控制
 * */

public class SoundsControl : MonoBehaviour
{
    AudioSource aud;
    Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        aud = player.GetComponent<AudioSource>();
        ani = player.transform.Find("T-Pose").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        string ani_name = ani.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (ani_name.Equals("Idle"))
        {
            aud.Stop();
        }
        else
        {
            PlayAudioByNmae(ani_name);
            if (aud.isPlaying == false)
            {
                aud.Play();
            }
        }
    }

    /**
     * 根据动画选择音效
     * */
    public void PlayAudioByNmae(string name)
    {
        if(name.Equals("Idle"))
        {
            aud.clip = null;
            return;
        }
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name) as AudioClip;
        aud.clip = clip;
    }
}
