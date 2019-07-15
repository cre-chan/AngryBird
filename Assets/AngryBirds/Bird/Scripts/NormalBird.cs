using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBird : Bird{

    public override void Start(){
        //the bird's default superpower is to shout a phrase
        this._superpower = new ActionOnce(
            new Existence<Action>(() => Debug.Log("Su--per--Po--wer!!"))
            );
        this.isDead = false;
    }
}
