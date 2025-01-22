using TMPro;
using UnityEngine;
public class Manager : MonoBehaviour
{
    public PoolGenerator poolEnemy;
    public PoolMunition poolMunition;
    public ShootBubble shootBubble;
    public RulesGame rulesGame;

    public Transform targetEnemy;
    
    [Header("ref limite of spawn")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    
    public float spawnInterval = 2f;
    private float _spawnTimer;
    public TextMeshProUGUI scoreText;
    
    [Header("bonus")]
    public int[] bonus;

    public int CountBonus;

    private void Start()
    {
        _spawnTimer = spawnInterval;

        foreach (var enemy in poolEnemy.GetAllEnemies())
        {
            var enemyScript = enemy.GetComponent<EnemyIA>();

            if (enemyScript != null) enemyScript.OnTouched += HandleEnemyCollision;
        }

        foreach (var bubble in poolMunition.GetAllBubbles())
        {
            var bubbleScript = bubble.GetComponent<Bubble>();

            if (bubbleScript != null) bubbleScript.OnBubbleTouched += HandleBubbleCollision;
        }
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            SpawnSlime();
            _spawnTimer = spawnInterval;
        }

        for (int i = 0; i < bonus.Length; i++)
        {
            if (CountBonus >= bonus[i])
            {
                Applybonus();
                bonus[i]--;
            }
        }
    }

    private void Applybonus()
    {
        shootBubble.missileCount += 2;
    }

    private void SpawnSlime()
    {
        GameObject slime = poolEnemy.GetEnemy();

        if (slime != null)
        {
            Vector3 pos = GetRandomPos();
            slime.transform.position = pos;
            ConfigureEnemy(slime);
        }
    }

    private void ConfigureEnemy(GameObject enemy)
    {
        EnemyIA ia = enemy.GetComponent<EnemyIA>();

        if (ia != null)
        {
            ia.target = targetEnemy;
        }
    }

    private void HandleEnemyCollision(GameObject enemy)
    {
        enemy.SetActive(false);
        poolEnemy.ReturnObject(enemy);
        int scoreAdd = rulesGame.AddScore100();
        scoreText.text = scoreAdd.ToString();
        CountBonus++;
    }
    
    private void HandleBubbleCollision(GameObject bubble)
    {
        bubble.SetActive(false);
        poolMunition.ReturnBubble(bubble);
    }

    private Vector3 GetRandomPos()
    {
        var randomX = Random.Range(spawnPoint1.position.x, spawnPoint2.position.x);
        var randomY = spawnPoint1.position.y;
        var randomZ = spawnPoint1.position.z;
        
        return new Vector3(randomX, randomY, randomZ);
    }
}
