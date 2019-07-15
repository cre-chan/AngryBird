using System.Collections.Generic;
using UnityEngine;
using System;

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
        private MyCamara sceneCamera;

        public void MoveAndScale() {
            var pan = new Vector3(
                    Input.GetAxis("Horizontal"),
                    Input.GetAxis("Vertical"),
                    0
                ).normalized;
            var scale= -Input.GetAxis("Mouse ScrollWheel");


            this.sceneCamera.Translate(pan * panSpeed * Time.deltaTime, sceneBorder);
            this.sceneCamera.Scale(scale * scaleRate * Time.deltaTime, sceneBorder);
        }

    }
}