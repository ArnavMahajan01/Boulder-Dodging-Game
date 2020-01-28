using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    public Transform armature;

    private float offsetX;
    private float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        offsetX = transform.position.x - player.position.x;
        offsetY = transform.position.y - player.position.y;
    }

    void LateUpdate()
    {
       if(GameObject.Find("PlayerObject Variant").GetComponent<Player>().isCoroutineRunning == true)
       {
           transform.position = new Vector3(armature.position.x + offsetX, armature.position.y + offsetY, armature.position.z);
       }
       else
       {
           transform.position = new Vector3(player.position.x + offsetX, player.position.y + offsetY, player.position.z);
       }
    }
}
