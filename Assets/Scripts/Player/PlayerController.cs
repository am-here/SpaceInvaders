using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private float speed = 10f;

    private Vector3 touchposi;
    private Rigidbody2D rb;
    private Vector3 direction;

    [SerializeField]
    private GameObject player_Bullet;

    [SerializeField]
    private Transform attack_Point;

    public float attack_Timer = 0.35f;
    private float current_Attack_Timer;
    private bool canAttack;

    private Animator anim;
    private AudioSource laserAudio;
    public AudioClip destroyClip;
    void Awake()
    {
        laserAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        rb = GetComponent<Rigidbody2D>();
        current_Attack_Timer = attack_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        anim.Play("newlife");
        Attack();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void MovePlayer()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchposi = Camera.main.ScreenToWorldPoint(touch.position);
            touchposi.z = 0;
            direction = (touchposi - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * speed;

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Attack()
    {

        attack_Timer += Time.deltaTime;
        if (attack_Timer > current_Attack_Timer)
        {
            canAttack = true;
        }

        //if (Input.touchCount > 0)
        //{
            if (canAttack)
            {

                canAttack = false;
                attack_Timer = -0.5f;

                Instantiate(player_Bullet, attack_Point.position, Quaternion.identity);

                //play the sound FX
                laserAudio.Play();

            }
        //}

    } // attack

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Bullet" || target.tag == "Enemy")
        {

            // prevent the player from attacking
            current_Attack_Timer = 1000f;
            laserAudio.clip = destroyClip;
            laserAudio.Play();
            anim.Play("Destroy");

            Invoke("DeactivateGameObject", 5f);

            

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        }
        if (target.tag == "Boost")
        {

        }

    }


} // class































