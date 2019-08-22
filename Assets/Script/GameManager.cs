using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
To Do For This Script:
- Bug Test
 */
public class GameManager : MonoBehaviour {
    public enum PlayerFilter { Air, Water, Earth, Fire, Lightning, None }
    public float PlayerHealth, PlayerMaxMana = 100, CurrentMana = 100;
    public static GameManager Instance;
    public GameObject EarthPuzzleSpike;
    public bool isBattle, PuzzleFail;
    public Image Filter;
    public List<PlayerFilter> Have;
    public Color[] FilterColor;
    public PlayerFilter CurrentType;
    private int cycle = 0;
    public GameObject[] Scenes, Battle, BattleBG, GameOverVillage;
    private GameObject EnemyTrigger;
    public int EnemiesLeft = 0, ObjectivesLeft, PuzzleBlockLeft = 3, ObjectiveCollected = 0;
    public TextMeshProUGUI _text;
    public GameObject gmUI;
    void Awake() {
        gmUI.SetActive(false);
        Time.timeScale = 1;
        if (Instance == null) {
            Instance = this;
        }
    }
    void Update() {
        if (!PuzzleFail && PuzzleBlockLeft <= 0 && EarthPuzzleSpike.transform.position.y >= 352f) {
            EarthPuzzleSpike.transform.position -= new Vector3(0, 1, 0) * 5 * Time.deltaTime;
        }
        if (PlayerHealth <= 0 || PlayerController.Instance.gameObject.transform.position.y <= -185f) {
            Dead();
        }
        if (CurrentType != PlayerFilter.None) {
            CurrentMana -= Time.deltaTime * 2.5f;
            if (CurrentMana <= 0) {
                CurrentMana = 0f;
                cycle = 0;
                CurrentType = Have[cycle];
                Filter.color = FilterColor[(byte) CurrentType];
            }
        } else {
            if (CurrentMana <= PlayerMaxMana) {
                CurrentMana += Time.deltaTime;
                if (CurrentMana >= PlayerMaxMana) {
                    CurrentMana = 100f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            if (cycle != (Have.ToArray().Length - 1)) {
                cycle++;
            } else {
                cycle = 0;
            }
            CurrentType = Have[cycle];
            Filter.color = FilterColor[(byte) CurrentType];
        }
    }
    public void TriggerBattle(EnemyBattle.EnemyType Type, GameObject Triggerer, int amt) {
        int _type = 0;
        foreach (GameObject i in BattleBG) {
            i.SetActive(false);
        }
        switch (Type) {
            case EnemyBattle.EnemyType.Air:
                BattleBG[0].SetActive(true);
                _type = 3;
                break;
            case EnemyBattle.EnemyType.Water:
                BattleBG[1].SetActive(true);
                _type = 4;
                break;
            case EnemyBattle.EnemyType.Fire:
                BattleBG[2].SetActive(true);
                _type = 5;
                break;
            case EnemyBattle.EnemyType.Earth:
                BattleBG[3].SetActive(true);
                _type = 6;
                break;
        }
        amt++;
        Scenes[0].SetActive(false);
        Scenes[1].SetActive(true);
        isBattle = true;
        EnemyTrigger = Triggerer;
        Battle[0].transform.position = Battle[1].transform.position;
        for (int i = 0; i < amt; i++) {
            Instantiate(Battle[_type], Battle[2].transform.position, Quaternion.identity, Scenes[1].transform).GetComponent<EnemyBattle>().Type = Type;
        }
    }
    public void BattleEnd(GameObject _Enemy) {
        Destroy(EnemyTrigger);
        Destroy(_Enemy);
        EnemiesLeft = 0;
        Scenes[1].SetActive(false);
        Scenes[0].SetActive(true);
    }

    public void Dead() {
        PlayerHealth = 0;
        _text.text = "You Died";
        PlayerController.Instance.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        PlayerController.Instance.enabled = false;
        Time.timeScale = 0;
    }

    public void GameOver() {
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Teleporters")) {
            i.SetActive(false);
        }
       
        gmUI.SetActive(true);
        if (ObjectivesLeft <= 0) {
            _text.text = "You Win";
        } else {
            _text.text = "You Lose";
            GameOverVillage[0].SetActive(false);
            GameOverVillage[1].SetActive(true);
        }
    }
}