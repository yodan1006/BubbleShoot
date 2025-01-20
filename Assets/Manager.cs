using UnityEngine;
public class Manager : MonoBehaviour
{
    [Header("ref poolEnemy")] public PoolGenerator poolEnemy;
    [Header("ref limite of spawn")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    
    public float spawnInterval = 2f;
    private float _spawnTimer;

    private void Start()
    {
        _spawnTimer = spawnInterval;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            SpawnSlime();
            _spawnTimer = spawnInterval;
        }
    }

    private void SpawnSlime()
    {
        GameObject slime = poolEnemy.GetEnemy();

        if (slime != null)
        {
            Vector3 pos = GetRandomPos();
            slime.transform.position = pos;
        }
    }

    private Vector3 GetRandomPos()
    {
        var randomX = Random.Range(spawnPoint1.position.x, spawnPoint2.position.x);
        var randomY = spawnPoint1.position.y;
        var randomZ = spawnPoint1.position.z;
        
        return new Vector3(randomX, randomY, randomZ);
    }
}
