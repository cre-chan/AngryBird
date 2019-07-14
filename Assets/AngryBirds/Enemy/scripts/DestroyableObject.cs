using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace Enemy
{
    [Serializable]
    public struct DestroyableObject
    {
        public float HP;

        //各种物体和本物体之间发生碰撞有不同的rate，会导致不同的伤害系数
        public float groundRate;
        public float birdRate;
        public float brickRate;
        public float pigRate;




        //根据tag选择合适的rate
        private float ChooseRate(Collision2D collision)
        {
            string othersTag = collision.gameObject.tag;     //获得对方的tag
            switch (othersTag)
            {
                case "bird": return birdRate;
                case "ground": return groundRate;
                case "brick": return brickRate;
                case "pig": return pigRate;
                default:
                    {
                        Debug.Log("碰撞时碰到了意外的gameobject，请检查猪的碰撞类型的switch是否覆盖所有物体。本次碰撞不进行计算");
                        return 0.0f;
                    }
            }
        }


        //计算碰撞时对方所携带的能量
        public float CalculateEnergy(Collision2D collision)
        {
            float relativeSpeed = collision.relativeVelocity.magnitude;//碰撞的相对速度的大小
            float mass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
            float energy = mass * relativeSpeed * relativeSpeed / 2;//动能
            return energy;
        }


        public float CalculateHP(Collision2D collision, float rate, float HP)
        {
            float energy = CalculateEnergy(collision);
            HP -= energy * rate;
            return HP;
        }

        //
        public States ReceiveDamage(Collision2D collision)
        {
            float damageRate = ChooseRate(collision);//设一个要被计算的rate.通过switch来判断要用哪个rate.如果都不匹配就传0.0不会有任何的计算

            HP = this.CalculateHP(collision, damageRate, HP);//计算hp减损

            if (HP <= 0)
                return States.dead;


            return States.alive;
        }
    }
}