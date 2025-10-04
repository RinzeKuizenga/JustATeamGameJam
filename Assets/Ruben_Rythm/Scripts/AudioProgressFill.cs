using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioProgressFill : MonoBehaviour
{
    public AudioSource audioSource;
    public Image progressImage;

    private bool audioStarted = false;

    private void Start()
    {
        progressImage.gameObject.SetActive(false);
    }
    void Update()
    {

        if (audioStarted)
        {
            progressImage.gameObject.SetActive(true);
            progressImage.fillAmount = Mathf.Clamp01(audioSource.time / audioSource.clip.length);
        }
    }
}
