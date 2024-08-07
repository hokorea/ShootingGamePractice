using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy
{
    float curMoveTime;
    public float moveTime;
    public float rotateSpeed;

    float curWaitTime;
    public float waitTime;

    Vector3 dir;

    public override void Attack()
    {
        if (!canAttack) return;

        curWaitTime += Time.deltaTime;

        if (curWaitTime < waitTime - 0.5f)
        {
            if (target != null)
            {
                Vector2 direction = new Vector2(
                    transform.position.x - target.position.x,
                    transform.position.y - target.position.y
                );

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
                transform.rotation = rotation;
            }

            dir = (target.position - transform.position).normalized;
        }
        else if(curWaitTime >= waitTime)
        {

            transform.position += dir * 20 * Time.deltaTime;
        }
    }

    public override void Move()
    {
        //정해진 시간만큼 이동 후 canAttack을 활성화
        if (curMoveTime >= moveTime) 
        { 
            canAttack = true;
            return;
        }

        //타이머
        curMoveTime += Time.deltaTime;
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
