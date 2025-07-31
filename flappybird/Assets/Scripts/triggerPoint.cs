using UnityEngine;

public class triggerPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("+1 Score from trigger");
            GameManager.Instance.AddScore(1);
        }
    }

}
