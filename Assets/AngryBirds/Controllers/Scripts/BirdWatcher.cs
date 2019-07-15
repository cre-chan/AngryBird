using System;
using UnityEngine;
using Utility;


namespace Controllers
{
    [Serializable]
    struct BirdWatcher
    {
        [HideInInspector]
        private Bird birdUnderWatch;
        [SerializeField]
        private float deadSpeed;
        [SerializeField]
        private Collider2D sceneBorder;


        public enum States
        {
            Free,
            OnWatch
        }

        //tells wehter the current state is free mode or onWatch
        public States CurrentState
        {
            get { return BirdUnderWatch == null ? States.Free : States.OnWatch; }
        }

        public Bird BirdUnderWatch {
            get { return birdUnderWatch; }
        }

        public void Watch(Existing<Bird> some_bird)
        {
            if (this.birdUnderWatch==null)
            {
                this.birdUnderWatch = some_bird.Unwrap();
                Debug.Log("Watch bird");
            }
        }

        //三元逻辑
        public bool? BirdWithinScene() {

            if (birdUnderWatch != null)
            {
                var sceneBound = sceneBorder.bounds;
                var scenceRect = CamUtility.BoundToRect(sceneBound);
                var birdPos = new Vector2(
                    birdUnderWatch.transform.position.x,
                    birdUnderWatch.transform.position.y
                    );
                //if the bird goes out of scene or it just reached some low speed, stop watch
                return scenceRect.Contains(birdPos) && birdUnderWatch.GetComponent<Rigidbody2D>().velocity.magnitude >= deadSpeed;

            }
            else {
                return new bool?();
            }

        }

        public void UnWatch()
        {
            if (this.birdUnderWatch != null)
            {
                this.birdUnderWatch = null;
                Debug.Log("Unwatched bird");
            }
        }
    }
}
