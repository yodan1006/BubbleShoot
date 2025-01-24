using UnityEngine;

public class ManageSound : MonoBehaviour
{
    public AudioSource audio;
    public AudioSource bossAudio;
    void Start()
    {
        audio.Play();
    }

    public void BossSound()
    {
        bossAudio.Play();
    }

    public void BossKilled()
    {
        bossAudio.Stop();
    }
    
}
