using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public Slider healthSlider;

    private float health = 10;

    public AudioClip damageSound;



    public void TakeDamage(float dmg) {

        StartCoroutine(FlashDamage());
        this.health -= dmg;
        healthSlider.value -= dmg;

        if (this.health <= 0) {
            GameManager.Instance.OnPlayerDied();
            this.gameObject.SetActive(false);
        }


    }




    IEnumerator FlashDamage() {
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<Renderer>().material.color = Color.white;
    }


 
}
