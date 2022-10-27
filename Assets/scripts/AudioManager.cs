using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip bossBGM;
    public AudioClip playerShotsfx;
    public AudioClip boomsfx;
    public AudioClip powerUpsfx;
    public AudioClip enemyDamage;
    public AudioClip powerDownSFX;
    public AudioClip creditosBGM;
    public AudioClip titleBGM;

    public AudioSource BGMaudiosource;
    public AudioSource SFXaudiosource;
    public AudioSource boomSFXaudiosource;
    public AudioSource enemyDamageSFXaudiosource;
    public AudioSource powerUpSFXaudiosource;
    public AudioMixerGroup BGMaudiomixergroup;
    public AudioMixerGroup SFXaudiomixergroup;
    public AudioMixerGroup boomSFXaudiomixergroup;
    public AudioMixerGroup enemyDamageSFXaudiomixergroup;
    public AudioMixerGroup powerUpSFXaudiomixergroup;
    void Start()
    {
        BGMaudiosource.clip = BGM;
        BGMaudiosource.outputAudioMixerGroup = BGMaudiomixergroup;
        BGMaudiosource.Play();
        BGMaudiosource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
