using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoulderTextDetails : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject boulder_;

    private Vector3 boulderLocalPosition;

    [SerializeField]
    private float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(boulder_ != null && LevelCompleteScript.isLevelComplete == false)
        {
          text.text = "" + System.Math.Round(boulder_.GetComponent<Boulder>().velocity, 0);    
        }
    }

    void LateUpdate()
    {
        if(boulder_ != null)
        {
          boulderLocalPosition = boulder_.GetComponent<Transform>().localPosition;
          transform.localEulerAngles = new Vector3(-29.533f, 86.021f, -1.198f);
          transform.localPosition = new Vector3(boulderLocalPosition.x, boulderLocalPosition.y + offsetY, boulderLocalPosition.z);
        }
    }
}
