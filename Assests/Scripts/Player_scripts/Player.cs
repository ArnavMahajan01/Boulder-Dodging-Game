using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rbd;

    private bool isColliding;

    private Vector3 initialAngles; 
    private PlayerControl playerControl;

    public bool isCoroutineRunning = false;

    private Coroutine standRoutine;

    List<Collider> ragDollColliders = new List<Collider>();
    List<Rigidbody> ragDollRbds = new List<Rigidbody>();
    List<Transform> ragDollTransforms = new List<Transform>();
    List<Vector3> initialRagdollAngles = new List<Vector3>();

    private Collider[] playerCollider;

    private Animator playerAnimator;

    public Transform armaturePos;

    private int index = 0;

    private bool isRagdollEnabled = false;
    // Start is called before the first frame update
    void Awake()
    {
        disableRagdollAtAwake();
        transformRagdollsAwake();
    }

    void Start()
    {
        isColliding = false;
        rbd = GetComponent<Rigidbody>();
        playerControl = gameObject.GetComponent<PlayerControl>();
        playerCollider = GetComponents<Collider>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isColliding)
        {
            rbd.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX 
            | RigidbodyConstraints.FreezeRotationY;
        }

        if(PlayerControl.isCrouching && !isRagdollEnabled)
        {
            playerCollider[1].isTrigger = false;
            playerCollider[0].isTrigger = true;
            index = 1;
        }
        else if(!PlayerControl.isCrouching && !isRagdollEnabled)
        {
            playerCollider[0].isTrigger = false;
            playerCollider[1].isTrigger = true;
            index = 0;
        }
    }

    protected void transformRagdollsAwake()
    {
        Transform[] rotRags = gameObject.GetComponentsInChildren<Transform>();

        foreach(Transform rotRag in rotRags)
        {
            if(rotRag.gameObject != gameObject)
            {
                ragDollTransforms.Add(rotRag);
                initialRagdollAngles.Add(rotRag.eulerAngles);
            }
        }
    }

    protected void transformRagdolls()
    {
        for(int i = 0; i < ragDollTransforms.Count; i++)
        {
            ragDollTransforms[i].eulerAngles = new Vector3(initialRagdollAngles[i].x, initialRagdollAngles[i].y, initialRagdollAngles[i].z);
        }
    }

    protected void disableRagdollAtAwake()
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        Rigidbody[] rbds = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach(Collider c in colliders)
        {
            if(c.gameObject != gameObject)
            {
                c.isTrigger = true;
                ragDollColliders.Add(c);
            }
        }

        foreach(Rigidbody rbdy in rbds)
        {
            if(rbdy.gameObject != gameObject)
            {
                rbdy.isKinematic = true;
                ragDollRbds.Add(rbdy);
            }
        }
    }
    
    protected void enableRagdoll()
    {
        rbd.isKinematic = true;
        playerCollider[index].isTrigger = true;
        isRagdollEnabled = true;
        foreach(Rigidbody rbdy in ragDollRbds)
        {
            rbdy.isKinematic = false;
        }
        foreach(Collider c in ragDollColliders)
        {
            c.isTrigger = false;
        }
        
    }

    protected void disableRagdoll()
    {
        foreach(Collider c in ragDollColliders)
        {
            c.isTrigger = true;
        }

        foreach(Rigidbody rbdy in ragDollRbds)
        {
            rbdy.isKinematic = true;
        }
        rbd.isKinematic = false;
        playerCollider[index].isTrigger = false;
        isRagdollEnabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("boulder"))
        {
            if(isCoroutineRunning == true)
            {
               StopCoroutine(standRoutine);
               isCoroutineRunning = false;
            }
    
            isColliding = true;
            playerControl.enabled = false;
            playerAnimator.enabled = false;

            rbd.constraints &= ~RigidbodyConstraints.FreezeRotationX;
            rbd.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            rbd.constraints &= ~RigidbodyConstraints.FreezeRotationZ;

            enableRagdoll();
        }
        
        /*
        if(collision.gameObject.CompareTag("ground"))
        {

        }
        */
    }

    

    void OnCollisionExit(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("boulder"))
        {  
          standRoutine = StartCoroutine(delayStanding());
        }
    }

    IEnumerator delayStanding()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(2);
        PlayerControl.isCrouching = false;
        transform.position = new Vector3(armaturePos.position.x, armaturePos.position.y, armaturePos.position.z);

        transformRagdolls();

        isColliding = false;
        playerControl.enabled = true;
        rbd.velocity = Vector3.zero;
        rbd.angularVelocity = Vector3.zero;
        playerAnimator.enabled = true;
        disableRagdoll();
        isCoroutineRunning = false;
    }
}
