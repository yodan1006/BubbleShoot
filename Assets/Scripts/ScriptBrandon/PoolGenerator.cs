using System.Collections.Generic;
using UnityEngine;

public class PoolGenerator : MonoBehaviour
{
    [Header("Prefabs for Slime")] 
    public List<GameObject> PrefabSlime;
    private List<GameObject> _poolEnemy;
    private int _numberOfEnemy = 30;

    private void Awake()
    {
        _poolEnemy = new List<GameObject>();

        for (int i = 0; i < _numberOfEnemy; i++)
        {
            foreach (var prefab in PrefabSlime)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                _poolEnemy.Add(obj);
            }
        }
    }

    public GameObject GetEnemy()
    {
        List<GameObject> inactiveEnemies = new List<GameObject>();

        // Parcours la pool pour récupérer les ennemis inactifs
        foreach (var obj in _poolEnemy)
        {
            if (!obj.activeInHierarchy)
            {
                inactiveEnemies.Add(obj);
            }
        }

        // Si des ennemis inactifs sont disponibles, en activer un au hasard
        if (inactiveEnemies.Count > 0)
        {
            GameObject randomEnemy = inactiveEnemies[Random.Range(0, inactiveEnemies.Count)];
            randomEnemy.SetActive(true);
            return randomEnemy;
        }

        // Sinon, créer un nouvel ennemi, l'activer et l'ajouter à la pool
        GameObject NewObj = Instantiate(PrefabSlime[Random.Range(0, PrefabSlime.Count)]);
        NewObj.SetActive(true);
        _poolEnemy.Add(NewObj);
        return NewObj;
    }

    public List<GameObject> GetAllEnemies()
    {
        return _poolEnemy;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
