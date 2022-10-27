using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public AudioManager aM;
    public LoadingScene loadScene;

    void Start()
    {
        aM.BGMaudiosource.clip = aM.titleBGM;
        aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
        aM.BGMaudiosource.Play();
        aM.BGMaudiosource.loop = true;
        aM.BGMaudiosource.volume = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //SceneManager.LoadScene("Game");
            aM.BGMaudiosource.clip = aM.titleBGM;
            aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
            aM.BGMaudiosource.Stop();
            aM.powerUpSFXaudiosource.clip = aM.powerUpsfx;
            aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
            aM.powerUpSFXaudiosource.Play();
            loadScene.LoadScene(1);


        }
    }

   
}
