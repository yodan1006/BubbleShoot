using System;
using TMPro;
using UnityEngine;

public class IAMiniBoss : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    public int life = 5;
    public RulesGame rulesGame;
    public TextMeshProUGUI scoreText;
    public ManageSound manageSound;


    private void Start()
    {
        manageSound.bossAudio.Play();
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
            Vector3 dir = (target.position - transform.position).normalized;
            if(dir != Vector3.zero) transform.forward = dir;
        }
        
        if (transform.position == target.position) rulesGame.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        life--;
        if (life <= 0)
        {
            Destroy(gameObject);
            int scoreAdd = rulesGame.AddScore500();
            scoreText.text = scoreAdd.ToString();
            manageSound.bossAudio.Stop();
            manageSound.audio.Play();
        }
    }
}
