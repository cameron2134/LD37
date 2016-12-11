using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapController : MonoBehaviour {

    public GameObject trapObj;

    public List<GameObject> platformList = new List<GameObject>();


    private const int TRAP_LIMIT = 2;

    private int activeTraps = 0;

    private GameObject tempPlatform;

    // Do something similar like with the portals. Replace a random platform with a trap. Have a limit of 2 active.


    private void SpawnTrap() {

        if (activeTraps < TRAP_LIMIT) {
            int index = Random.Range(0, platformList.Count);

            //platformList[index].SetActive(false);

            GameObject trap = (GameObject)Instantiate(trapObj, platformList[index].transform);
            trap.transform.position = platformList[index].transform.position;

            activeTraps++;
        }

        
    }



    private void OnTrapDeath() {
        activeTraps--;
    }



    void OnDisable() {
        GameManager.Instance.TrapExpired -= OnTrapDeath;
    }


	// Use this for initialization
	void Start () {

        GameManager.Instance.TrapExpired += OnTrapDeath;

        InvokeRepeating("SpawnTrap", 0f, 4f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
