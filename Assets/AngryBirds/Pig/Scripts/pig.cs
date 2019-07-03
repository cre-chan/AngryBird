using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class pig : MonoBehaviour {

    public float HP;
    public float DamageRate;
    public GameObject pigBoom;
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

        if(collision.gameObject.tag=="bird")
        {
            Instantiate(pigBoom, transform);
            Destroy(gameObject);
           
            
            //此处应生成动画
        }
        else
        {
            float damage = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * DamageRate;
            HP -= damage;
            if(HP<=0)
            {
                Instantiate(pigBoom, transform);
                Destroy(gameObject);
              
            }
        }
    }
}