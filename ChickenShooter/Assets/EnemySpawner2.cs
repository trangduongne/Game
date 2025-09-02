using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner2 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject boss;

    [SerializeField] GameObject NextBttn;
    [SerializeField] int eventPos = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EventStarter());
        NextBttn.GetComponent<Button>().onClick.AddListener(NextButton);
    }

    // Update is called once per frame
    void Update()
    {
        // Đếm còn bao nhiêu enemy đang tồn tại
        int aliveEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (eventPos == 0 && aliveEnemy == 0 && GameManager.Instance.killed_chicken >= 24 && GameManager.Instance.heart>0)
        {
            NextBttn.SetActive(true);
            eventPos = 1;
        }
        else if (eventPos == 1 && aliveEnemy == 0 && GameManager.Instance.killed_chicken >= 24 + 8 && GameManager.Instance.heart > 0)
        {
            NextBttn.SetActive(true);
            eventPos = 2;
        }
        else if (eventPos == 2 && aliveEnemy == 0 && GameManager.Instance.killed_chicken >= 24 + 8 + 10 && GameManager.Instance.heart > 0)
        {
            NextBttn.SetActive(true);
            eventPos = 3;
        }
    }

    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine(EventOne());
        }
        else if (eventPos == 2)
        {
            StartCoroutine(EventTwo());
        }
        else if (eventPos == 3)
        {
            NextBttn.SetActive(true);
            StartCoroutine(EventThree());
        }
    }
        IEnumerator EventStarter()
    {
        NextBttn.SetActive(false);
        float totalWidth = (8 - 1) * 2f; // 8 con, cách nhau 2 đơn vị
        float startX = -totalWidth / 2;


        for (int row = 0; row < 3; row++) // số hàng
        {
            for (int i = 0; i < 8; i++) // số con trong hàng
            {
                float posX = startX + i * 2f; // khoảng cách giữa các con
                float posY = 5f - row * 1f; // khoảng cách giữa các hàng
                Vector3 spawnPosition = new Vector3(posX, posY, 0);
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.SetDirection(Vector3.down);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator EventOne()
    {
        NextBttn.SetActive(false);
        Vector3 center = new Vector3(0, 2f, 0); // tâm vòng tròn (có thể chỉnh)
        for (int i = 0; i < 8; i++)
        {
            float angle = i * Mathf.PI * 2f / 8; // chia đều theo vòng tròn
            float x = Mathf.Cos(angle) * 3f;
            float y = Mathf.Sin(angle) * 3f;

            Vector3 pos = center + new Vector3(x, y, 0);
            GameObject chicken = Instantiate(enemyPrefab, pos, Quaternion.identity);
            Enemy enemyScript = chicken.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.SetDirection(Vector3.down);
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator EventTwo()
    {
        NextBttn.SetActive(false);
        float startY = 4f;
        for (int i = 0; i < 5; i++)
        {
            Vector3 leftPos = new Vector3(-i, startY - i, 0);
            Vector3 rightPos = new Vector3(i, startY - i, 0);

            GameObject c1 = Instantiate(enemyPrefab, leftPos, Quaternion.identity);
            GameObject c2 = Instantiate(enemyPrefab, rightPos, Quaternion.identity);
            Enemy enemyScript = c1.GetComponent<Enemy>();
            Enemy enemyScript2 = c2.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.SetDirection(Vector3.down);
            }
            if (enemyScript2 != null)
            {
                enemyScript2.SetDirection(Vector3.down);
            }
        }
        yield return new WaitForSeconds(0.5f);

    }
    IEnumerator EventThree()
    {
        NextBttn.SetActive(false);
        Vector3 positionofboss = new Vector3(3f, 3f, 0);
        GameObject bossne = Instantiate(boss, positionofboss, Quaternion.identity);
        boss script = bossne.GetComponent<boss>();
        if (script != null)
        {
            while (true)
            {
                script.SetDirection(Vector3.left);
                yield return new WaitForSeconds(2f);
                script.SetDirection(Vector3.right);
                yield return new WaitForSeconds(2f);
            }
        }
        yield return new WaitForSeconds(0.5f);
    }

}
