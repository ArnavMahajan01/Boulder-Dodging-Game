using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public  float distanceEdges;
    // Start is called before the first frame update

    void Awake()
    {
        distanceEdges = Vector3.Distance(object2.position, object1.position); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}