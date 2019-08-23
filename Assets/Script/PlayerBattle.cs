using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    public AudioSource BattleAudio;
    public AudioClip Music;
    public AudioClip[] MagicSound;

    private AudioSource magicsfx;

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
        magicsfx = GetComponent<AudioSource>();
        StartCoroutine(BattleMusic());
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
                StartCoroutine(MagicCasting());
                Instantiate(Attacks[0], new Vector3(isFlipped ? transform.position.x - 3 : transform.position.x + 3, transform.position.y, transform.position.z), Quaternion.identity).transform.Rotate(0, isFlipped ? 180 : 0, 0);
                fireRateDelay = 0;
                MagicSoundCast(0);
            }
            else if (Manager.CurrentType == GameManager.PlayerFilter.Water && fireRateDelay >= 3f)
            {
                GameManager.Instance.PlayerHealth += 15f;
                fireRateDelay = 0;
                MagicSoundCast(1);
                StartCoroutine(MagicCasting());
                Instantiate(Attacks[1], transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
            }
            else if (Manager.CurrentType == GameManager.PlayerFilter.Earth && fireRateDelay >= 0.5f)
            {
                StartCoroutine(MagicCasting());
                Instantiate(Attacks[2], transform.position, Quaternion.identity).GetComponent<Projectile>().setDir((isFlipped ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0)));
                fireRateDelay = 0;
                MagicSoundCast(2);

            }
            else if (Manager.CurrentType == GameManager.PlayerFilter.Air && fireRateDelay >= 2f)
            {
                StartCoroutine(MagicCasting());
                Instantiate(Attacks[3], transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).GetComponent<Projectile>().setDir((isFlipped ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0)));
                fireRateDelay = 0;
                MagicSoundCast(3);

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

    private void MagicSoundCast(int MagicNumber)
    {
        //magicsfx.clip = MagicSound[MagicNumber];
        magicsfx.PlayOneShot(MagicSound[MagicNumber]);
    }

    //Testing Battle Music
    public IEnumerator BattleMusic()
    {
        BattleAudio.Play();
        yield return new WaitForSeconds(BattleAudio.clip.length);
        BattleAudio.clip = Music;
        BattleAudio.Play();
    }

    IEnumerator MagicCasting()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5F);
        anim.SetBool("Attack", false);
    }
}