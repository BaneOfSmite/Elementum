using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
To Do For This Script: 
- Bug Test
 */
public class EnemyBattle : MonoBehaviour
{
    public float Damage, Health, speed;
    public enum EnemyType { Air, Water, Earth, Fire, Lightning, Minion }
    public EnemyType Type;
    public GameObject EarthMinion;
    public GameObject TEMP;
    public Sprite[] _Sprites;
    public SpriteRenderer _SR;
    public bool tookDmg;
    void Start()
    {
        _SR = GetComponent<SpriteRenderer>();
        StartCoroutine(Walking());
        if (Type != EnemyType.Minion)
        {
            GameManager.Instance.EnemiesLeft++;
        }
        switch (Type)
        { //Enemy Damage/Ability
            case EnemyType.Air:
                GetComponent<Rigidbody2D>().gravityScale = 0;
                InvokeRepeating("UncarnyEvasion", 1, 1f);
                Damage = 20;
                break;
            case EnemyType.Water:
                speed *= 1.25f;
                Damage = 20;
                break;
            case EnemyType.Earth:
                TEMP = new GameObject("Temp");
                TEMP.transform.position = transform.position;
                Health *= 2f;
                speed /= 2f;
                Damage = 15;
                InvokeRepeating("SummonMinion", 1, 1.5f);
                break;
            case EnemyType.Fire:
                Damage = 20;
                break;
            case EnemyType.Minion:
                Damage = 20;
                break;
                /*case EnemyType.Lightning:
					Damage = 0;
					break;*/
        }
    }
    void Update()
    {
        if (Health > 0)
        {
            Vector3 dir = PlayerBattle.Instance.gameObject.transform.position - transform.position;
            transform.position += dir.normalized * speed * Time.deltaTime;
			if (dir.x > 0)
			{
				_SR.flipX = true;
			}
			else if (dir.x < 0)
			{
				_SR.flipX = false;
			}
        }
        if (Health <= 0)
        {
            Health = 0;
            StartCoroutine(Death());
            if (Type == EnemyType.Earth)
            {
                Destroy(TEMP);
            }
        }
    }
    private void SummonMinion()
    {
        if (Health > 0)
        {
            Instantiate(EarthMinion, transform.position, Quaternion.identity, TEMP.transform);
        }
    }
    private void UncarnyEvasion()
    {
        if (GameObject.FindGameObjectWithTag("Projectile") != null)
        {
            TEMP = GameObject.FindGameObjectWithTag("Projectile");
            Vector3 dir = transform.position - GameObject.FindGameObjectWithTag("Projectile").transform.position;
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("Projectile").transform.position, transform.position) <= 3)
            {
                if (Random.Range(1, 3) == 1)
                {
                    GetComponent<Rigidbody2D>().AddForce(dir.normalized * 15f, ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            TEMP = null;
        }
    }
	public void ouch() {
		StartCoroutine(HitAnimation());
	}
    private IEnumerator HitAnimation()
    {
        if (!tookDmg)
        {
            tookDmg = true;
            _SR.sprite = _Sprites[2];
            yield return new WaitForSeconds(1);
		}
		print("D");
		tookDmg = false;
    }

    private IEnumerator Death()
    {
        _SR.sprite = _Sprites[3];
        yield return new WaitForSeconds(1);
        if (GameManager.Instance.EnemiesLeft <= 1)
        {
            GameManager.Instance.BattleEnd(gameObject);
        }
        else
        {
            GameManager.Instance.EnemiesLeft--;
            Destroy(gameObject);
        }
    }
    private IEnumerator Walking()
    {
        while (true)
        {
            if (!tookDmg && Health > 0)
            {
                _SR.sprite = _Sprites[0];
                yield return new WaitForSeconds(0.5f);
                _SR.sprite = _Sprites[1];
                yield return new WaitForSeconds(0.5f);
            }
			yield return new WaitForSeconds(1f);
        }
    }
}