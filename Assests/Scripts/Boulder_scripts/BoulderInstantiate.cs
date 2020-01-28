 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderInstantiate : MonoBehaviour
{
    private GameObject boulder_copy;
    public GameObject boulder;

    public Transform plane;
    public GameObject ruler;

    private bool isCreated = false;

    private float widthPlane;

    [SerializeField]
    private float timeTillNextBoulder;

    private float timeSum = 0;

    private float noOfBouldersPerFrame = 0;
    
    public float upperForce;
  
    public float lowerForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        calculateDifferentialEquationSolution();
        boulder_instantiate();
    }

    protected void boulder_instantiate()
    {
        widthPlane = ruler.GetComponent<Ruler>().distanceEdges;
        if(!isCreated && LevelCompleteScript.isLevelComplete == false)
        {
          boulder_copy = Instantiate(boulder, new Vector3(transform.position.x, transform.position.y, 
          transform.position.z + Random.Range((float)(-1*(widthPlane/2.0) + 2f), (float)(widthPlane/2.0 - 2f))), transform.rotation);

          isCreated = true;

          StartCoroutine(delayCreation());
        }
    }

    protected void calculateDifferentialEquationSolution()
    {
        timeSum += Time.deltaTime;
        noOfBouldersPerFrame = (float)((Mathf.Pow(2.71828f, (float)(Time.time + Time.deltaTime)) - Mathf.Pow(2.71828f, (float)Time.time)));
        
    }

    IEnumerator delayCreation()

    {
        yield return new WaitForSeconds(timeTillNextBoulder);
        isCreated = false;
    }
}
