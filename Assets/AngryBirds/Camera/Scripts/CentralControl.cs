using System.Collections;
using System.Collections.Generic;
using UnityEngine;



partial class MyCamara {
#if !DEBUG
    private Bird activeBird;//the currently on-fly bird, add by Watch()
    private float dead;

    public SlingShot shooter;
#else
    public Bird activeBird;//the currently on-fly bird, add by Watch()
    public float deadSpeed;
    public SlingShot shooter;

    public enum States {
        Free,
        OnWatch
    }

    States CurrentState
    {
        get {
            return activeBird == null ? States.Free : States.OnWatch;
        }

    }

#endif
    //used to setup the whole scene
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //get some input
        var mov = new Move();

        this.Translate(mov.pan * panSpeed * Time.deltaTime);
        this.Scale(mov.scale * scaleRate * Time.deltaTime);
    }

    void FixedUpdate() {
        //judge if the bird is within
        if (activeBird != null){
            var sceneBound = sceneBorder.bounds;
            var scenceRect = Utility.BoundToRect(sceneBound);
            var birdPos = new Vector2(
                activeBird.transform.position.x,
                activeBird.transform.position.y
                );
            //if the bird goes out of scene or it just reached some low speed, stop watch
            if ( !scenceRect.Contains(birdPos)||
                activeBird.GetComponent<Rigidbody2D>().velocity.magnitude<deadSpeed)
                this.UnWatch();

        }

    }


   //U cannot watch a bird that does not exist
   public void Watch(Existence<Bird> some_bird) {
        if (this.CurrentState == States.Free) {
            activeBird = some_bird.Unwrap();
            shooter.GetComponent<Collider2D>().enabled = false;
            Debug.Log("watch bird");
        }
    }


    public void UnWatch() {
        if (this.CurrentState == States.OnWatch){
            activeBird = null;
            shooter.GetComponent<Collider2D>().enabled = true;
            Debug.Log("Unwatched bird");
        }
    }
}