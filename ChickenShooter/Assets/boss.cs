using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class boss : MonoBehaviour
{
    public float speed = 1f;
    public float moveRange = 2f;
    private Vector3 startPosition;
    private Vector3 moveDirection;
    public GameObject boomboom;
    public GameObject egg;
    public GameObject chickenleg;
    private float health =100;
    private float maxHealth = 100;
    public Slider healthSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(Egg_born());
        if (healthSlider != null)
        {
            health = maxHealth;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * moveRange;

        Vector3 move = moveDirection * speed * Time.deltaTime;

        transform.position = new Vector3(startPosition.x + x, transform.position.y + move.y, 0);
    }
    IEnumerator Egg_born()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 4f));
            GameObject eggne = Instantiate(egg, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1f, 4f));
            Destroy(eggne.gameObject);
        }

    }
    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir.normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            StartCoroutine(FlashRed());
            health = health- 5;
            AudioManager.instance.playSFX("Chick_hurt1");
            health = Mathf.Clamp(health, 0, maxHealth);

            // Update UI
            if (healthSlider != null)
                healthSlider.value = health;

            if (boomboom != null && health ==0)
            {
                GameObject bom = Instantiate(boomboom, transform.position, Quaternion.identity);
                GameObject chickenleggg = Instantiate(chickenleg, transform.position, Quaternion.identity);
                Destroy(bom.gameObject, 0.5f);
                GameManager.Instance.boss(0);
                AudioManager.instance.playSFX("Chicken_death2");
                Destroy(gameObject);
            }
            //GameManager.Instance.AddScore(10);
            //GameManager.Instance.ChickenKilled(1);
        }
        Destroy(collision.gameObject);
    }
    IEnumerator FlashRed()
    {
        SpriteRenderer boss = GetComponent<SpriteRenderer>();
        boss.color = Color.red; // đổi sang đỏ
        yield return new WaitForSeconds(0.2f); // chờ 1 giây
        boss.color = Color.white; // đổi lại màu gốc
    }
}
