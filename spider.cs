using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class Spider : MonoBehaviour
{
    Rigidbody rb;
    public Image healthBar;

    float maxHealth = 100;
    float currentHealth = 100;

    float speed = 4;
    Camera cam;

    public ParticleSystem greenEffect;

    Transform line;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            greenEffect.Play();
        }

        if (collision.gameObject.CompareTag("Line"))
        {
            GameObject.Find("Manager").GetComponent<Manager>().EnemyFinishWin();
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        line = GameObject.Find("Line").transform;

        transform.LookAt(line);
        greenEffect.Stop();

        maxHealth = 100;
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        GetComponentInChildren<Canvas>().gameObject.transform.LookAt(cam.transform);
        transform.LookAt(line);
    }

    public void HealthDown(float value)
    {
        currentHealth -= value;
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            speed = 0;
            GetComponent<Animator>().Play("Death");
            GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 2);
        }
    }
}
