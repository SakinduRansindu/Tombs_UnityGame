using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    public float speed = 1f;
    public float xoffset = 1f;
    public float yoffset = 1f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 plPos = rb.position;
        plPos.z = transform.position.z;
        plPos.x += xoffset;
        plPos.y += yoffset;
        transform.position = Vector3.Slerp(transform.position, plPos, speed * Time.deltaTime);
    }
}
