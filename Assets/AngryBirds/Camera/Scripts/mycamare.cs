using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class mycamare : MonoBehaviour {

	public float panSpeed;
	public float scaleRate;

    public float maxSize;//determines the max Size of camera scale
    public float minSize;//the minimum
    public Collider2D sceneBorder;//this is optional, if absent,the


	//a structure indicating to what extent the camera move and 
	class Move{
		//indicates the direction a camera should go,normalized
		public Vector3 pan;
		public float scale;

		//get those nasty input done.
		public Move(){
			this.pan=new Vector3(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"),
				0
			);
			this.pan.Normalize();

			this.scale=-Input.GetAxis("Mouse ScrollWheel");
		}

	}

	//restricts the translation in some area
	void Translate(Vector3 direct){
        this.transform.Translate(direct);
        AdjustPos();

    }

    //we always adjust position in order to keep the camera in
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
        endScale=Mathf.Clamp(endScale, minSize, maxSize);
        //ensures scale between a valid range
        GetComponent<Camera>().orthographicSize = endScale;
        AdjustPos();
    }


    // Update is called once per frame
    void Update()
    {
		var mov = new Move ();
		Translate (mov.pan * panSpeed * Time.deltaTime);
        Scale(mov.scale * scaleRate * Time.deltaTime);

    }

    

}
