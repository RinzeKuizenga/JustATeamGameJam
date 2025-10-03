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
    

    void Start()
    {
        instance = this;

        plushieSlider.value = points;
        combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = $"Combo: {combo}";
    }

    public void NoteHit()
    {
        Debug.Log("Hit on Time");

        combo++;
        points += 10;
        plushieSlider.value = points;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        points -= 2;
        plushieSlider.value = points;
        combo = 0;
    }
}
