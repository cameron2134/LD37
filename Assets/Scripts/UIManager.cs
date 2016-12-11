using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text killCountText;


    private void UpdateKillCountText() {

        killCountText.text = (int.Parse(killCountText.text) + 1).ToString();

    }


    void OnDisable() {
        GameManager.Instance.EnemyWasKilled -= UpdateKillCountText;
    }


	// Use this for initialization
	void Start () {

        GameManager.Instance.EnemyWasKilled += UpdateKillCountText;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
