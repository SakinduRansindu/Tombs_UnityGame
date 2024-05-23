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
    private bool canJump = true;
    private bool canFly = false;
    private bool isFacedToRight=true;


    public float speedX = 1f;
    public float speedY = 1f;
    public float VELO_STEPS = 10f;

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
        // Debug.Log(groundCheckController.isGroundTouched);
        


        if ((moveX > 0.00 || moveX < -0.00) && !groundCheckController.isInAir) {
            anim.SetBool("isMovingX", true);
            speedXtmp = moveX * speedX * Time.fixedDeltaTime;
            if(boost<-0.00 || boost>0.00)
            {
                speedXtmp *= 2;
            }
            if (groundCheckController.isStepsTouched)
            {
                anim.SetBool("isSteps",true);
                if (!groundCheckController.slope)
                {
                    speedYtmp = VELO_STEPS*Time.fixedDeltaTime;
                }
            }
            else
            {
                anim.SetBool("isSteps",false);
            }

        }
        else if(!groundCheckController.isInAir)
        {
            anim.SetBool("isMovingX", false);
            speedXtmp = 0f;
        }
        else
        {
            anim.SetBool("isMovingX", false);
        }

        if ((moveY > 0.00 || moveY < -0.00) && !groundCheckController.isInAir)
        {
            if (moveY > 0)
            {
                if (canJump) {
                    // on ground and need to go up - jump
                    Debug.Log("jump");
                    anim.SetBool("isMovingY", true);
                    canJump = false;
                    canFly = true;
                    speedYtmp = speedY * Time.fixedDeltaTime;
                }
            }
            else
            {
                // on ground and need to go down - crouch
                Debug.Log("crouch");
                anim.SetBool("isMovingY", false);
                canJump = true;
            }
        }
        else if(moveY>0.00 || moveY<-0.00)
        {
            if (moveY > 0.00)
            {
                if (canFly)
                {
                    // not on the ground and need to go up - transform to fly
                    Debug.Log("fly");
                    canFly = false;
                }
            }
            else
            {
                // not on the ground and need to go down - fall
                Debug.Log("fall");
            }
        }
        else if (!groundCheckController.isInAir) {
            anim.SetBool("isMovingY", false);
            canJump = true;
            canFly = false;
        }


        rb.velocity = new Vector2(speedXtmp, speedYtmp);
        anim.SetFloat("speedX", speedXtmp);
        anim.SetFloat("speedY", speedYtmp);
        if (speedXtmp > 0 && !isFacedToRight)
        {
            flipCharacter();
        }
        else if(speedXtmp < 0 && isFacedToRight)
        {
            flipCharacter();
        }

        //Debug.Log(groundCheckController.isGroundTouched);
    }

    private void flipCharacter()
    {
        Vector3 current = rb.transform.localScale;
        current.x *= -1;
        rb.transform.localScale = current;
        isFacedToRight = !isFacedToRight;
    }

}
