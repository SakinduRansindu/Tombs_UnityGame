using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllScript : MonoBehaviour
{
    [SerializeField] GameObject gc;

    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float moveX;
    private float moveY;
    private float boost;
    private Animator anim;
    private groundCheck groundCheckController;


    public float speedX = 1f;
    public float speedY = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheckController = gc.GetComponent<groundCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        boost = Input.GetAxis("SpeedUp");

    }

    private void FixedUpdate()
    {
        float speedXtmp = rb.velocity.x;
        float speedYtmp = rb.velocity.y;


        


        if (moveX!=0 && groundCheckController.isGroundTouched) {
            anim.SetBool("isMovingX", true);
            speedXtmp = moveX * speedX * Time.fixedDeltaTime;
            if(boost!=0)
            {
                speedXtmp *= 2;
            }

        }
        else if(groundCheckController.isGroundTouched)
        {
            anim.SetBool("isMovingX", false);
            speedXtmp = 0f;
        }

        if (moveY!=0 && groundCheckController.isGroundTouched)
        {
            if (moveY > 0)
            {
                // on ground and need to go up - jump
                Debug.Log("jump");
                anim.SetBool("isMovingY", true);
                speedYtmp = speedY * Time.fixedDeltaTime;

            }
            else
            {
                // on ground and need to go down - crouch
                Debug.Log("crouch");
            }
        }
        else if(moveY!=0)
        {
            if (moveY > 0)
            {
                // not on the ground and need to go up - transform to fly
                Debug.Log("fly");

            }
            else
            {
                // not on the ground and need to go down - fall
                Debug.Log("fall");
            }
        }

        rb.velocity = new Vector2(speedXtmp, speedYtmp);
        anim.SetFloat("speedX", speedXtmp);
        anim.SetFloat("speedY", speedYtmp);


        //Debug.Log(groundCheckController.isGroundTouched);
    }

}
