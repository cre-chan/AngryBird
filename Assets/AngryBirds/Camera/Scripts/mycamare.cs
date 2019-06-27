using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mycamare : MonoBehaviour {

    private float worldWidth;
    private float worldHeight;

    public float myMaxSize;
    public float myMinSize;
    public float changeRate;
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (GetComponent<Camera>().orthographicSize < myMaxSize)
            {
                GetComponent<Camera>().orthographicSize += changeRate;
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (GetComponent<Camera>().orthographicSize > myMinSize)
            {
                GetComponent<Camera>().orthographicSize -= changeRate;
            }

        }
        
    }

    

}
