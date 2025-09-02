using UnityEngine;
using UnityEngine.UIElements;

public class egg : MonoBehaviour
{
    public GameObject egg_break;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            GameManager.Instance.MinusHeart(1);
            if (egg_break != null)
            {
                GameObject bom = Instantiate(egg_break, transform.position, Quaternion.identity);
                AudioManager.instance.playSFX("Egg_break");
                Destroy(bom.gameObject, 0.5f);
            }
            Destroy(gameObject); // Hủy quả trứng sau khi va chạm với người chơi
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            GameObject x = Instantiate(egg_break, transform.position, Quaternion.identity);
            AudioManager.instance.playSFX("Egg_break");
            Destroy(x.gameObject, 0.8f);
        }

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag =="Ground")
    //    {
    //        GameObject x = Instantiate(egg_break, transform.position, Quaternion.identity);
    //        Destroy(x.gameObject, 0.8f);
    //    }    
    //}
}
