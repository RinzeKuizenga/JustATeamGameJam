using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;   // <-- Needed for Coroutines
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider plushieSlider;
    public float points;
    public int combo;

    public TextMeshProUGUI comboText;
    public TextMeshProUGUI countdownText;   // 👈 drag your TMP object here in inspector

    public SpriteRenderer feedbackRenderer;
    public Sprite perfect;
    public Sprite good;
    public Sprite miss;

    public ParticleSystem[] perfectParticles;
    public ParticleSystem[] goodParticles;
    public ParticleSystem[] missParticles;

    public Animator winAnim;
    public Animator loseAnim;

    public float sliderSpeed = 5f;
    public AudioSource BackgroundMusic;

    [Header("BeatScroller Reference")]
    public BeatScroller beatScroller;      // 👈 assign in inspector

    [Header("Easy to change variables")]
    public float pointWorth;
    public float pointWorthPerfect;
    public float pointWorthMiss;
    public string sceneChanger;

    void Start()
    {
        instance = this;
        plushieSlider.value = 0;
        combo = 0;
        points = 0;
        feedbackRenderer.sprite = null;
        BackgroundMusic.volume = 0f;

        // Start countdown when scene loads
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        comboText.text = $"Combo {combo}";

        plushieSlider.value = Mathf.Lerp(plushieSlider.value, points, sliderSpeed * Time.deltaTime);

        // 🟢 Playtesting cheat: Press P to instantly win
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerImmediateWin();
        }

        if (BackgroundMusic.isPlaying == false && beatScroller.hasStarted)
        {
            if (points >= 100)
            {
                winAnim.SetTrigger("Win");
            }
            else
            {
                loseAnim.SetTrigger("Lose");
            }
        }
    }

    IEnumerator StartCountdown()
    {
        // disable scrolling until countdown done
        beatScroller.hasStarted = false;

        countdownText.gameObject.SetActive(true);

        // 3..2..1..
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // GO!
        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);

        // Now start the music and movement
        BackgroundMusic.volume = 1f;
        BackgroundMusic.Play();
        beatScroller.hasStarted = true;
    }

    public void PerfectHit(int lane)
    {
        combo++;
        points += pointWorthPerfect;
        ShowFeedback(perfect);
        PlayParticles(perfectParticles, lane);
        Audio_Script.instance.PlayHitStreak();
    }

    public void GoodHit(int lane)
    {
        combo++;
        points += pointWorth;
        ShowFeedback(good);
        PlayParticles(goodParticles, lane);
        Audio_Script.instance.PlayHitStreak();
    }

    public void NoteMissed(int lane)
    {
        // Prevent early misses during countdown
        if (!beatScroller.hasStarted)
            return;

        points -= pointWorthMiss;
        combo = 0;
        ShowFeedback(miss);
        PlayParticles(missParticles, lane);

        Audio_Script.instance.PlayMiss();
        Debug.Log("Hit played");
    }

    public void TriggerImmediateWin()
    {
        Debug.Log("Playtesting: Forced win triggered!");

        // Stop the song so Update() doesn't override us
        if (BackgroundMusic.isPlaying)
            BackgroundMusic.Stop();

        // Prevent further scrolling
        beatScroller.hasStarted = false;

        // Trigger the win animation right away
        winAnim.SetTrigger("Win");
    }



    private void ShowFeedback(Sprite feedback)
    {
        if (feedbackRenderer != null)
            feedbackRenderer.sprite = feedback;
    }

    private void PlayParticles(ParticleSystem[] particleArray, int lane)
    {
        if (lane < particleArray.Length && particleArray[lane] != null)
        {
            particleArray[lane].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleArray[lane].Play();
        }
    }
}
