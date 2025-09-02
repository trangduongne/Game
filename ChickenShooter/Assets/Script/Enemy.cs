using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float moveRange = 2f;
    private Vector3 startPosition;
    private Vector3 moveDirection;
    public GameObject boomboom;
    public GameObject egg;
    public GameObject chickenleg;
    public GameObject Special;
    private float randomOffset = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(Egg_born());
        StartCoroutine(Special_born());
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin((Time.time + randomOffset) * speed) * moveRange;
        float y = transform.position.y - speed * Time.deltaTime;

        // cho nó di chuyển zig-zag xuống
        transform.position = new Vector3(startPosition.x + x, y, 0);

        // chỉ clamp trục X (không clamp Y để nó thoát ra ngoài màn hình)
        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector3 BottomRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, TopLeftPoint.x + 0.7f, BottomRightPoint.x - 0.7f),
            transform.position.y,  // giữ nguyên Y
            transform.position.z);

    }
    IEnumerator Egg_born()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            GameObject eggne = Instantiate(egg, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            Destroy(eggne.gameObject);
        }
        
    }
    IEnumerator Special_born()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            GameObject specialne = Instantiate(Special, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            Destroy(specialne.gameObject);
        }
    }
    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir.normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            if(boomboom!=null)
            {
                GameObject bom = Instantiate(boomboom,transform.position, Quaternion.identity);
                AudioManager.instance.playSFX("Chicken_death1");
                GameObject chickenleggg = Instantiate(chickenleg,transform.position, Quaternion.identity);
                Destroy(bom.gameObject, 0.5f);
            }
            //GameManager.Instance.AddScore(10);
            GameManager.Instance.ChickenKilled(1);
        }
        Destroy(collision.gameObject);
        Destroy(gameObject);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            if (boomboom != null)
            {
                GameObject x = Instantiate(boomboom, transform.position, Quaternion.identity);
                Destroy(x.gameObject, 0.8f);
            }
            GameManager.Instance.MinusHeart(1);
        }
    }
}


