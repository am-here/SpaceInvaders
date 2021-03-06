using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 2f;
    public float deactivate_Timer = 4f;

    [HideInInspector]
    public bool is_EnemyBullet = false;

    // Start is called before the first frame update
    void Start()
    {

        if (is_EnemyBullet)
            speed *= -1f;

        Invoke("DeactivateGameObject", deactivate_Timer);

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Bullet1" || target.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }

    }

} // class
