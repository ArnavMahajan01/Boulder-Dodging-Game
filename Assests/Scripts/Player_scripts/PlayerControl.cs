using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{ 
    private Rigidbody rbd;
    protected Vector3 direction;
    private bool isMoving;

    public GameObject plane;

    private float rootTwo = Mathf.Sqrt(2.0f);

    [SerializeField]
    private float speedMultiplier;

    private bool isMultiplied = false;
    public static bool isCrouching = false;

    private Animator animator;

    [SerializeField]
    private float speed;

    public static bool isSpecialAbilityActivated = false;
    // Start is called before the first frame update
    void Start()
    {
       rbd = GetComponent<Rigidbody>();
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();

        if(LevelCompleteScript.isLevelComplete == true)
        {
          isMoving = false;
          isMultiplied = false;
        }
     
        animator.SetBool("isRunning", isMultiplied);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isCrouching", isCrouching);
    }

    void FixedUpdate()
    {
      if(isMultiplied && LevelCompleteScript.isLevelComplete == false)
      {
        rbd.MovePosition(rbd.position + direction*Time.deltaTime*speed*speedMultiplier);
      }
      else if(!isMultiplied && LevelCompleteScript.isLevelComplete == false)
      {
        rbd.MovePosition(rbd.position + direction*Time.deltaTime*speed);
      }
    }

    protected void setDirection()
    {
        direction = Vector3.zero;
        isMultiplied = false;
        isMoving = false;
        if(isCrouching == false)
        {
        if(Input.GetKey(KeyCode.W))
        {
          isMoving = true;
            if(Input.GetKey(KeyCode.D))
            {
              direction = new Vector3((float)(Mathf.Cos((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), 
              (float)(Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), (float)(-1/rootTwo));
            }
            else if(Input.GetKey(KeyCode.A))
            {
                direction = new Vector3((float)(Mathf.Cos((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), 
              (float)(Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), (float)(1/rootTwo));
            }
            else{
              direction = new Vector3(Mathf.Cos((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z)), 
              Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z)), 0);
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
          isMoving = true;
            if(Input.GetKey(KeyCode.S))
            {
                direction = new Vector3((float)(-1*Mathf.Cos((float)(-1*Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), 
              (float)(-1*Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), (float)(-1/rootTwo));
            }
            else if(Input.GetKey(KeyCode.W))
            {
                direction = new Vector3((float)(Mathf.Cos((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), 
              (float)(Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), (float)(-1/rootTwo));
            }
            else{
               direction = new Vector3(0, 0, -1);
            }
            
        }
        else if(Input.GetKey(KeyCode.A))
        {
          isMoving = true;
            if(Input.GetKey(KeyCode.W))
            {
               direction = new Vector3((float)(Mathf.Cos((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), 
              (float)(Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), (float)(1/rootTwo));
            }
            else if(Input.GetKey(KeyCode.S))
            {
                direction = new Vector3((float)(-1*Mathf.Cos((float)(-1*Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), 
              (float)(-1*Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), (float)(1/rootTwo));
            }
            else{
                direction = new Vector3(0, 0, 1);
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
          isMoving = true;
            if(Input.GetKey(KeyCode.D))
            {
                direction = new Vector3((float)(-1*Mathf.Cos((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), 
              (float)(-1*Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), (float)(-1/rootTwo));
            }
            else if(Input.GetKey(KeyCode.A))
            {
                direction = new Vector3((float)(-1*Mathf.Cos((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), 
              (float)(-1*Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z))/rootTwo), (float)(1/rootTwo));
            }
            else{
            direction = new Vector3(-1*Mathf.Cos((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z)), 
            -1*Mathf.Sin((float)(Mathf.Deg2Rad*plane.transform.eulerAngles.z)), 0);
            }
        }

        if(Input.GetKey(KeyCode.K))
        {
           isMultiplied = true;
        }
        }

        if(Input.GetKeyDown(KeyCode.Y) && isSpecialAbilityActivated == false)
        {
          isSpecialAbilityActivated = true;
          StartCoroutine(specialAbilityCoroutine());
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
          if(isCrouching)
          {
            isCrouching = false;
          }
          else if(!isCrouching)
          {
          isCrouching = true;
          }
        }
    }

    IEnumerator specialAbilityCoroutine()
    {
      yield return new WaitForSeconds(10);
      isSpecialAbilityActivated = false;
    }
}
