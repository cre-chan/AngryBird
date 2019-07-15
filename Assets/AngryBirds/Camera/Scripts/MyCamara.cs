using UnityEngine;
using Utility;

//this is mainly the camara itself. The central control will be scattered into other file
/*
 包括摄像机和中央控制脚本。分为MyCamera.cs和CentralControl.cs两部分。MyCamera.cs主要负责摄像机本身。
 CentralControl.cs负责游戏循环和各种信息交流。目前CentralControl仍不成熟，需要改进。

 摄像机可以没有移动边界，但是场景不可以没有边界。因此，为了降低耦合度，决定由调用者传入边界，来保证摄像机在场景内.

     */

[RequireComponent(typeof(Camera))]
public partial class MyCamara : MonoBehaviour {
    [SerializeField]
    private float maxSize;//determines the max Size of camera scale
    [SerializeField]
    private float minSize;//the minimum

	//restricts the translation in some area
	public void Translate(Vector3 direct,Collider2D sceneBorder=null){
        this.transform.Translate(direct);
        if (sceneBorder != null)
            AdjustPos(sceneBorder);
    }


    //we always adjust position in order to keep the camera in scene.The sceneBorder should always be non-null.
    void AdjustPos(Collider2D sceneBorder=null) {
        var border=CamUtility.BoundToRect(sceneBorder.bounds);
        var camBorder = CamUtility.GetCamViewRect(GetComponent<Camera>());

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


    public void Scale(float scaleDiff,Collider2D sceneBorder = null) {
        var endScale = GetComponent<Camera>().orthographicSize + scaleDiff;
        //ensures scale between a valid range
        endScale = Mathf.Clamp(endScale, minSize, maxSize);
        GetComponent<Camera>().orthographicSize = endScale;

        //this ensures no null-reference exception
        if (sceneBorder != null)
            AdjustPos(sceneBorder);
    }


    
}
