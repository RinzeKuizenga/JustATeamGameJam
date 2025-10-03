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
    public Animator winAnim;

    public float sliderSpeed = 5f; 

    void Start()
    {
        instance = this;

        plushieSlider.value = 0;
        combo = 0;
        points = 0;
    }

    void Update()
    {
        comboText.text = $"Combo: {combo}";

        plushieSlider.value = Mathf.Lerp(plushieSlider.value, points, sliderSpeed * Time.deltaTime);

        // Check win condition
        if (points >= 100)
        {
            winAnim.SetTrigger("Win");
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on Time");

        combo++;
        points += 10;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        points -= 2;
        combo = 0;
    }
}
