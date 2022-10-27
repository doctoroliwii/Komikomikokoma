using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject bulletDestroy;
    public GameObject explosion;
    public GameObject enemHit;
    
    bool canBeDestroyed = false;
    public GameManager scoreGame;
    public AudioManager aM;

    public GameManager gameM;


    // Start is called before the first frame update
    void Start()
    {
        gameM = GameManager.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 17.0f)
        {
            canBeDestroyed = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (!canBeDestroyed)
        {
            return;
        }

        bullet bullet = collision.GetComponent<bullet>();
        if (bullet != null)
        {   
            if (!bullet.isEnemy)
            {

                
                
                scoreGame.score += 1;
                Destroy(bullet.gameObject);
                DestroyDestructable();
                StartCoroutine(TextFlash());
                Instantiate(bulletDestroy, bullet.transform.position, Quaternion.identity);
                Instantiate(enemHit, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);

            }
        }

    }

    void DestroyDestructable()
    {
        
        Instantiate(explosion, transform.position, Quaternion.identity);
        aM.boomSFXaudiosource.clip = aM.boomsfx;
        aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
        aM.boomSFXaudiosource.Play();
        

    }

    public IEnumerator TextFlash()
    {
        StartCoroutine(TextShake());
        gameM.scoreUI.color = Color.black;
        yield return new WaitForSeconds(0.05f);
        scoreGame.scoreUI.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        scoreGame.scoreUI.color = new Color32(180, 255, 78, 255);


    }

    IEnumerator TextShake()
    {
        gameM.aniM.SetTrigger("shake");
        yield return new WaitForSeconds(0.05f);
    }
}
