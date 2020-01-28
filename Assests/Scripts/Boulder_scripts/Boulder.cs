using System.Collections;
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

    private double timeOfFlight;
    private double slopeAngle;

    private bool removed = false;

    private Vector3 point;

    private Vector3 initialPoint;

    private bool trajectoryShown = false;

    public float velocity;
    private float timeElapsed = 0;

    public GameObject boulderDeatils;

    private bool destroyed = false;
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

        slopeAngle = (double)(planeAngle*22.0/(7.0*180.0));
        timeOfFlight = (double)(2.0f*InitialVelocity*Mathf.Tan((float)slopeAngle)/(gravitationalForce));
        
        lr.SetWidth(0.1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerControl.isSpecialAbilityActivated == true && trajectoryShown == false)
        {
            trajectoryShown = true;
            boulderDeatils.SetActive(true);
            showTrajectory();
        }

        removePointsFromTrajectory();

        if(points.Count == 0 && destroyed == false && trajectoryShown == true)
        {
            destroyed = true;
            boulderDeatils.SetActive(false);
        }

        if(LevelCompleteScript.isLevelComplete == true)
        {
            rbd.isKinematic = true;
        }

        timeElapsed += Time.deltaTime;
        velocity = Mathf.Sqrt(InitialVelocity*InitialVelocity + Mathf.Pow(gravitationalForce*timeElapsed*timeElapsed/2.0f, 2.0f));
    }

    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("ground"))
       {
           StartCoroutine(delayDelete());
       }
    }

    protected void showTrajectory()
    {
       for(float t = 0; t <= timeOfFlight; t += 0.05f)
          {
            point = new Vector3(initialPoint.x - InitialVelocity*t, 
                initialPoint.y - (float)(gravitationalForce*t*t/2.0f), initialPoint.z);
            
            if(point.x < transform.position.x && point.y < transform.position.y)
            {
                points.Add(point);
            }

          }

          points.Add(new Vector3((float)(initialPoint.x - InitialVelocity*timeOfFlight), 
            initialPoint.y - (float)(gravitationalForce*timeOfFlight*timeOfFlight/2.0f), initialPoint.z));

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
            if(points[i].x > transform.position.x && points[i].y > transform.position.y)
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

    IEnumerator delayDelete()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
