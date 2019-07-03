using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is mainly the camara itself. The central control will be scattered into other file
/*
 包括摄像机和中央控制脚本。分为MyCamera.cs和CentralControl.cs两部分。MyCamera.cs主要负责摄像机本身。
 CentralControl.cs负责游戏循环和各种信息交流。目前CentralControl仍不成熟，需要改进。

     */

[RequireComponent(typeof(Camera))]
public partial class MyCamara : MonoBehaviour {
    [SerializeField]
	private float panSpeed;
    [SerializeField]
    private float scaleRate;

    [SerializeField]
    private float maxSize;//determines the max Size of camera scale
    [SerializeField]
    private float minSize;//the minimum
    [SerializeField]
    private Collider2D sceneBorder;//this is optional, if absent,the camera is unbound




	//restricts the translation in some area
	void Translate(Vector3 direct){
        this.transform.Translate(direct);
        if (sceneBorder != null)
            AdjustPos();
    }


    //we always adjust position in order to keep the camera in scene.The sceneBorder should always be non-null.
    void AdjustPos() {
        var border=Utility.BoundToRect(sceneBorder.bounds);
        var camBorder = Utility.GetCamViewRect(GetComponent<Camera>());

        var y_diff = 0f;
        var  x_diff=0f;

        if (camBorder.yMax < border.yMin)
            y_diff = border.yMin - camBorder.yMax;
        else if (camBorder.yMin > border.yMax)
            y_diff = border.yMax- camBorder.yMin;

        if (camBorder.xMin < border.xMin)
            x_diff = border.xMin - camBorder.xMin;
        else if (camBorder.xMax > border.xMax)
            x_diff = border.xMax - camBorder.xMax;


        var mov_vec = new Vector3(x_diff, y_diff, 0);

        this.transform.Translate(mov_vec);
    }


    void Scale(float scaleDiff) {
        var endScale = GetComponent<Camera>().orthographicSize + scaleDiff;
        //ensures scale between a valid range
        endScale = Mathf.Clamp(endScale, minSize, maxSize);
        GetComponent<Camera>().orthographicSize = endScale;

        //this ensures no null-reference exception
        if (sceneBorder != null)
            AdjustPos();
    }


    
}
