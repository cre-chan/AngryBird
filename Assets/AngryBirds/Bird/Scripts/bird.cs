using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface birdBullet
{
    void ReLoad();//将鸟放到弹弓上并初始化
    void PullPoisition();//调整拉动的位置
    void ShootOut();//发射
    bool IsDead();//判断速度，如果过慢则返回true
}



public class bird : MonoBehaviour,birdBullet {

    public float maxDistance;
    public float speed;
    public float deadSpeed;
    public enum BirdState{ wait,load,pull,shoot,dead };
    public BirdState bs;

    private GameObject midPoint;
    // Use this for initialization
    void Start () {
        
	}

    public void ReLoad()
    {
        midPoint = GameObject.Find("shootpoint");
        transform.position = midPoint.transform.position;
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void PullPoisition()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
        if (Vector2.Distance(transform.position, midPoint.transform.position) > maxDistance)
        {
            Vector3 pos = transform.position - midPoint.transform.position;
            pos = pos.normalized * maxDistance;
            transform.position = pos + midPoint.transform.position;
        }
    }

    public void ShootOut()
    {
        Vector3 pos = transform.position - midPoint.transform.position;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = pos * speed;
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public bool IsDead()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude < deadSpeed)
            return true;
        else
            return false;
    }
	
}
