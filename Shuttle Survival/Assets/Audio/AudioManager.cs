using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    AudioSource audioSource;
    [SerializeField] AudioClip shipEventWindowPopupSFX;
    [SerializeField] AudioClip computerBootingSFX;
    [SerializeField] AudioClip unlockDoorSFX;
    [SerializeField] AudioClip asteroidBlowUpSFX;
    [SerializeField] AudioClip characterHurtSFX;
    [SerializeField] AudioClip buttonClickSFX;




    private void Awake()
    {
        if(audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(this);
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySoundEffect(SoundEffectsType soundEffectsType)
    {
        AudioClip audioToPlay = null;
        switch (soundEffectsType)
        {
            case SoundEffectsType.ShipEventWindowPopup:
                audioToPlay = shipEventWindowPopupSFX;
                break;
            case SoundEffectsType.AsteroidBlowUp:
                audioToPlay = asteroidBlowUpSFX;
                break;
            case SoundEffectsType.Hurt:
                audioToPlay = characterHurtSFX;
                break;
            case SoundEffectsType.UnlockDoor:
                audioToPlay = unlockDoorSFX;
                break;
            case SoundEffectsType.SuccessDungeonEvent:
                break;
            case SoundEffectsType.FailureDungeonEvent:
                break;
            case SoundEffectsType.EventPopup:
                break;
            case SoundEffectsType.ButtonClick:
                audioToPlay = buttonClickSFX;
                break;
        }

        if (audioToPlay != null)
            audioSource.PlayOneShot(audioToPlay);
    }

    public void PlaySoundEffect(int enumIndex)
    {
        PlaySoundEffect(enumIndex);
        //cause un stack overflow please fix
    }


}
