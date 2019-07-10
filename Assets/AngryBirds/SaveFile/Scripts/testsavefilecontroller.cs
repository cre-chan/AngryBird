using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.AngryBirds.UI.levelloaders;
using Assets.AngryBirds.SaveFile.Scripts.SaveFile;
public class testsavefilecontroller : MonoBehaviour {

    public GameObject overRecord;
	// Use this for initialization
	void Start () {



        LevelRecordLoader temp = new LevelRecordLoader(12);
        temp.ShowAllLevelRecord();

        temp.CompareMaxRecord(1,3);
        temp.CompareMaxRecord(1,7);
        temp.CompareMaxRecord(1, 2);

       
        
        
    }
	

}
