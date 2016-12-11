using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public GameObject player;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

    }
}
