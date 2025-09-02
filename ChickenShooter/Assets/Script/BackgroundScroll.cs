using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed=5f;
    public GameObject secondBackground;
    private float backgroundHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y <= -backgroundHeight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + backgroundHeight * 2, transform.position.z);
        }
        secondBackground.transform.position += Vector3.down * speed * Time.deltaTime;

        if (secondBackground.transform.position.y <= -backgroundHeight)
        {
            secondBackground.transform.position = new Vector3(secondBackground.transform.position.x, secondBackground.transform.position.y + backgroundHeight * 2, secondBackground.transform.position.z);

        }
    }
}
