using UnityEngine;
using UnityEngine.UI;

public class RulesGame : MonoBehaviour
{
    public int life { get; private set; } = 3;
    public int score { get; private set; } = 0;

    
    public Image heart;
    public Transform conteneurHeart;

    private void Start()
    {
        UpdateHeart();
    }

    private void UpdateHeart()
    {
        foreach (Transform child in conteneurHeart)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < life; i++)
        {
            Instantiate(heart, conteneurHeart);
        }
    }

    public void TakeDamage(int damage)
    {
        damage = 1;
        life -= damage;
        life = Mathf.Max(life, 0);
        UpdateHeart();
    }

    public int AddScore100()
    {
        int addScore = 100;
        score += addScore;
        return score;
    }

    public int AddScore500(int addScore)
    {
        addScore = 500;
        score += addScore;
        return score;
    }
    
}
