using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalController : MonoBehaviour {


    public GameObject portalObj;
    public List<GameObject> portalSpawnPoints;


    private const int PORTAL_LIMIT = 6;

    private List<GameObject> activePortals = new List<GameObject>();
    private List<int> usedPortalSpawns = new List<int>();



    private void SpawnPortal() {


        if (activePortals.Count < PORTAL_LIMIT) {

            int index = Random.Range(0, portalSpawnPoints.Count);
            Debug.Log("Spawning portal at " + index);
            // spawn portal

            
            for (int x = 0; x < usedPortalSpawns.Count; x++) {
                if (index == usedPortalSpawns[x]) {
                    while (index == usedPortalSpawns[x]) {
                        index = Random.Range(0, portalSpawnPoints.Count);
                    }
                }
            }


            GameObject portal = (GameObject)Instantiate(portalObj, portalSpawnPoints[index].transform);
            portal.GetComponent<Portal>().SetIndex(index);

            portal.transform.position = portalSpawnPoints[index].transform.position;
            activePortals.Add(portal);
            

            usedPortalSpawns.Add(index);
        }

        else
            Debug.Log("PORTAL LIMIT REACHED");

    }




    // Remove the portal entry from the list, let a new one be spawned
    private void OnPortalDeath(GameObject portal) {

        Debug.Log("Returning " + portal.GetComponent<Portal>().GetIndex());
        activePortals.Remove(portal);
        usedPortalSpawns.Remove(portal.GetComponent<Portal>().GetIndex());
    }



    void OnDisable() {
        GameManager.Instance.PortalDied -= OnPortalDeath;
    }




	// Use this for initialization
	void Start () {

        GameManager.Instance.PortalDied += OnPortalDeath;

        Debug.Log("Portal spawn points: " + portalSpawnPoints.Count);
        InvokeRepeating("SpawnPortal", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

    

    }



    
}
