using UnityEngine;
using Utility;
using Controllers;

//this file handles the interaction and some nasty scripts,
partial class SlingShot {
    [SerializeField]
    private CentralControl supervisor;

    //used to indicate the strength used to shoot the bird.
    //It restricts vec's magnitude to a range.
    private struct Strength
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
        //Security check!!!!!
        if (supervisor == null)
            Debug.LogError(
                "Supervisor not found. Consider fix this by link a MyCamera" +
                " to the supervisor attribute."
                );
        
        if (factor <= 0)
            Debug.LogError("Factor less or equal to 0.0f. This may results in undefined behaviour");

        if (max_strength <= dead_sterngth)
            Debug.LogError("Having max_strength less than dead_strength");

        var localClickpos = this.transform.InverseTransformPoint(positioner.position);
        this.GetComponent<LineRenderer>().SetPosition(1, localClickpos);
    }

    //enables the slingshot control to get bird from some source
    private Bird GetBird() {
        return supervisor.FetchBird();
    }


    //load when mouse clicked
    private void OnMouseDown() {
       
        var next_bird = this.GetBird();

        if (next_bird != null){
            var temp = new Existing<Bird>(next_bird);
            this.Load(temp);

        }
    }

    private void OnMouseDrag() {
        //calculate a vector poting from clickpos to positioner
        var clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var delta3D = positioner.transform.position - clickpos;
        var delta2D = new Vector2(delta3D.x, delta3D.y);

        var localClickpos= this.transform.InverseTransformPoint(clickpos);
        this.GetComponent<LineRenderer>().SetPosition(1, localClickpos);

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
                supervisor.Watch( new Existing<Bird>(bird) );
           
        }
        else
            this.Revert();

        var localClickpos = this.transform.InverseTransformPoint(positioner.position);
        this.GetComponent<LineRenderer>().SetPosition(1, localClickpos);
    }
}
