using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public float moveSpeed;
    public GameObject shell;
    public Transform bulletSpawn;


    private int health = 3;

    private float moveX, moveY;

    private Animator anim;
    private Rigidbody2D tankBody;
    private SpriteRenderer rend;


    private bool canShoot = true;

    private GameObject player;

    private float right, left;


    private void Fire() {
        Debug.Log("Firing");
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



    public void TakeDamage() {

        StartCoroutine(FlashDamage());
        this.health--;

        if (this.health <= 0) {
            GameManager.Instance.OnEnemyKilled();
            Destroy(this.gameObject);
        }

        
        
    }



    IEnumerator FlashDamage() {
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<Renderer>().material.color = Color.white;
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
        this.GetComponent<Animator>().SetBool("Moving", true);  
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
       
        if (transform.position.x > Vector3.forward.x) {
            this.GetComponent<SpriteRenderer>().flipX = true;
            this.GetComponent<Animator>().SetBool("Moving", true);
        }

        else if (transform.position.x < Vector3.forward.x) {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }



        
            

    }


    void FixedUpdate() {

        if (this.GetComponent<SpriteRenderer>().flipX == false) {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
            if (hit.collider.tag == "Player") {
                Fire();
            }

        }

        else {

            // Check if player is infront. If they are, shoot
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
            if (hit.collider.tag == "Player") {
                Fire();
            }

        }
        

    }



    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "PlayerBullet") {
            TakeDamage();
        }
    }


    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Portal") {


            other.GetComponent<Portal>().PortalTank(this.gameObject);

        }

    }
}
