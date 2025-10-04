using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogTalk : MonoBehaviour
{
    public Text text;
    public Text nameBox;
    public List<string> names;
    public List<string> sentences;

    public int tickDelay = 1;
    public TextAsset file;
    public Move playerMoveComponent;

    private int textIndex = 0;
    private string currentText = string.Empty;
    private int sentenceIndex = 0;
    private string lastSentence = string.Empty;
    public bool finished = false;

    private int ticks = 0;

    private void ResetVars()
    {
        textIndex = 0;
        textIndex = 0;
        currentText = string.Empty;
        sentenceIndex = -1;
        lastSentence = string.Empty;
        finished = false;
    }

    public void Begin()
    {
        var content = file.text;

        if (content == null)
        {
            Debug.LogWarning("TextAsset doesn't have any text!");
            return;
        }

        var all = content.Split("\n");

        names = new List<string>();
        sentences = new List<string>();

        int i = 0;
        foreach (var line in all)
        {
            if (i == 0 || i % 2 == 0)
                names.Add(line);
            else
                sentences.Add(line);
            i++;
        }

        if (names.Count != sentences.Count)
            Debug.LogWarning("Names count and sentences count aren't equal!");

        if (names.Count == 0 || sentences.Count == 0)
        {
            Debug.LogWarning("Names or sentences with zero elements present.");
            Debug.LogWarning($"{names.Count} names");
            Debug.LogWarning($"{sentences.Count} sentences");
        }

        ResetVars();

        if (playerMoveComponent)
            playerMoveComponent.canMove = false;
    }

    private string GetSentence()
    {
        long currentTime = DateTime.Now.Ticks;

        if (sentenceIndex >= sentences.Count)
        {
            finished = true;

            if (playerMoveComponent)
                playerMoveComponent.canMove = true;

            return null;
        }

        string current = sentences[sentenceIndex];
        if (textIndex < current.Length && ticks > tickDelay)
        {
            currentText += current[textIndex];
            textIndex++;
            ticks = 0;
        }

        return currentText;
    }

    private void NextSentence()
    {
        sentenceIndex++;
        textIndex = 0;
        currentText = string.Empty;
    }

    public void Talk()
    {
        string sentence = GetSentence();

        if (sentence == null)
            sentence = "...";
        else if (sentence == lastSentence)
            return;

        lastSentence = sentence;
        text.text = sentence;

        if (sentenceIndex >= names.Count)
            return;
        nameBox.text = names[sentenceIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
            Destroy(gameObject);

        if (Input.GetMouseButtonDown(0))
            NextSentence();
    }

    private void FixedUpdate()
    {
        Talk();
        ticks++;
    }
}
