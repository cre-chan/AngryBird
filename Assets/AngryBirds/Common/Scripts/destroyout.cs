using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyout : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "bird":
                {
                    other.gameObject.GetComponent<bird>().bs = bird.BirdState.dead;
                    return;
                }
        }
        Destroy(other.gameObject);
    }
    
}
