using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this file handles the interaction and some nasty scripts,
partial class SlingShot {
    public MyCamara _supervisor;
    private Queue<Bird> ammo;

    //used to indicate the strength used to shoot the bird.
    //It restricts vec's magnitude to a range.
    public struct Strength
    {
        //allows read only
        public Vector2 vec
        {
            get
            {
                return _vec;
            }
        }
        Vector2 _vec;

        public Strength(Vector2 vec_, float min, float max)
        {
            if (vec_.magnitude < min)
            {
                this._vec = new Vector2(0, 0);
            }
            else if (vec_.magnitude > max)
            {
                vec_.Normalize();
                this._vec = vec_ * max;
            }
            else
                this._vec = vec_;
        }

        //if the vec is clamped?
        public bool IsDead()
        {
            return vec == new Vector2(0, 0);
        }


    }


    void Start() {
        var all_birds=GameObject.FindGameObjectsWithTag("bird");
        ammo = new Queue<Bird>(all_birds.Length);
        foreach (var bird in all_birds) {
            ammo.Enqueue(bird.GetComponent<Bird>());
        }
    }


    //load when mouse clicked
    private void OnMouseDown() {
        if (this.ammo.Count == 0)
            return;
        var next_bird = this.ammo.Dequeue();
        if (next_bird!=null)
            this.Load(new Existence<Bird>(next_bird));
    }

    private void OnMouseDrag() {
        //calculate a vector poting from clickpos to positioner
        var clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var delta3D = positioner.transform.position - clickpos;
        var delta2D = new Vector2(delta3D.x, delta3D.y);


        this.Drag(delta2D);

    }


    // some controller function. Emit the bird when released
    private void OnMouseUp()
    {
        //calculate the click position in world. This ensures shooting strength irrelevant to 
        var clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var delta3D = positioner.transform.position - clickpos;
        var delta2D = new Vector2(delta3D.x, delta3D.y);
        

        var strength = new Strength(delta2D, dead_sterngth, max_strength);

        //if the strength is not too weak
        if (!strength.IsDead())
        {
            var bird = this.bullet;
            Emit(ref strength);
            if(bird!=null)
            _supervisor.Watch( new Existence<Bird>(bird) );
        }
        else
            this.Revert();
    }
}
