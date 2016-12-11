using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public GameObject shell;
    public Transform bulletSpawn;
    public AudioClip clip;


    private float moveX, moveZ;

    private Animator anim;
    private Rigidbody2D tankBody;
    private SpriteRenderer rend;
    private PlayerHealthManager health;

    private AudioSource source;
    


    private bool canShoot = true;


    private float right, left;

    private float rotateAmnt = 5;




    private void Fire() {
        source.Play();
        GameObject bullet = (GameObject) Instantiate(shell, bulletSpawn.position, bulletSpawn.rotation);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());

        if (this.transform.localScale.x == 1) {
            
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);
        }

        else {
            
            bullet.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
        }

        canShoot = false;
        StartCoroutine(BulletCooldown());
    }




    IEnumerator BulletCooldown() {

        yield return new WaitForSeconds(0.7f);
        canShoot = true;
    }




	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.clip = clip;

        tankBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        rend = this.GetComponent<SpriteRenderer>();
        health = this.GetComponent<PlayerHealthManager>();

    }
	
	// Update is called once per frame
	void Update () {


        #region Player Movement
        moveX = moveZ = 0;
        anim.SetBool("Moving", false);

        // Movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1;
            moveZ = -1;


            this.transform.localScale = new Vector2(-1, 1);
            //rend.flipX = true;
            anim.SetBool("Moving", true);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            moveX = 1;
            moveZ = -1;


            this.transform.localScale = new Vector2(1, 1);
            //rend.flipX = false;
            anim.SetBool("Moving", true);
        }




        if (Input.GetKeyDown(KeyCode.Space)) {
            if (canShoot)
                Fire();
        }





        //anim.SetBool("Moving", false);

        #endregion


    }


    void FixedUpdate() {

        tankBody.velocity = new Vector3(moveX * moveSpeed, -6f, moveZ);

    }




    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Portal") {


            other.GetComponent<Portal>().PortalTank(this.gameObject);

        }

    }

    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "Wall")
            this.transform.position = new Vector2(Random.Range(-6, 6), 2.7f);

        if (other.gameObject.tag == "Bullet") {
            health.TakeDamage(0.5f);
        }

        
    }


    void OnCollisionStay2D(Collision2D other) {

        if (other.gameObject.tag == "Trap")
            health.TakeDamage(0.05f);

    }



}
