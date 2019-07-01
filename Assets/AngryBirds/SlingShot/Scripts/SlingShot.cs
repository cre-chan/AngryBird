using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SlingShot : MonoBehaviour{
	public float factor=1.0f;//this is used to adjust the strength of shooter

	public Transform positioner;//the center of slingshot in local space
	private bird bullet;//The bird that can be emitted from slingshot

	public float max_strength;//The shooting strength 
	public float dead_sterngth;//the shooting strength below this won't make sense

	//used to indicate the strength used to shoot the bird.
	//It restricts vec's magnitude to a range.
	public struct Strength{
		//allows read only
		public Vector2 vec{
			get{ 
				return _vec;
			}
		}
		Vector2 _vec;

		public Strength(Vector2 vec_,float min,float max){
			if (vec_.magnitude<min)
				this._vec=new Vector2(0,0);
			else if (vec_.magnitude>max){
				vec_.Normalize();
				this._vec=vec_*max;
			}
			else
				this._vec=vec_;
		}

		//if the vec is clamped?
		public bool isDead(){
			return vec == new Vector2 (0, 0);
		}


	}

	void Start(){
	}

	//load the bird onto SlingShot. The slingshot's bullet
	public void load (bird bullet)
	{
		this.bullet = bullet;
		this.bullet.physicsLock = true;
		this.bullet.transform.position = positioner.position;
	}

	//unsafe type 
	public void emit(ref Strength direction){
		if (this.bullet != null) {
			this.bullet.physicsLock = false;//unlock
			this.bullet.shoot (direction.vec * factor);
			this.bullet = null;
		}
	}
		
	// some controller function. Emit the bird when released
    private void OnMouseUp()
    {
		//calculate the click position in world. This ensures shooting strength irrelevant to 
		var clickpos =Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector3 delta3D = positioner.transform.position-clickpos;
		Vector2 delta2D = new Vector2 (delta3D.x, delta3D.y);

		//float strength=delta2D.magnitude;
		var strength=new Strength(delta2D,dead_sterngth,max_strength);
		if (!strength.isDead())
			emit(ref strength);
    }

}
