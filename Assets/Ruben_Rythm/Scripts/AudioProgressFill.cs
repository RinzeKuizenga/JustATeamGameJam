using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioProgressFill : MonoBehaviour
{
    public AudioSource audioSource;
    public Image progressImage;
    public float delay = 4f;

    private bool hasClicked = false;
    private bool audioStarted = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClicked)
        {
            hasClicked = true;
            StartCoroutine(PlayAudioAfterDelay());
        }

        if (audioStarted)
        {
            progressImage.fillAmount = Mathf.Clamp01(audioSource.time / audioSource.clip.length);
        }
    }

    IEnumerator PlayAudioAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
        audioStarted = true;
    }
}
