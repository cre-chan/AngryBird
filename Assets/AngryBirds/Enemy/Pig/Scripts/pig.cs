using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy;

[RequireComponent(typeof(Rigidbody2D))]
public class pig :Damageable{
    [SerializeField]
    private DestroyableObject destroyablePart;

    public override States ReceiveDamage(Collision2D collision) {

        return destroyablePart.ReceiveDamage(collision);
    }
}