using UnityEditor.Build.Content;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D rb;
    public float flyForce = 5f;
    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource hit;
    public CameraShake camShake;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, flyForce);
            float angle = Mathf.Clamp(rb.linearVelocity.y * 3f, -90f, 30f);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            animator.SetTrigger("fly");
            jump.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pipe"))
        {
            hit.Play();
            TriggerShake();
            Debug.Log("Da cham vat");
            GameManager.Instance.GameOver();
        }

    }
    void TriggerShake()
{
    StartCoroutine(camShake.Shake());
}

    
}
