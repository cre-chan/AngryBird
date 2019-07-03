using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 I was considering changing this slingshot to a state machine pattern.
 There're two states:unloaded and loaded

 unloaded converts to loaded via calling Load()
 loaded can call emit() to change itself into unloaded.
 also, under the loaded state, you can drag objects,revert states to loaded.
     

 2019/7/3
 弹弓类的实现分布在SlingShot.cs和ReleaseControl.cs中。其中SlingShot.cs包括的主要是弹弓本身的属性及操作：
 @factor:表示弹力的系数，用于调节力度大小
 @positioner:链接到某空物体，用于表示弹弓瞄准的中心点(考虑在下个版本里移除，改用判定区中心，以保证同步)
 @bullet:表示弹弓发射的弹药。使用Load()会将bullet赋值，使用Emit()则会将bullet赋空。

 ###max_strength应总是大于dead_strength######
 @max_strength:最大的拉伸长度
 @dead_strength:最小的合法力度，低于此力度则不会发射

 @IsLoaded:返回是否上弹了。通常不用检查该属性，主要用于内部判断状态方便，明确语义。
 ...
 ReleaseControl.cs主要包含处理输入的部分。在Start()函数中进行了错误检查,对于不可修复的错误会报错，请遵守
 Error Log中的提示修改，以避免未定义行为。

    状态转换图大致如下:

                Emit()
    Loaded---------------|
    |                    |
    |                    |
    |                    |
    -----------------UnLoaded
    Load()
     */

public partial class SlingShot : MonoBehaviour{
    [SerializeField]
	private float factor;//this is used to adjust the strength of shooter
    [SerializeField]
    private Transform positioner;//the center of slingshot in local space
	private Bird bullet;//The bird that can be emitted from slingshot

    [SerializeField]
	private float max_strength;//The shooting strength 
    [SerializeField]
	private float dead_sterngth;//the shooting strength below this won't make sense
    private bool IsLoaded {
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

    private void Revert()
    {
        if(this.IsLoaded)
            this.bullet.transform.position = positioner.position;
    }


	private void Emit(ref Strength direction){
		if (this.IsLoaded) {
			this.bullet.physicsLock = false;//unlock
			this.bullet.Shoot (direction.vec * factor);
			this.bullet = null;
		}
	}
	
}
