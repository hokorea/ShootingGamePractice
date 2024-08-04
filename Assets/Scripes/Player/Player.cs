using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    [SerializeField] GameObject bulletPrefab;

    public float speed = 0;
    public float shootDelay;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    void Move() 
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0);

        transform.position += dir.normalized * speed * Time.deltaTime;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x > 1) pos.x = 1;
        else if (pos.x < 0) pos.x = 0;
        if (pos.y > 1) pos.y = 1;
        else if (pos.y < 0) pos.y = 0;

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if(Input.GetButtonDown("Horizontal")||Input.GetButtonUp("Horizontal"))
        {
            animator.SetInteger("Move", (int)x);
        }
    }

    void Shoot()
    {
        if (!Input.GetKey(KeyCode.Z)) return;
        if(!canShoot) return;
        
        canShoot = false;
        StartCoroutine(ShootDelay());

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15, ForceMode2D.Impulse);
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
