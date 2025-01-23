using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public PoolGenerator poolEnemy;
    public PoolMunition poolMunition;
    public ShootBubble shootBubble;
    public RulesGame rulesGame;
    public GameObject KingSlime;
    public IAMiniBoss miniBoss;

    public Transform targetEnemy;
    
    [Header("ref limite of spawn")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform SpawnKingSLime;
    
    public float spawnInterval = 2f;
    private float _spawnTimer;
    public TextMeshProUGUI scoreText;

    [Header("rules of Spawn slime")] 
    public float timeToIncreasedSpawn = 60f;
    public float increasedSpeedSlime = 0.5f;
    public int CountForKingSlime;
    private float _timeElapsed;
    private int spawnCounter = 1;
    
    [Header("bonus")]
    public int[] bonus;
    public int CountBonus;
    private bool[] bonusApplied;

    private void Start()
    {
        _spawnTimer = spawnInterval;
        miniBoss.target = targetEnemy;
        miniBoss.scoreText = scoreText;
        miniBoss.rulesGame = rulesGame;

        foreach (var enemy in poolEnemy.GetAllEnemies())
        {
            var enemyScript = enemy.GetComponent<EnemyIA>();

            if (enemyScript != null)
            {
                enemyScript.OnTouched += HandleEnemyCollision;
                enemyScript.OnPlayerTouched += HandlePlayerHit;

            }
        }

        foreach (var bubble in poolMunition.GetAllBubbles())
        {
            var bubbleScript = bubble.GetComponent<Bubble>();

            if (bubbleScript != null) bubbleScript.OnBubbleTouched += HandleBubbleCollision;
        }
        bonusApplied = new bool[bonus.Length];
        
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= timeToIncreasedSpawn)
        {
            spawnCounter++;
            _timeElapsed = 0;
            UpdateSpeedSlime();
        }

        if (_spawnTimer <= 0)
        {
            SpawnSlime();
            _spawnTimer = spawnInterval;
        }

        for (int i = 0; i < bonus.Length; i++)
        {
            if (CountBonus >= bonus[i] && !bonusApplied[i])
            {
                Applybonus(i);
                bonusApplied[i] = true;
            }
        }
        
        if (rulesGame.life == 0)
            rulesGame.GameOver();

        if (CountForKingSlime == 15)
        {
            SpawnKingSlime();
            CountForKingSlime = 0;
        }
    }
    
    private void SpawnKingSlime()
    {
        Instantiate(KingSlime, SpawnKingSLime.position, SpawnKingSLime.rotation);
    }

    private void UpdateSpeedSlime()
    {
        foreach (var slime in poolEnemy.GetAllEnemies())
        {
            var enemy = slime.GetComponent<EnemyIA>();

            enemy.speed += increasedSpeedSlime;
        }
    }

    private void Applybonus(int bonusIndex)
    {
        shootBubble.missileCount += 2;
    }

    private void SpawnSlime()
    {
        for (int i = 0; i < spawnCounter; i++)
        {
                
            GameObject slime = poolEnemy.GetEnemy();
    
            if (slime != null)
            {
                Vector3 pos = GetRandomPos();
                slime.transform.position = pos;
                ConfigureEnemy(slime);
            }
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

    private void HandlePlayerHit(GameObject player)
    {
        rulesGame.life--;
    }

    private void HandleEnemyCollision(GameObject enemy)
    {
        enemy.SetActive(false);
        poolEnemy.ReturnObject(enemy);
        int scoreAdd = rulesGame.AddScore100();
        scoreText.text = scoreAdd.ToString();
        CountBonus++;
        CountForKingSlime++;
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
