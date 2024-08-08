using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyA : Enemy
{
    Transform firePoint;

    private void Start()
    {
        firePoint = transform.GetChild(0);
        canAttack = true;
    }
    public override void Attack()
    {
        if (!canAttack) return;
        //firepoint 각도 조절
        if (target != null)
        {
            Vector2 direction = new Vector2(
                transform.position.x - target.position.x,
                transform.position.y - target.position.y
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            firePoint.rotation = angleAxis;
        }
        firePoint.Rotate(Vector3.forward * 180);
        firePoint.Rotate(Vector3.forward * -15);

        canAttack = false;
        StartCoroutine(ShootDelay());

        //총알 발사
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);

            firePoint.Rotate(Vector3.forward * 15);
        }



    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSecondsRealtime(shootDelay);
        canAttack = true;
    }

    public override void Move()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
