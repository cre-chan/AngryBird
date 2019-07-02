using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 I was considering changing this slingshot to a state machine pattern.
 There're two states:unloaded and loaded

 unloaded converts to loaded via calling Load()
 loaded can call emit() to change itself into unloaded.
 also, under the loaded state, you can drag objects,revert states to loaded.
     
     */

public partial class SlingShot : MonoBehaviour{
	public float factor;//this is used to adjust the strength of shooter

	public Transform positioner;//the center of slingshot in local space
	private Bird bullet;//The bird that can be emitted from slingshot

	public float max_strength;//The shooting strength 
	public float dead_sterngth;//the shooting strength below this won't make sense
    public bool IsLoaded {
        get {
            return this.bullet != null;
        }
    }

	


	//load the bird onto SlingShot. The slingshot's bullet cannot be NULL!!!
    //The existence simplify the implemantation. It ensures the load to change states.
	public void Load (Existence<Bird> bullet)
	{
        //since the bullet reflects the state, this ensures call only works under unloaded state
        if (!this.IsLoaded) {
            this.bullet = bullet.Unwrap();
            this.bullet.physicsLock = true;
            this.bullet.transform.position = positioner.position;
        }
		
	}


    //dragDelta is a vector pointing from clickpos to positioner
    public void Drag(Vector2 dragDelta) {

        if (this.IsLoaded) {
            var strength = new Strength(dragDelta, 0f, max_strength);
            this.bullet.transform.position = new Vector3(
                positioner.transform.position.x - strength.vec.x,
                positioner.transform.position.y - strength.vec.y,
                0
                );
        }
    }

    public void Revert()
    {
        if(this.IsLoaded)
            this.bullet.transform.position = positioner.position;
    }

	public void Emit(ref Strength direction){
		if (this.IsLoaded) {
			this.bullet.physicsLock = false;//unlock
			this.bullet.Shoot (direction.vec * factor);
			this.bullet = null;
		}
	}
	
}
