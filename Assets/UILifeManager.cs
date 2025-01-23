using System.Collections.Generic;
using UnityEngine;

public class UILifeManager : MonoBehaviour
{
    public RulesGame rulesGame;
    public List<GameObject> uicoeur;

    private void Start()
    {
        UpdateHeart();
    }

    private void Update()
    {
        UpdateHeart();
    }

    private void UpdateHeart()
    {
        for (int i = 0; i < uicoeur.Count; i++)
        {
            uicoeur[i].SetActive(i < rulesGame.life);
        }
    }
}
