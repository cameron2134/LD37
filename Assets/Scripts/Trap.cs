using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    private void Die() {

        GameManager.Instance.OnTrapDeath();
        Destroy(this.gameObject);

    }



    // Use this for initialization
    void Start() {
        Invoke("Die", 8f);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
