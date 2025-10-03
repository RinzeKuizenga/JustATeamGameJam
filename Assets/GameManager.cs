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

    public void PerfectHit()
    {
        Debug.Log("Perfect Hit!");
        combo++;
        points += 10;
        plushieSlider.value = points;
        ShowFeedback("Perfect!");
    }

    public void GoodHit()
    {
        Debug.Log("Good Hit!");
        combo++;
        points += 5; // less than perfect
        plushieSlider.value = points;
        ShowFeedback("Good!");
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        points -= 2;
        plushieSlider.value = points;
        combo = 0;
        ShowFeedback("Miss!");
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            // Optionally fade it out later
        }
    }
}
