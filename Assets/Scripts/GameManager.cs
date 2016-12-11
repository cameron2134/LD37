using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;


    public delegate void PortalIsDead(GameObject portal);
    public event PortalIsDead PortalDied;

    public void OnPortalDeath(GameObject portal) {

        if (PortalDied != null)
            PortalDied(portal);
        else
            Debug.Log("No subscribers to PirtalDied");

    }




    public delegate void TrapDied();
    public event TrapDied TrapExpired;

    public void OnTrapDeath() {

        if (TrapExpired != null)
            TrapExpired();
        else
            Debug.Log("No subscribers to TrapExpired");

    }




    public delegate void PlayerDeath();
    public event PlayerDeath PlayerDied;

    public void OnPlayerDied() {

        if (PlayerDied != null)
            PlayerDied();
        else
            Debug.Log("No subscribers to PlayerDied");

    }



    public delegate void EnemyKilled();
    public event EnemyKilled EnemyWasKilled;

    public void OnEnemyKilled() {
        if (EnemyWasKilled != null)
            EnemyWasKilled();
        else
            Debug.Log("No subscribers to EnemyWasKilled");
    }




    void Awake() {
        if (Instance == null)
            Instance = this;
    }


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
