using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 此文件为Enemy模块暴露给外界的接口。States表示受到伤害后对象的状态，Damageable表示
 实现了ReceiveDamage方法的组件。类型继承该类，以表示自己可以接收伤害。
     
     */


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
