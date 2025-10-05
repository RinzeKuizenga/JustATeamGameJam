using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider plushieSlider;
    public float points;
    public int combo;

    public TextMeshProUGUI comboText;
    public TextMeshProUGUI countdownText;

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
    public BeatScroller beatScroller;

    [Header("Easy to change variables")]
    public float pointWorth;
    public float pointWorthPerfect;
    public float pointWorthMiss;

    // 🟢 Player Character Sprite
    [Header("Player Character")]
    public SpriteRenderer playerRenderer;
    public Sprite playerSad;
    public Sprite playerNeutral;
    public Sprite playerHappy;

    // thresholds to decide moods
    [Header("Mood thresholds")]
    public int happyComboThreshold = 10;     // above this = happy
    public int sadComboThreshold = 0;        // when combo resets / low score = sad

    void Start()
    {
        instance = this;
        plushieSlider.value = 0;
        combo = 0;
        points = 0;
        feedbackRenderer.sprite = null;
        BackgroundMusic.volume = 0f;

        // Set player to neutral at start
        if (playerRenderer != null)
            playerRenderer.sprite = playerNeutral;

        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        comboText.text = $"Combo {combo}";
        plushieSlider.value = Mathf.Lerp(plushieSlider.value, points, sliderSpeed * Time.deltaTime);

        UpdatePlayerMood();   // <-- check mood every frame

        if (BackgroundMusic.isPlaying == false && beatScroller.hasStarted)
        {
            if (points >= 100)
                winAnim.SetTrigger("Win");
            else
                loseAnim.SetTrigger("Lose");
        }
    }

    IEnumerator StartCountdown()
    {
        beatScroller.hasStarted = false;
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);

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

        UpdatePlayerMood();
    }

    public void GoodHit(int lane)
    {
        combo++;
        points += pointWorth;
        ShowFeedback(good);
        PlayParticles(goodParticles, lane);
        Audio_Script.instance.PlayHitStreak();

        UpdatePlayerMood();
    }

    public void NoteMissed(int lane)
    {
        if (!beatScroller.hasStarted)
            return;

        points -= pointWorthMiss;
        combo = 0;
        ShowFeedback(miss);
        PlayParticles(missParticles, lane);
        Audio_Script.instance.PlayMiss();

        Debug.Log("Hit played");

        UpdatePlayerMood();
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

    // 🟢 Player Mood Update
    private void UpdatePlayerMood()
    {
        if (playerRenderer == null) return;

        if (combo >= happyComboThreshold)
        {
            playerRenderer.sprite = playerHappy;
        }
        else if (combo <= sadComboThreshold)
        {
            playerRenderer.sprite = playerSad;
        }
        else
        {
            playerRenderer.sprite = playerNeutral;
        }
    }
}
