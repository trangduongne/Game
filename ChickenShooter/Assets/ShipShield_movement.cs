using System.Threading;
using UnityEngine;

public class ShipShield_movement : MonoBehaviour
{
    int count = 0;
    public GameObject ship;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Egg") || collision.gameObject.CompareTag("Boss"))
        {
            count++;
            Destroy(collision.gameObject);
        }
        if (count >= 3) {
            ship.GetComponent<PlayerController>().shipShield = null;
            Destroy(this.gameObject);
        }

    }
}

