using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy;

public class brick : Damageable{
    [SerializeField]
    private DestroyableObject destroyablePart;

    public override States ReceiveDamage(Collision2D collision)
    {

        return destroyablePart.ReceiveDamage(collision);
    }
}