using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyScripts : MonoBehaviour
{
    public float speed = 3f;
    public float rotate_Speed = 100f;

    public bool canShoot;
    public bool canRotate;
    private bool canMove = true;

    public float bound_Y = -6f;
    //float r = 4;
    public Transform attack_Point;
    public GameObject bulletPrefab;

    private Animator anim;
    private AudioSource explosionSound;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (canRotate)
        {
            rotate_Speed = Random.Range(rotate_Speed, rotate_Speed + 50f);
            rotate_Speed *= -1;
        }

        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 3f));

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateEnemy();
    }

    void Move()
    {
        if (canMove)
        {

            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            transform.position = temp;
            //Vector3 tempo = transform.position;
            //if (canShoot)
            //{

                //r--;
                //Vector3 tempo = transform.position;
                //if (r < 2)
                //temp.x -= speed * Time.deltaTime;
                //if(r > 2)
                //temp.x += speed * Time.deltaTime;
                //if (r == 0)
                //r = 4;
                //temp.x -= speed * Time.deltaTime;
                //if (temp.x < -2.1)
                    //temp.x -= speed * Time.deltaTime;
                //if (temp.x > 2.1)
                    //temp.x += speed * Time.deltaTime;
                //transform.position = temp;
            //}

            if (temp.y < bound_Y)
                gameObject.SetActive(false);
            
                
        }
    }

    void RotateEnemy()
    {
        if (canRotate)
        {
            transform.Rotate(new Vector3(0f, 0f, rotate_Speed*Time.deltaTime), Space.World);
        }
    }

    void StartShooting()
    {

        GameObject bullet = Instantiate(bulletPrefab, attack_Point.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().is_EnemyBullet = true;

        if (canShoot)
            Invoke("StartShooting", Random.Range(0f, 3f));

    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Bullet1")
        {

            canMove = false;

            if (canShoot)
            {
                canShoot = false;
                CancelInvoke("StartShooting");
            }

            Invoke("TurnOffGameObject", 0.5f);

            //play explosion sound
            explosionSound.Play();
            anim.Play("Destroy");

        }
    }

}