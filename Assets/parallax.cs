using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField]
    GameObject camOBJ;
    [SerializeField]
    GameObject layer1;
    [SerializeField]
    GameObject layer2;
    [SerializeField]
    GameObject layer3;
    [SerializeField]
    GameObject layer4;
    [SerializeField]
    float layer1XSpeed = 1.0f;
    [SerializeField]
    float layer2XSpeed = 1.0f;
    [SerializeField]
    float layer3XSpeed = 1.0f;
    [SerializeField]
    float layer4XSpeed = 1.0f;
    [SerializeField]
    float layer1YSpeed = 1.0f;
    [SerializeField]
    float layer2YSpeed = 1.0f;
    [SerializeField]
    float layer3YSpeed = 1.0f;
    [SerializeField]
    float layer4YSpeed = 1.0f;
    Camera cam;

    private Vector3 prevCampos;


    private Vector2 l1_dim;
    private Vector2 l2_dim;
    private Vector2 l3_dim;
    private Vector2 l4_dim;

    private Vector3 campos;
    private Vector3 camdiff;

    // Start is called before the first frame update
    void Awake()
    {
       cam = camOBJ.GetComponent<Camera>();
       prevCampos = cam.transform.position;
        Sprite[] sp= new Sprite[4];
        sp[0] = layer1.GetComponent<SpriteRenderer>().sprite;
        sp[1] = layer2.GetComponent<SpriteRenderer>().sprite;
        sp[2] = layer3.GetComponent<SpriteRenderer>().sprite;
        sp[3] = layer4.GetComponent<SpriteRenderer>().sprite;

        int i = 0;
        l1_dim.x = (sp[i].texture.width / sp[i].pixelsPerUnit);
        l1_dim.y = (sp[i].texture.height / sp[i].pixelsPerUnit);
        i = 1;
        l2_dim.x = (sp[i].texture.width / sp[i].pixelsPerUnit);
        l2_dim.y = (sp[i].texture.height / sp[i].pixelsPerUnit);
        i = 2;
        l3_dim.x = (sp[i].texture.width / sp[i].pixelsPerUnit);
        l3_dim.y = (sp[i].texture.height / sp[i].pixelsPerUnit);
        i = 3;
        l4_dim.x = (sp[i].texture.width / sp[i].pixelsPerUnit);
        l4_dim.y = (sp[i].texture.height / sp[i].pixelsPerUnit);


        Debug.Log(l1_dim);
        Debug.Log(l2_dim);
        Debug.Log(l3_dim);
        Debug.Log(l4_dim);
        campos = cam.transform.position;
        camdiff = Vector3.zero;
}

    // Update is called once per frame
    void Update()
    {
        campos = cam.transform.position;
        camdiff =  campos - prevCampos;

        //Debug.Log(campos);

        parallaxLayer(layer1,layer1XSpeed,layer1YSpeed,l1_dim);
        parallaxLayer(layer2,layer2XSpeed,layer2YSpeed,l2_dim);
        parallaxLayer(layer3,layer3XSpeed,layer3YSpeed,l3_dim);
        parallaxLayer(layer4,layer4XSpeed,layer4YSpeed,l4_dim);



        prevCampos = campos;
        
    }

    private void parallaxLayer(GameObject layer , float xspeed , float yspeed , Vector2 diamentions)
    {
        Vector3 layerPos = layer.transform.position;
        layerPos = new Vector3(layerPos.x + camdiff.x * xspeed, layerPos.y + camdiff.y * yspeed, layerPos.z);

        if (campos.x>diamentions.x+layerPos.x)
        {
            layerPos.x += diamentions.x;
            Debug.Log(layer.name+" shifted just now");
        }
        
        layer.transform.position = layerPos;
    }


}
