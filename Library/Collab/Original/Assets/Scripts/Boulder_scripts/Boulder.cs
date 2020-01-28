kusing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private LineRenderer lr;
    private Rigidbody rbd;
    
    private float lowForce;
    private float highForce;

    private float InitialVelocity;

    List<Vector3> points = new List<Vector3>();

    private float gravitationalForce; 

    private float planeAngle;

    private Vector3[] pointsArray;

    private float timeOfFlight;
    private float slopeAngle;

    private bool removed = false;

    private Vector3 point;

    private Vector3 initialPoint;

    private bool trajectoryShown = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        lr = GetComponent<LineRenderer>();
        rbd = GetComponent<Rigidbody>();
        
        lowForce = GameObject.Find("Boulder_parent").GetComponent<BoulderInstantiate>().lowerForce;
        highForce = GameObject.Find("Boulder_parent").GetComponent<BoulderInstantiate>().upperForce;

        InitialVelocity = Random.Range(lowForce, highForce);

        rbd.AddForce(new Vector3(-1*InitialVelocity, 0, 0), ForceMode.VelocityChange);


        gravitationalForce = GameObject.Find("Boulder_parent").GetComponent<PhysicsWorld>().gravityForce;
        planeAngle = GameObject.Find("plane").GetComponent<Transform>().eulerAngles.z;

        timeOfFlight = 2*InitialVelocity*Mathf.Tan((float)(planeAngle*22.0/(7.0*180.0)))/(gravitationalForce);
        slopeAngle = (float)(planeAngle*22.0/(7.0*180.0));

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -16)
        {
            Destroy(gameObject);
        }

        if(PlayerControl.isSpecialAbilityActivated == true && trajectoryShown == false)
        {
            trajectoryShown = true;
            showTrajectory();
        }

        removePointsFromTrajectory();
    }

    protected void showTrajectory()
    {
       for(float t = 0; t <= timeOfFlight; t += 0.1f)
          {
            point = new Vector3(initialPoint.x - InitialVelocity*Mathf.Cos(slopeAngle)*t, 
                initialPoint.y - gravitationalForce*t*t/2, initialPoint.z);
            
            if(point.x < transform.position.x && point.y < transform.position.y)
            {
                points.Add(point);
            }

          }

          points.Add(new Vector3(initialPoint.x - InitialVelocity*Mathf.Cos(slopeAngle)*timeOfFlight, 
            initialPoint.y - gravitationalForce*timeOfFlight*timeOfFlight/2, initialPoint.z));

          pointsArray = new Vector3[points.Count];
          for(int i = 0; i < points.Count; i++)
          {
            pointsArray[i] = points[i];
          }


          lr.positionCount = points.Count;
          lr.SetPositions(pointsArray);
    } 

    protected void removePointsFromTrajectory()
    {
        for(int i = 0; i < points.Count; i++)
        {
            if(points[i].x > transform.position.x)
            {
                points.RemoveAt(i);
                removed = true;
            }
            
        }
        if(removed == true)
        {
          pointsArray = new Vector3[points.Count];
          for(int i = 0; i < points.Count; i++)
          {
              pointsArray[i] = points[i];
          }

          lr.positionCount = points.Count;
          lr.SetPositions(pointsArray);
        }
    }
}
