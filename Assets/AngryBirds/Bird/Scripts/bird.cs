using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


//A bird consists of its MASS,Collider,superpower
//The Mass is defined in rigidbody as part of physics
//superpower is triggered when mouse clicked
[RequireComponent(typeof(Rigidbody2D))]
public abstract partial class Bird : MonoBehaviour
{

    public void Start() {
        //the bird's default superpower is to shout a phrase
        this._superpower = new ActionOnce(
            new Existing<Action>(this.BirdPower)
            );
        this.isDead = false;
    }

    public abstract void BirdPower();

    //used to lock and unlock as well as shoot the bird
    //the bird is physics-capable just because of rb2d
    private Rigidbody2D rb2d
    {
        get
        {
            return GetComponent<Rigidbody2D>();
        }
    }


    //to set if the bird is affected by physics. normally the bird will be locked when on ground or 
    //when on te slingshot
    public bool physicsLock
    {
        set
        {
            if (value)
                Debug.Log("lock");
            else
                Debug.Log("unlock");

            _physicsLock = value;
            rb2d.simulated = !value;
        }
        get
        {
            return _physicsLock;
        }
    }
    private bool _physicsLock;
    [HideInInspector]
    public bool isDead;


    //give an impulse to shoot the bird out. This makes its acceleration more natural
    public void Shoot(Vector2 force)
    {
        rb2d.AddForce(force, ForceMode2D.Impulse);
    }


    //triggered when mouse-left pressed
    public void UseBirdPower()
    {
        _superpower.Call();
    }
}
