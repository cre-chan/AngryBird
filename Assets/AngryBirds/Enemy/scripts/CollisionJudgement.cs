using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Enemy;


interface ICom { }


//判断碰撞的发生，将碰撞转发给collidable对象，判断对象状态，并向计分器发送消息
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Damageable))]
public class CollisionJudgement : MonoBehaviour
{
    public ScoreTextController scoreText;
    //public Damageable obj;
    public int score;


    //如果有东西destroy就添加他对应的score
    public void AddScore(int score)
    {
        scoreText.AddScore(score);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = this.GetComponent<Damageable>();

        if (obj != null){
            var resultState= obj.ReceiveDamage(collision);
            if (resultState == States.dead) {
                this.AddScore(score);
                Destroy(this.gameObject);
            }
        }
        else {
            Debug.LogWarning("No Destroyable object attatched to the same object. Malfunction may occur");
        }
    }
}