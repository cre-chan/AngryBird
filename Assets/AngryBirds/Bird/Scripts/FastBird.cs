using UnityEngine;
using Utility;

public class FastBird : Bird {
    [SerializeField]
    private float acceleration;


    public override void BirdPower()
    {
        var direction = this.GetComponent<Rigidbody2D>().velocity.normalized;
        var acc = acceleration * direction;
        this.GetComponent<Rigidbody2D>().AddForce(acc, ForceMode2D.Impulse);
    }
}
