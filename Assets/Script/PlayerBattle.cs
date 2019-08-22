using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
To Do For This Script:
- Bug Test
- 2D Animation
 */
public class PlayerBattle : MonoBehaviour
{
    private GameManager Manager;

    public static PlayerBattle Instance;
    public GameObject[] Attacks;
    public float speed;
    public bool isGrounded;
    private bool isFlipped;
    private float fireRateDelay;

    private Animator anim;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Manager = GameManager.Instance;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        fireRateDelay += Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            isFlipped = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            isFlipped = true;
        }

        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * speed * Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Manager.CurrentType == GameManager.PlayerFilter.Fire && fireRateDelay >= 1.5f)
            {
                Instantiate(Attacks[0], new Vector3(isFlipped ? transform.position.x - 3 : transform.position.x + 3, transform.position.y, transform.position.z), Quaternion.identity).transform.Rotate(0, isFlipped ? 180 : 0, 0);
                fireRateDelay = 0;
                StartCoroutine(MagicCasting());
            }
            else if (Manager.CurrentType == GameManager.PlayerFilter.Water && fireRateDelay >= 3f)
            {
                GameManager.Instance.PlayerHealth += 15f;
                fireRateDelay = 0;
                StartCoroutine(MagicCasting());
                Instantiate(Attacks[1], transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
            }
            else if (Manager.CurrentType == GameManager.PlayerFilter.Earth && fireRateDelay >= 0.5f)
            {
                Instantiate(Attacks[2], transform.position, Quaternion.identity).GetComponent<Projectile>().setDir((isFlipped ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0)));
                fireRateDelay = 0;
                StartCoroutine(MagicCasting());
            }
            else if (Manager.CurrentType == GameManager.PlayerFilter.Air && fireRateDelay >= 2f)
            {
                Instantiate(Attacks[3], transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).GetComponent<Projectile>().setDir((isFlipped ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0)));
                fireRateDelay = 0;
                StartCoroutine(MagicCasting());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            anim.SetBool("Jump", false);
        }
        else if (hit.gameObject.CompareTag("Enemy"))
        {
            if (hit.gameObject.GetComponent<EnemyBattle>().Health > 0)
            {
                GameManager.Instance.PlayerHealth -= hit.gameObject.GetComponent<EnemyBattle>().Damage;
            }
        }
    }
    void FixedUpdate()
    {
        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            isGrounded = false;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * 150);
            anim.SetBool("Jump", true);
        }
    }
    IEnumerator MagicCasting()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Attack", false);
    }
}