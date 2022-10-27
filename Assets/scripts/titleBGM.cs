using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleBGM : MonoBehaviour
{
    public AudioManager aM;
    void Start()
    {
        aM.BGMaudiosource.clip = aM.titleBGM;
        aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
        aM.BGMaudiosource.Play();
        aM.BGMaudiosource.loop = true;
        aM.BGMaudiosource.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
