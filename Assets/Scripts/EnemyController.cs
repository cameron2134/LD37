using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public float moveSpeed;
    public GameObject shell;
    public Transform bulletSpawn;


    private float moveX, moveY;

    private Animator anim;
    private Rigidbody2D tankBody;
    private SpriteRenderer rend;


    private bool canShoot = true;

    private GameObject player;

    private float right, left;


    private void Fire() {

        GameObject bullet = (GameObject)Instantiate(shell, bulletSpawn.position, bulletSpawn.rotation);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());

        if (!rend.flipX) {

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
        }

        else {

            bullet.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
        }

        canShoot = false;
        StartCoroutine(BulletCooldown());
    }




    IEnumerator BulletCooldown() {

        yield return new WaitForSeconds(1);
        canShoot = true;
    }




    // Use this for initialization
    void Start() {
        tankBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        rend = this.GetComponent<SpriteRenderer>();

        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);


    }


    void FixedUpdate() {

        //tankBody.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);

    }




    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Portal") {


            other.GetComponent<Portal>().PortalTank(this.gameObject);

        }

    }
}
