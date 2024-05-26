using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public int GROUND_LAYER = 6;
    public int STEPS_LAYER = 7;
    public bool isGroundTouched = false;
    public bool isStepsTouched = false;
    public bool isInAir = true;
    public bool slopeAtX_Plus = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == GROUND_LAYER)
        {
            isGroundTouched = true;
        }
        if (collision.gameObject.layer == STEPS_LAYER)
        {
            isStepsTouched=true;
            if (collision.gameObject.tag == "UP")
            {
                slopeAtX_Plus = false;
            }
            else
            {
                slopeAtX_Plus = true;
            }
        }
        isInAir = (!(isGroundTouched || isStepsTouched));

         
       

        Debug.Log(collision.gameObject.layer);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GROUND_LAYER)
        {
            isGroundTouched = false;
        }
        if (collision.gameObject.layer == STEPS_LAYER)
        {
            isStepsTouched = false;
        }

        isInAir = (!(isGroundTouched || isStepsTouched));

        Debug.Log(collision.gameObject.layer);
    }
}
