using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider plushieSlider;
    public int points;
    public int combo;

    public TextMeshProUGUI comboText;

    public SpriteRenderer feedbackRenderer;
    public Sprite perfect;
    public Sprite good;
    public Sprite miss;

    public ParticleSystem[] perfectParticles;
    public ParticleSystem[] goodParticles;
    public ParticleSystem[] missParticles;

    public Animator winAnim;

    public float sliderSpeed = 5f;

    void Start()
    {
        instance = this;
        plushieSlider.value = 0;
        combo = 0;
        points = 0;

        feedbackRenderer.sprite = null;
    }

    void Update()
    {
        comboText.text = $"Combo: {combo}";

        // Smoothly animate the slider toward the target points
        plushieSlider.value = Mathf.Lerp(plushieSlider.value, points, sliderSpeed * Time.deltaTime);

        if (points >= 100)
        {
            winAnim.SetTrigger("Win");
        }
    }

    public void PerfectHit(int lane)
    {
        Debug.Log("Perfect Hit!");
        combo++;
        points += 10;                // Increase target
        ShowFeedback(perfect);
        PlayParticles(perfectParticles, lane);
    }

    public void GoodHit(int lane)
    {
        Debug.Log("Good Hit!");
        combo++;
        points += 5;                 // Increase target
        ShowFeedback(good);
        PlayParticles(goodParticles, lane);
    }

    public void NoteMissed(int lane)
    {
        Debug.Log("Missed Note");
        points -= 2;                 // Decrease target
        combo = 0;
        ShowFeedback(miss);
        PlayParticles(missParticles, lane);
    }

    private void ShowFeedback(Sprite feedback)
    {
        if (feedbackRenderer != null)
        {
            feedbackRenderer.sprite = feedback;
        }
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
