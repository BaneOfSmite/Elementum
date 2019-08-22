using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
To Do For This Script:
- Bug Test
 */
public class ObjectiveRenderer : MonoBehaviour {
    public GameManager Manager;
    public bool IsFilter;

    void Start() {
        Manager = GameManager.Instance;
        Manager.ObjectivesLeft++;
    }
    void Update() {
        if (!IsFilter) {
			transform.GetChild(0).Rotate(new Vector3(1, 1, 1));
            transform.GetChild(1).Rotate(new Vector3(1, 1, 1));
            transform.GetChild(2).Rotate(new Vector3(-1, -1, -1));
            if (gameObject.name.Contains("Fire") && Manager.CurrentType == GameManager.PlayerFilter.Water) {
                SetInvis();
            } else if (gameObject.name.Contains("Water") && Manager.CurrentType == GameManager.PlayerFilter.Earth) {
                SetInvis();
            } else if (gameObject.name.Contains("Earth") && Manager.CurrentType == GameManager.PlayerFilter.Air) {
                SetInvis();
            } else if (gameObject.name.Contains("Air") && Manager.CurrentType == GameManager.PlayerFilter.Fire) {
                SetInvis();
            } else {
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                if (!IsFilter) {
                    transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                    transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
                }
            }
        } else {
			transform.Rotate(new Vector3(1, 1, 1));
		}
    }
    private void SetInvis() {
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        if (!IsFilter) {
            transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
        }
    }
}