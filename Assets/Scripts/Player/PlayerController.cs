﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 控制人物移动
 * */

public class PlayerController : MonoBehaviour
{
    CharacterController player;
    Animator animator;
    Vector3 moveDirection;
    GameObject model;

    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float backSpeed = -1;
    public float jumpSpeed = 3;
    public float gravity = 9.8f;

   
    public bool isJump = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        animator = this.transform.GetComponentInChildren<Animator>();
        model = transform.Find("T-Pose").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        string anim_name = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        // 在空中的时候不能控制
        if (player.isGrounded && !anim_name.Equals("Jump"))
        {
            if (anim_name.Equals("Idle"))
            {
                SetModelPosition();
            }

            // WASD控制
            if (Input.GetKey(KeyCode.W))
            {
                Quaternion q = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
                this.transform.rotation = q;
                moveDirection = new Vector3(0.0f, 0.0f, 1);
                moveDirection = transform.TransformDirection(moveDirection);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection = moveDirection * runSpeed;
                    animator.SetFloat("speed", 5);
                }
                else
                {
                    moveDirection = moveDirection * walkSpeed;
                    animator.SetFloat("speed", 3);
                }


            }
            else if (Input.GetKey(KeyCode.S))
            {
                Quaternion q = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 180, 0);
                this.transform.rotation = q;
                moveDirection = new Vector3(0.0f, 0.0f, -1);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection = moveDirection * backSpeed;
                animator.SetFloat("speed", 3);
            }
            else if (Input.GetKey(KeyCode.A))
            {

                Quaternion q = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y - 90, 0);
                this.transform.rotation = q;
                moveDirection = new Vector3(0.0f, 0.0f, 1);
                moveDirection = transform.TransformDirection(moveDirection);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection = moveDirection * runSpeed;
                    animator.SetFloat("speed", 5);
                }
                else
                {
                    moveDirection = moveDirection * walkSpeed;
                    animator.SetFloat("speed", 3);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Quaternion q = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 90, 0);
                this.transform.rotation = q;
                moveDirection = new Vector3(0.0f, 0.0f, 1);
                moveDirection = transform.TransformDirection(moveDirection);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection = moveDirection * runSpeed;
                    animator.SetFloat("speed", 5);
                }
                else
                {
                    moveDirection = moveDirection * walkSpeed;
                    animator.SetFloat("speed", 3);
                }
            }
            else
            {
                moveDirection = Vector3.zero;
                animator.SetFloat("speed", 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isJump)
            {
                animator.SetFloat("speed", 0);
                animator.SetBool("isJump", true);
                moveDirection.y = jumpSpeed;
                moveDirection.x /= 3;
                moveDirection.z /= 3;
                isJump = true;
            }
        }
        else if(!player.isGrounded && anim_name.Equals("Jump"))
        {
            animator.SetBool("isJump", false);
            isJump = false;
        }
        else if(player.isGrounded && anim_name.Equals("Jump"))
        {
            moveDirection = Vector3.zero;
        }
        
        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        player.Move(moveDirection * Time.deltaTime);
    }

    /**
     * 固定角色模型位置
     * */
    void SetModelPosition()
    {
        model.transform.localPosition = Vector3.zero;
    }
}