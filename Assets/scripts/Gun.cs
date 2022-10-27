using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int powerUpLevelRequirement = 0;
    public bullet bullet;
    Vector2 direction;
    public AudioManager aM;

    public bool isActive = false;
    public bool spread = false;
    public GameObject shotSpread;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        bullet goBullet = go.GetComponent<bullet>();
        goBullet.direction = direction;
        aM.SFXaudiosource.clip = aM.playerShotsfx;
        aM.SFXaudiosource.outputAudioMixerGroup = aM.SFXaudiomixergroup;
        aM.SFXaudiosource.Play();
        if (spread)
        {
         Instantiate(shotSpread.gameObject, transform.position, Quaternion.identity);
        }
    }  
}
