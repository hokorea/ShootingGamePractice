using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : Enemy
{
    float curMoveTime;
    public float moveTime;


    Transform firePoint;

    private void Start()
    {
        firePoint = transform.GetChild(0);
        canAttack = true;
    }

    public override void Attack()
    {
        if (!(curMoveTime >= moveTime && curMoveTime <= moveTime + 2)) return;
        if (!canAttack) return;

        canAttack = false;
        StartCoroutine(AttackDelay());

        int bulletCount = 20;

        for (int i = 0; i < bulletCount; i++)
        { 
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);

            firePoint.Rotate(Vector3.forward * (360 / bulletCount));

        }
        firePoint.Rotate(Vector3.forward * 9);

    }

    public override void Move()
    {
        curMoveTime += Time.deltaTime;
        if (curMoveTime >= moveTime && curMoveTime <= moveTime + 2) 
        {
            return; 
        }
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSecondsRealtime(shootDelay);
        canAttack = true;
    }

}
