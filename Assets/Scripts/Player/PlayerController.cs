using System.Collections;
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

    private float walkSpeed = 3;
    private float runSpeed = 6;
    private float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        animator = this.transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //在空中的时候不能控制
        if (player.isGrounded)
        {
            //WASD控制
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
                moveDirection = moveDirection * (-walkSpeed);
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

        }
        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        player.Move(moveDirection * Time.deltaTime);
    }
}