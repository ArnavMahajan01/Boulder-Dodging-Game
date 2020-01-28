using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsWorld : MonoBehaviour
{
    [SerializeField]
    public float gravityForce;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
         Physics.gravity = new Vector3(0, -1*gravityForce, 0); 
    }
}
