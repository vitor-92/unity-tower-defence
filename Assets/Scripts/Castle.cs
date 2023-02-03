using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public float totalHealth = 1000f;
    [HideInInspector] public float currentHealth;
    public Slider healthBar;

    public GameObject takeHit;

    private float startHitTime = .08f;
    private float currentHitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        healthBar.maxValue = totalHealth;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHitTime > 0){
            currentHitTime -= 1 * Time.deltaTime;
        }else{
            takeHit.SetActive(false);
        }
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            currentHealth = 0;
            gameObject.SetActive(false);
        }
        healthBar.value = currentHealth;

        currentHitTime = startHitTime;
        takeHit.SetActive(true);
    }
}
