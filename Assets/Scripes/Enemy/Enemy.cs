using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform target;

    public int hp;
    public float shootDelay;
    public float moveSpeed;
    public float bulletSpeed;

    public bool canAttack;
    public bool canMove;

    protected void Update()
    {
        Attack();
        Move();
    }

    public virtual void Attack()
    {

    }

    public virtual void Move()
    {

    }

    public void Hit(int Damage)
    {
        hp -= Damage;
        if (hp <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    void DropItem()
    {

    }
}
