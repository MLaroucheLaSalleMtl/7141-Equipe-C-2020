using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameParameter;
    private Slider slide;

    void Start()
    {
        slide = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParameter, 0);
        SetVolume(v);
    }

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat(nameParameter, vol);
        slide.value = vol;
        PlayerPrefs.SetFloat(nameParameter, vol);
    }
}
