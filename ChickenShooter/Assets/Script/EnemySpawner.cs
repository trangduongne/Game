using System.Collections;
using System.Drawing;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject boss;
    private int enemiesPerRow = 10;
    private int numberOfRows = 3;
    //private int totalEnemies;
    private float spacingX = 0.8f;
    private float spacingY = 1f;
    private float startY = 5f;
    public float spawnRangeX = 5f;
    float timer;
    private Camera cam;
    public string enemyTag = "Enemy"; // tag của kẻ địch


    void Awake()
    {
        //cam = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        //int visibleAliveEnemies = CountVisibleAliveEnemies();
        //GameManager.Instance.totalchickens(visibleAliveEnemies);
    }
    //int CountVisibleAliveEnemies()
    //{
    //    int count = 0;
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    //    foreach (var e in enemies)
    //    {
    //        // Kiểm tra xem có đang ở trong frustum của camera hay không
    //        var renderer = e.GetComponent<Renderer>();
    //        if (renderer != null)
    //        {
    //            if (IsVisibleFrom(renderer, cam))
    //            {
    //                count++;
    //            }
    //        }
    //        else
    //        {
    //            // Nếu không có Renderer, có thể xem vị trí có trong frustum bằng kĩ thuật khác
    //            // Ví dụ: xem xem có trong culling bằng Camera current
    //            Vector3 viewportPos = cam.WorldToViewportPoint(e.transform.position);
    //            if (viewportPos.z > 0 &&
    //                viewportPos.x > 0 && viewportPos.x < 1 &&
    //                viewportPos.y > 0 && viewportPos.y < 1)
    //            {
    //                count++;
    //            }
    //        }
    //    }
    //    return count;
    //}
    //bool IsVisibleFrom(Renderer renderer, Camera camera)
    //{
    //    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
    //    return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    //}


    IEnumerator SpawnWave()
    {

        float totalWidth = (enemiesPerRow - 1) * spacingX;
        float startX = -totalWidth / 2;


        for (int row = 0; row < numberOfRows; row++)
        {
            for (int i = 0; i < enemiesPerRow; i++)
            {
                float posX = startX + i * spacingX;
                float posY = startY - row * spacingY; Vector3 spawnPosition = new Vector3(posX, posY, 0);
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.SetDirection(Vector3.down);
                }
            }
        }
        yield return new WaitForSeconds(10f);


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
        yield return new WaitForSeconds(10f);
        startY = 4f;
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

        yield return new WaitForSeconds(10f);
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
    }
}
