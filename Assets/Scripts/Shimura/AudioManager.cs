using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip mainBGM;
    public AudioClip evolutionBGM;
    public AudioClip buttonclick;

    [Header("BGM")]
    public AudioSource bgmSource;
    public float bgmFadeDuration = 1f;

    [Header("SE")]
    public AudioSource seSource; // PlayOneShot用

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // -------------------
    // BGM再生
    // -------------------
    public void PlayBGM(AudioClip clip, bool loop = true, float fadeDuration = -1f)
    {
        if (clip == null) return;
        if (fadeDuration < 0) fadeDuration = bgmFadeDuration;

        // 既存BGMをフェードアウト
        bgmSource.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
            bgmSource.volume = 0f;
            bgmSource.DOFade(1f, fadeDuration);
        });
    }

    public void StopBGM(float fadeDuration = -1f)
    {
        if (fadeDuration < 0) fadeDuration = bgmFadeDuration;
        bgmSource.DOFade(0f, fadeDuration).OnComplete(() => bgmSource.Stop());
    }

    // -------------------
    // SE再生
    // -------------------
    public void PlaySE(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        seSource.PlayOneShot(clip, volume);
    }

    public void StopSE()
    {
        seSource.Stop();
    }

    public void ButtonClick()
    {
        PlaySE(buttonclick);
    }
}
