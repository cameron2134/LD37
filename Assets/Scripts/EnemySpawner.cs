using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyObj;

    public List<GameObject> spawners = new List<GameObject>();


    private void SpawnEnemy() {

        int index = Random.Range(0, spawners.Count);
  

        GameObject obj = (GameObject)Instantiate(enemyObj, spawners[index].transform);

        obj.transform.position = spawners[index].transform.position;


    }



	// Use this for initialization
	void Start () {

        InvokeRepeating("SpawnEnemy", 0f, 2f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
