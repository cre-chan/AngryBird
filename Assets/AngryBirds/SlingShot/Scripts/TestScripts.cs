using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour {
	public bird bullet;
	public SlingShot shot;

	// Use this for initialization
	void Start () {
		shot.load (bullet);
		//shot.emit (Vector2.right * 2);
	}
	

}
