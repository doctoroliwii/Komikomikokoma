using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2hp : MonoBehaviour
{
    
   
    bool canBeDestroyed = false;
    [SerializeField] float health, maxHealth = 9f;
    public AudioManager aM;

    public Animator anim;

    public GameObject bulletDestroy;
    public GameObject explosion;
    public GameManager scoreGame;
    public GameObject enemHit;

    public int enemyScore = 10;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        

        // Update is called once per frame
    }

    void Update()
    {   


        if (transform.position.x < 17.0f)
        {
            canBeDestroyed = true;

        }

        if (health <= 0)
        {
            health = 0;
            DestroyEnemy2();
            StartCoroutine(scoreGame.TextFlash());

        }

    }

    void TakeDamage()
    {

        
        health -= 1;
        


        
    }

    void OnTriggerEnter2D(Collider2D collision)
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
                TakeDamage();
                anim.SetTrigger("flash");
                Destroy(bullet.gameObject);
                Instantiate(bulletDestroy, bullet.transform.position, Quaternion.identity);
                Instantiate(enemHit, gameObject.transform.position, Quaternion.identity);
                aM.enemyDamageSFXaudiosource.clip = aM.enemyDamage;
                aM.enemyDamageSFXaudiosource.outputAudioMixerGroup = aM.enemyDamageSFXaudiomixergroup;
                aM.enemyDamageSFXaudiosource.Play();
                




            }
          
        }

            
    }

    void DestroyEnemy2()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        
        aM.boomSFXaudiosource.clip = aM.boomsfx;
        aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
        aM.boomSFXaudiosource.Play();

        scoreGame.score += enemyScore;
        
        Destroy(gameObject);

    }


}
