using UnityEngine;

public class Audio_Script : MonoBehaviour
{
    public static Audio_Script instance;

    [Header("Miss Audio's")]
    public AudioSource miss01;
    public AudioSource miss02;

    [Header("Hit Audio's")]
    public AudioSource hitStreak;

    [Header("References")]
    int hitCombo = GameManager.instance.combo;

    private void Awake()
    {
        instance = this;
    }

    public void PlayMiss()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            miss01.Play();
        }
        else
        {
            miss02.Play();
        }
    }
    public void Update()
    {
        hitCombo = GameManager.instance.combo;
        Debug.Log(hitCombo);
    }

    public void PlayHitStreak()
    {
        if (hitCombo == 10)
        {
            hitStreak.Play();
            Debug.Log("WHAAAAATTT");
        }
    }
}
