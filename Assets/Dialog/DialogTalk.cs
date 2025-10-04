using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTalk : MonoBehaviour
{
    public Text text;
    public int tickDelay = 1;
    public TextAsset file;
    public Move playerMoveComponent;
    public List<string> sentences;

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
        var all = content.Split("\n");
        sentences = new List<string>(all);
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
            Debug.Log(ticks);
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
