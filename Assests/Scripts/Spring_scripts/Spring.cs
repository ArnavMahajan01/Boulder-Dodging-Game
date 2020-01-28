using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public GameObject block;
    private BoulderSpring boulderSpringScript;

    private bool stopLoop = false;

    private float num;

    private float checkDirection = 0;

    private bool ignoreFirstVal = true;
    // Start is called before the first frame update
    void Start()
    {
       boulderSpringScript = block.GetComponent<BoulderSpring>();
    }

    // Update is called once per frame
    void Update()
    {
        num = checkDirection/25f;
        checkDirection = boulderSpringScript.lateUpdatePosZ;
        

        if(stopLoop == false)
        {
         transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + num, transform.localScale.z);
        }
        if(checkDirection  < 0)
        {
           stopLoop = true;
        }
        else if(checkDirection > 0)
        {
            stopLoop = false;
        }
               
    }
    

    void OnTriggerEnter(Collider collider)
    {
        if(checkDirection > 0 && collider.gameObject.CompareTag("boulder") == true)
        {
         stopLoop = true;
        }
        else if(checkDirection < 0 && collider.gameObject.CompareTag("boulder") == true)
        {
            stopLoop = false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(checkDirection > 0 && collider.gameObject.CompareTag("boulder") == true)
        {
         stopLoop = true;
        }
        else if(checkDirection < 0 && collider.gameObject.CompareTag("boulder") == true)
        {
            stopLoop = false;
        }
    }
}
