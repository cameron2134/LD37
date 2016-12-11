using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    private int indexUsed;


    public void PortalTank(GameObject tank) {

        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");

        tank.transform.position = portals[Random.Range(0, portals.Length - 1)].transform.position;


    }



    public void SetIndex(int index) {
        this.indexUsed = index;
    }

    public int GetIndex() {
        return this.indexUsed;
    }


    // Portals only last short time
    private void Die() {

        GameManager.Instance.OnPortalDeath(this.gameObject);
        Destroy(this.gameObject);

    }



    // Use this for initialization
    void Start () {
        Invoke("Die", 8f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
