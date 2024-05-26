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
    private bool isJumped = false;
    private bool canFly = false;
    private bool isFacedToRight=true;
    private int lastJumpedFrame = 0;


    public float speedX = 1f;
    public float speedY = 1f;
    public float VELO_STEPS = 10f;

    private float speedXtmp ;
    private float speedYtmp ;

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

    private void LateUpdate()
    {
        if (isJumped)
        {
            if (!groundCheckController.isInAir)
            {
                if (!(rb.velocity.y > 0.001) && Math.Abs(lastJumpedFrame - Time.frameCount) * Time.fixedDeltaTime > 1.5)
                {
                    isJumped = false;
                    anim.SetBool("isJump", false);
                    Debug.Log("not Jumping");
                }
                else if (!(rb.velocity.y > 0.00001) && Math.Abs(lastJumpedFrame - Time.frameCount) * Time.fixedDeltaTime > 0.6)
                {
                    anim.SetBool("isJump", false);
                }
            }

        }
    }

    private void FixedUpdate()
    {
        speedXtmp = rb.velocity.x;
        speedYtmp = rb.velocity.y;
        // Debug.Log(groundCheckController.isGroundTouched);
        
        bool XFourcedToRight = moveX > 0.00;
        bool XFourcedToLeft = moveX < -0.00;
        bool XFourcedToChar = XFourcedToLeft || XFourcedToRight;
        bool YFourcedToUp = moveY > 0.00;
        bool YFourcedToDown = moveY < -0.00;
        bool YFourcedToChar = YFourcedToUp || YFourcedToDown;
        bool isBoosted = boost > 0;





        if (XFourcedToChar && !groundCheckController.isInAir) {
            if (!groundCheckController.isInAir)
            {
                if (groundCheckController.isStepsTouched)
                {
                    anim.SetBool("isSteps", true);
                    steps();
                }
                else
                {
                    anim.SetBool("isSteps", false);
                    if (isBoosted)
                    {
                        run();
                    }
                    else
                    {
                        walk();
                    }
                }
            }
        }
        if(XFourcedToChar && groundCheckController.isInAir)
            {
                floatMove(isBoosted);
            }


        if (YFourcedToChar && !groundCheckController.isInAir)
        {
            if(YFourcedToUp)
            {
                if (!isJumped)
                {
                    anim.SetBool("isJump", true);
                    StartCoroutine(jump());

                }
            }
        }
        
        

        




        rb.velocity = new Vector2(speedXtmp, speedYtmp);
        anim.SetFloat("speedX", speedXtmp);
        anim.SetFloat("speedY", speedYtmp);

        if (Math.Abs(speedXtmp) > 0.001)
        {
            anim.SetBool("isMovingX", true);
        }
        else
        {
        anim.SetBool("isMovingX", false);
        }

        if (Math.Abs(speedYtmp) > 0.001)
        {
            anim.SetBool("isMovingY", true);
        }
        else
        {
            anim.SetBool("isMovingY", false);
        }

        if (speedXtmp > 0.001 && !isFacedToRight)
        {
            flipCharacter();
        }
        else if(speedXtmp < -0.001 && isFacedToRight)
        {
            flipCharacter();
        }

        //Debug.Log(groundCheckController.isGroundTouched);
    }

    private void walk()
    {
        Debug.Log("walking");
        speedXtmp = moveX * speedX * Time.deltaTime;
    }

    private void run()
    {
        Debug.Log("running");
        speedXtmp = moveX * speedX * 2 * Time.deltaTime;
    }

    private void steps()
    {
            if (groundCheckController.slopeAtX_Plus ^ isFacedToRight){
                anim.SetBool("isSlope", false);
                speedYtmp = VELO_STEPS * Time.deltaTime;
                speedXtmp = moveX * speedX * 0.8f * Time.deltaTime;
                Debug.Log("step up");
            }
            else{
                anim.SetBool("isSlope", true);
                speedXtmp = moveX * speedX * 0.5f * Time.deltaTime;
                Debug.Log("step down");
            }
        


    }

    private IEnumerator jump()
    {
        Debug.Log("jumping wait");
        isJumped = true;
        lastJumpedFrame = Time.frameCount;
        yield return new WaitForSeconds(0.5f);
        Vector2 v = rb.velocity;
        v.y = speedY * Time.deltaTime;
        rb.velocity = v;
        Debug.Log("jumping");
    }
    private void floatMove(bool isBoosted)
    {
        if (isBoosted)
        {
            Debug.Log("floatmove boost");
            speedXtmp = moveX * speedX * 1.8f * Time.deltaTime;
        }
        else
        {
            Debug.Log("floatmove");
            speedXtmp = moveX * speedX * 1.5f * Time.deltaTime; 
        }
    }

    private void flipCharacter()
    {
        Vector3 current = rb.transform.localScale;
        current.x *= -1;
        rb.transform.localScale = current;
        isFacedToRight = !isFacedToRight;
    }

}
