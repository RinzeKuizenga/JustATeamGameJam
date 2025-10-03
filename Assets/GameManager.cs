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
    public TextMeshProUGUI feedbackText;

    public ParticleSystem[] perfectParticles;
    public ParticleSystem[] goodParticles;
    public ParticleSystem[] missParticles;

    void Start()
    {
        instance = this;
        plushieSlider.value = points;
        combo = 0;
    }

    void Update()
    {
        comboText.text = $"Combo: {combo}";
    }

    public void PerfectHit(int lane)
    {
        Debug.Log("Perfect Hit!");
        combo++;
        points += 10;
        plushieSlider.value = points;
        ShowFeedback("Perfect!");
        PlayParticles(perfectParticles, lane);
        
    }

    public void GoodHit(int lane)
    {
        Debug.Log("Good Hit!");
        combo++;
        points += 5; // less than perfect
        plushieSlider.value = points;
        ShowFeedback("Good!");
        PlayParticles(goodParticles, lane);
    }

    public void NoteMissed(int lane)
    {
        Debug.Log("Missed Note");
        points -= 2;
        plushieSlider.value = points;
        combo = 0;
        ShowFeedback("Miss!");
        PlayParticles(missParticles, lane);
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
        }
    }

    private void PlayParticles(ParticleSystem[] particleArray, int lane)
    {
        if (lane < particleArray.Length && particleArray[lane] != null)
        { 
            particleArray[lane].Play();
        }
    }
}
