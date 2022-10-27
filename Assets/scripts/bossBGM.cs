using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class bossBGM : MonoBehaviour
{
    public float moveSpeed = 5;
    bool neverDone = true;
    public AudioManager aM;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (neverDone)
        {
            
            ship ship = collision.GetComponent<ship>();
            if (ship != null)
            {
                aM.BGMaudiosource.clip = aM.bossBGM;
                aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
                aM.BGMaudiosource.Play();
                aM.BGMaudiosource.loop = true;
                aM.BGMaudiosource.volume = 1.1f;
                neverDone = false;

            }
        }
        
        
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;

        if (pos.x < -2)
        {
            Destroy(gameObject);
        }


        transform.position = pos;
    }
}
