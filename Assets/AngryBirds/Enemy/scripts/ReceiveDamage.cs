using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public enum States
    {
        alive,
        dead
    }

    //表示可以接收伤害的组件，作为类型标记存在
    abstract public class Damageable : MonoBehaviour
    {
        public abstract States ReceiveDamage(Collision2D collision);
    }

    

}
