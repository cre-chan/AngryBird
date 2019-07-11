using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class pig : MonoBehaviour {

    public float HP;
    public float DamageRate;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
            return;

        float damage = Mathf.Pow(collision.rigidbody.velocity.magnitude,2) * collision.rigidbody.mass * DamageRate;
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);

        }
    }
}