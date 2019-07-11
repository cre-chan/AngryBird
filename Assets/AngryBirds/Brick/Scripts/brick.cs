using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour {

    public float HP;
    public float DamageRate;
	// Use this for initialization

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody == null)
            return;
        float damage =Mathf.Pow(collision.rigidbody.velocity.magnitude,2) * collision.rigidbody.mass * DamageRate;
        HP -= damage;
        if(HP<=0)
        {
            
            Destroy(gameObject);
           


        }
    }
}