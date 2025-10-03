using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTalk : MonoBehaviour
{
    public Text text;
    public int talkSpeed = 1;
    public TextAsset file;
    private List<string> sentences;

    private int textIndex = 0;
    private string currentText = string.Empty;
    private int sentenceIndex = 0;
    private string lastSentence = string.Empty;
    public bool finished = false;

    private long lastUpdateTime;

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
        talkSpeed *= 100000; // Sorry magic fix
        ResetVars();
    }

    private string GetSentence()
    {
        long currentTime = DateTime.Now.Ticks;

        if (sentenceIndex >= sentences.Count)
        {
            finished = true;
            return null;
        }

        string current = sentences[sentenceIndex];
        if (textIndex < current.Length && currentTime - lastUpdateTime > talkSpeed)
        {
            lastUpdateTime = currentTime;
            currentText += current[textIndex];
            textIndex++;
        }

        return currentText;
    }

    private void NextSentence()
    {
        sentenceIndex++;
        textIndex = 0;
        currentText = string.Empty;
        lastUpdateTime = 0;
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
            Destroy(this);

        if (Input.GetMouseButtonDown(0))
            NextSentence();

        Talk();
    }
}
