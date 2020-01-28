using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpring : MonoBehaviour
{
    private Ruler rulerPlane;
    public Ruler rulerMass;
    private Transform plane;

    private Rigidbody rbd;

    private float k;

    [SerializeField]
    private float initialSpeed;

    [SerializeField]
    private float c;

    private Vector3 restPos;

    private float totalDist;

    private float c1;
    private float c2;

    private float initialAmplitude;

    private float initialAngle;

    private float timeSum = 0;

    float updatePosZ;
    public float lateUpdatePosZ;

    private float B;
    void Start()
    {
        plane = GameObject.Find("FlatPlane").GetComponent<Transform>();
        rulerPlane = GameObject.Find("ScalePlane").GetComponent<Ruler>();
        rbd = gameObject.GetComponent<Rigidbody>();
     
        initialAmplitude = rulerPlane.distanceEdges/2.0f - rulerMass.distanceEdges;

        k = rbd.mass*initialSpeed*initialSpeed/((initialAmplitude - transform.position.z)*(initialAmplitude + transform.position.z));
 
        if(c*c <= 4*rbd.mass*k)
        {
          c1 = transform.position.z;
        }
        else if(c*c > 4*rbd.mass*k)
        {
            B = Mathf.Sqrt(c*c - 4*rbd.mass*k);
            c1 = (transform.position.z*(B - c) - 2*initialSpeed*rbd.mass)/
              (2*B);
        }

        if(c*c < 4*rbd.mass*k)
        {
          c2 = Mathf.Sqrt((initialAmplitude-c1)*(initialAmplitude+c1));
        }
        else if(c*c == 4*rbd.mass*k)
        {
            c2 = c*c1/(2*rbd.mass);
        }
        else if(c*c > 4*rbd.mass*k)
        {
            c2 = transform.position.z - c1;
        }

        initialAngle = Mathf.Asin(c1/initialAmplitude);

        restPos = new Vector3(transform.position.x, transform.position.y, plane.position.z);
        
        rbd.AddForce(0, 0, initialSpeed, ForceMode.VelocityChange);
        //Debug.Log(c2);
        Debug.Log(c*c);
        Debug.Log(4*rbd.mass*k);
        Debug.Log(k);
    }

    // Update is called once per frame
    void Update()
    {
        timeSum += Time.deltaTime;
        if(c == 0)
        {
          SpringNoDamping();
        }
        else if(c*c < 4*rbd.mass*k)
        {
            SpringDampingImaginary();
        }
        else if(c*c == 4*rbd.mass*k)
        {
            SpringDampingEqual();
        }
        else if(c*c > 4*rbd.mass*k)
        {
            OverDamping();
        }
        lateUpdatePosZ = transform.position.z - updatePosZ;
        
        updatePosZ = transform.position.z;
    }

    protected void SpringNoDamping()
    {
        
        transform.position = new Vector3(transform.position.x, transform.position.y, initialAmplitude*Mathf.Sin(initialAngle + 
        Mathf.Sqrt(k/rbd.mass)*timeSum));
    }

    protected void SpringDampingImaginary()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, initialAmplitude*Mathf.Exp(-1*c*timeSum/(2*rbd.mass))*
        Mathf.Sin(initialAngle + Mathf.Sqrt(4*rbd.mass*k - c*c)*timeSum/(2*rbd.mass)));
    }

    protected void SpringDampingEqual()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y,
         (c1 + c2*timeSum)*Mathf.Exp(-1*c*timeSum/(2*rbd.mass)));
    }

    protected void OverDamping()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, c1*Mathf.Exp(-1*(c + B)*timeSum/(2*rbd.mass))
         + c2*Mathf.Exp((-1*c + B)*timeSum/(2*rbd.mass)));
    }

}
