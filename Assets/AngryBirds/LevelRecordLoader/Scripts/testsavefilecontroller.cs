﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testsavefilecontroller : MonoBehaviour {

    public GameObject overRecord;
	// Use this for initialization
	void Start () {



        LevelRecordLoader temp = LevelRecordLoader.GetInstance();
        temp.ShowAllLevelRecord();

        temp.CompareMaxRecord(1,3);
        temp.CompareMaxRecord(1,7);
        temp.CompareMaxRecord(1, 2);

       
        
        
    }
	

}