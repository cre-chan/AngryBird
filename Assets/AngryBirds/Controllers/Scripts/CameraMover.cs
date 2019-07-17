using System.Collections.Generic;
using UnityEngine;
using System;
using Utility;

namespace Controllers
{
    [Serializable]
    struct CameraManipulator
    {
        [SerializeField]
        private float panSpeed;
        [SerializeField]
        private float scaleRate;
        [SerializeField]
        private Collider2D sceneBorder;

        [SerializeField]
        private Camera cam;
        [SerializeField]
        private float maxSize;//determines the max Size of camera scale
        [SerializeField]
        private float minSize;//the minimum

        //restricts the translation in some area
        public void Translate(Vector3 direct)
        {
            this.cam.transform.Translate(direct);
            if (sceneBorder != null)
                AdjustPos();
        }


        //we always adjust position in order to keep the camera in scene.The sceneBorder should always be non-null.
        void AdjustPos()
        {
            var border = CamUtility.BoundToRect(sceneBorder.bounds);
            var camBorder = CamUtility.GetCamViewRect(cam);

            var y_diff = 0f;
            var x_diff = 0f;

            if (camBorder.yMax < border.yMin)
                y_diff = border.yMin - camBorder.yMax;
            else if (camBorder.yMin > border.yMax)
                y_diff = border.yMax - camBorder.yMin;

            if (camBorder.xMin < border.xMin)
                x_diff = border.xMin - camBorder.xMin;
            else if (camBorder.xMax > border.xMax)
                x_diff = border.xMax - camBorder.xMax;


            var mov_vec = new Vector3(x_diff, y_diff, 0);

            this.cam.transform.Translate(mov_vec);
        }


        public void Scale(float scaleDiff)
        {
            var endScale = this.cam.orthographicSize + scaleDiff;
            //ensures scale between a valid range
            endScale = Mathf.Clamp(endScale, minSize, maxSize);
            this.cam.orthographicSize = endScale;

            //this ensures no null-reference exception
            if (sceneBorder != null)
                AdjustPos();
        }

        public void MoveAndScale() {
            var pan = new Vector3(
                    Input.GetAxis("Horizontal"),
                    Input.GetAxis("Vertical"),
                    0
                ).normalized;
            var scale= -Input.GetAxis("Mouse ScrollWheel");


            this.Translate(pan * panSpeed * Time.deltaTime);
            this.Scale(scale * scaleRate * Time.deltaTime);
        }

    }
}