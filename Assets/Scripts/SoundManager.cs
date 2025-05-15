using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip audio;
    private AudioSource controlAudio;

    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
    }

    public void SeleccionAudio()
    {
        controlAudio.PlayOneShot(audio, 0.3f);
    }
}
