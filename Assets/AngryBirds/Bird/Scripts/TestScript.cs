using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//tests if the bird works well
public class TestScript : MonoBehaviour {

	public bird test_target;

	// Use this for initialization
	void Start () {
		test_target.physicsLock = false;
		test_target.shoot (new Vector2(2,0));
	}

}
