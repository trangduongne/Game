using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    //public void SetDirection(Vector3 dir)
    //{
    //    direction = dir.normalized;
        
    //}

    // Update is called once per frame
    void Update()
    {
        //transform.position += direction * speed * Time.deltaTime;
        transform.position += transform.up * speed * Time.deltaTime;



        //OnDestroyIfReachedDistance();
    }
    //private void OnDestroyIfReachedDistance()
    //{
    //    Vector3 CenterScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    //    if (Vector2.Distance(CenterScreen, transform.position) > 10f)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

}
