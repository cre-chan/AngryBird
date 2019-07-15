using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBird : Bird {
    [SerializeField]
    private float acceleration;

    public override void Start()
    {

        //the bird's default superpower is to shout a phrase
        this._superpower = new ActionOnce(
            new Existence<Action>(() => {
                var direction = this.GetComponent<Rigidbody2D>().velocity.normalized;
                var acc = acceleration * direction;
                this.GetComponent<Rigidbody2D>().AddForce(acc, ForceMode2D.Impulse);
            } )
            );

        this.isDead = false;
    }
}
