using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public GameObject shell;
    public Transform bulletSpawn;


    private float moveX, moveY;

    private Animator anim;
    private Rigidbody2D tankBody;
    private SpriteRenderer rend;


    private bool canShoot = true;


    private float right, left;


    private void Fire() {

        GameObject bullet = (GameObject) Instantiate(shell, bulletSpawn.position, bulletSpawn.rotation);
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
	void Start () {
        tankBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        rend = this.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        #region Player Movement
        moveX = moveY = 0;
        anim.SetBool("Moving", false);

        // Movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1;
            moveY = 0;

            
           
            rend.flipX = true;
            anim.SetBool("Moving", true);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            moveX = 1;
            moveY = 0;

            
            rend.flipX = false;
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

        tankBody.velocity = new Vector2(moveX * moveSpeed, -6f);

    }




    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Portal") {


            other.GetComponent<Portal>().PortalTank(this.gameObject);

        }

    }

    void OnCollisionEnter2D(Collision2D other) {
       
        if (other.gameObject.tag == "Wall")
            Destroy(this.gameObject);
    }



}
