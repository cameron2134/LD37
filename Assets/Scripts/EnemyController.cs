using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public float moveSpeed;
    public GameObject shell;
    public Transform bulletSpawn;
    public AudioClip clip, clip2;

    public GameObject muzzleFlash;

    public AnimationClip deathClip, muzzleClip;


    private int health = 3;

    private float moveX, moveY;

    private Animator anim;
    private Rigidbody2D tankBody;
    private SpriteRenderer rend;


    private AudioSource source;


    private bool canShoot = true, playerDead = false;

    private GameObject player;

    private float right, left;


    private void Fire() {
        source.clip = clip;
        source.Play();

        StartCoroutine(MuzzleFlash());

        GameObject bullet = (GameObject)Instantiate(shell, bulletSpawn.position, bulletSpawn.rotation);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());

        if (!rend.flipX) {

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
        }

        else {

            bullet.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
        }

    }


    IEnumerator MuzzleFlash() {

        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleClip.length);
        muzzleFlash.SetActive(false);



    }



    public void TakeDamage() {
        
        StartCoroutine(FlashDamage());
        this.health--;

        

        if (this.health <= 0) {
            StartCoroutine(WaitForAudio());
            this.transform.FindChild("EnemyDeath").gameObject.SetActive(true);
            
            GameManager.Instance.OnEnemyKilled();
            
            
        }

        
        
    }



    IEnumerator WaitForAudio() {
        this.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(clip2.length);
        Destroy(this.gameObject);
    }


    


    IEnumerator FlashDamage() {
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<Renderer>().material.color = Color.white;
    }



    private void OnPlayerDeath() {
        this.enabled = false;
    }



    void OnDestroy() {
        GameManager.Instance.PlayerDied -= OnPlayerDeath;
    }


    // Use this for initialization
    void Start() {

        source = GetComponent<AudioSource>();
        source.clip = clip;

        GameManager.Instance.PlayerDied += OnPlayerDeath;

        tankBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        rend = this.GetComponent<SpriteRenderer>();

        this.player = GameObject.Find("Player");

        InvokeRepeating("Fire", 0, Random.Range(1f, 3f));
    }

    // Update is called once per frame
    void Update() {

        if (!playerDead) {

            this.GetComponent<Animator>().SetBool("Moving", true);
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

            if (transform.position.x > Vector3.forward.x) {

                this.tankBody.velocity = new Vector3(this.tankBody.velocity.x, this.tankBody.velocity.y, -1);
                this.GetComponent<SpriteRenderer>().flipX = true;
                this.GetComponent<Animator>().SetBool("Moving", true);
            }

            else if (transform.position.x < Vector3.forward.x) {
                this.tankBody.velocity = new Vector3(this.tankBody.velocity.x, this.tankBody.velocity.y, 1);
                this.GetComponent<SpriteRenderer>().flipX = false;
            }


        }
        
            

    }





    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet") {
            TakeDamage();
            source.clip = clip2;
            source.Play();
        }

        if (other.gameObject.tag == "Wall")
            this.transform.position = new Vector2(Random.Range(-6, 6), Random.Range(2, 5));

        if (other.gameObject.tag == "Trap")
            TakeDamage();
    }


    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Portal") {


            other.GetComponent<Portal>().PortalTank(this.gameObject);

        }

    }
}
