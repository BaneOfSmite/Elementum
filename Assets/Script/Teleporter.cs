using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool isEnding;
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            if (isEnding)
            {
                GameManager.Instance.GameOver();
				ChangeMusic(4);
            }
            hit.transform.position = transform.GetChild(0).transform.position;

            if (gameObject.name.Contains("FireRealm"))
            {
                ChangeMusic(0);
            }
            else if (gameObject.name.Contains("WaterRealm"))
            {
                ChangeMusic(1);
            }
            else if (gameObject.name.Contains("EarthRealm"))
            {
                ChangeMusic(2);
            }
            else if (gameObject.name.Contains("AirRealm"))
            {
                ChangeMusic(3);
            }
        }
    }
    public void ChangeMusic(int clipNumber)
    {
        GameManager.Instance.source.clip = GameManager.Instance.music[clipNumber];
        GameManager.Instance.source.Play();
    }
}