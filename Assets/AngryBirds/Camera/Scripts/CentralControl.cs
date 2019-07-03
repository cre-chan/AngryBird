using System.Collections;
using System.Collections.Generic;
using UnityEngine;



partial class MyCamara {
    private Bird activeBird;//the currently on-fly bird, add by Watch()
    public float deadSpeed;
    public SlingShot shooter;
    private Queue<Bird> birds;//initialized at Start(), all the birds in the scene
                              //access via fetchBird(), the count of birds are used to indicate failure
    FnOnce finalAction=new FnOnce();
    public enum States
    {
        Free,
        OnWatch
    }

    //tells wehter the current state is free mode or onWatch
    States CurrentState
    {
        get
        {
            return activeBird == null ? States.Free : States.OnWatch;
        }
    }


    //a structure indicating to what extent the camera move and scale
    class Move
    {
        public Vector3 pan;//indicates the direction a camera should go (normalized)
        public float scale;//indicates how much the camera's view should scale
        public bool clickOnAir;//

        //get those nasty input done.
        public Move(MyCamara cam)
        {
            this.pan = new Vector3(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0
            );
            this.pan.Normalize();
            this.clickOnAir = cam.CurrentState == States.OnWatch && Input.GetMouseButton(0);
            this.scale = -Input.GetAxis("Mouse ScrollWheel");
        }
    }

    //used to setup the whole scene
    void Start()
    {
        var all_birds = GameObject.FindGameObjectsWithTag("bird");
        this.birds = new Queue<Bird>(all_birds.Length);
        foreach (var bird in all_birds)
        {
            this.birds.Enqueue(bird.GetComponent<Bird>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //get some input
        var mov = new Move(this);

        this.Translate(mov.pan * panSpeed * Time.deltaTime);
        this.Scale(mov.scale * scaleRate * Time.deltaTime);
        if (mov.clickOnAir)
            activeBird.Superpower();
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

        if (this.Fail()){
            finalAction.call(
                () => Debug.Log("You Failed!")
                );
            return;
        }

        if (this.Win()) {
            finalAction.call(
                () => Debug.Log("You Won!")
                );
            return;
        }

    }


   //U cannot watch a bird that does not exist
   public void Watch(Existence<Bird> some_bird) {
        if (this.CurrentState == States.Free) {
            activeBird = some_bird.Unwrap();
            shooter.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Watch bird");
        }
    }


    public void UnWatch() {
        if (this.CurrentState == States.OnWatch){
            activeBird = null;
            shooter.GetComponent<Collider2D>().enabled = true;
            Debug.Log("Unwatched bird");
        }
    }


    public Bird FetchBird() {
        if (this.birds.Count <= 0)
            return null;
        else
            return this.birds.Dequeue();
    }

    //You win only when there's no pig left
    private bool Win() {
        return GameObject.FindGameObjectsWithTag("pig").Length == 0;
    }

    private bool Fail() {
        return this.CurrentState==States.Free&& this.birds.Count <= 0;
    }
}