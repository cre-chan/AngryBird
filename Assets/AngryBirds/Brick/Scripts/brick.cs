using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour {

    public float HP;
    public float DamageRate;
    public GameObject brickBoom;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
            return;
        float damage = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * DamageRate;
        HP -= damage;
        if(HP<=0)
        {
            Instantiate(brickBoom, transform.position, transform.rotation);
            Destroy(gameObject);
           


        }
    }
}