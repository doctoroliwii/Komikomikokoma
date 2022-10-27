using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bossHP : MonoBehaviour
{

    bool neverDone = true;
    bool canBeDestroyed = false;
    bool bossFase2 = false;
    [SerializeField] float health, maxHealth = 900f;
    
    public AudioManager aM;

    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;

    public Animator anim;

    public GameObject bulletDestroy;
    public GameObject explosion;
    public GameManager scoreGame;
    public GameObject enemHit;

    public GameObject yunque;

    public SpriteRenderer spriteX;

    public enemyGun eGun1;
    public enemyGun eGun2;
    public ship player;
    public TMP_Text credits;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        spriteX = GetComponent<SpriteRenderer>();
        

        originalMaterial = spriteX.material;

        // Update is called once per frame
    }

    void Update()
    {   
        if (neverDone)
        {
            if (transform.position.x < 15.0f)
            {
                StartCoroutine(WaitAttack());
                neverDone = false;
            }
        }

        if (transform.position.x < 15.0f)
        {
            canBeDestroyed = true;
            

        }

        if (health <= 0)
        {
            health = 0;
            DestroyBoss();
            
        }

        if (!bossFase2)
        {
            if (health <= 225)
            {
                spriteX.color = Color.red;
                bossFase2 = true;
                StartCoroutine(WaitAttack2());
                anim.SetBool("fase2", true);
                eGun1.shootIntervalSeconds = 0.2f;
                eGun2.shootIntervalSeconds = 0.2f;

                
            }
        }
    }

    void TakeDamage()
    {

        
        health -= 1;
        StartCoroutine(FlashCo());
        Instantiate(enemHit, gameObject.transform.position, Quaternion.identity);






    }

    

    void shotYunque()
    {
        GameObject go = Instantiate(yunque.gameObject, transform.position, Quaternion.identity);
        


    }

    public IEnumerator FlashCo()
    {

        if (!bossFase2)
        {
            spriteX.material = flashMaterial;
            
            yield return new WaitForSeconds(0.05f);
            spriteX.material = originalMaterial;
            
        }

        if (bossFase2)
        {
            spriteX.material = flashMaterial;
            spriteX.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            spriteX.material = originalMaterial;
            spriteX.color = Color.red;
        }
        


    }

    public IEnumerator BossAttack1()
    {

        yield return new WaitForSeconds(6);
        anim.SetBool("attack1", true);
        yield return new WaitForSeconds(3);
        anim.SetBool("attack1", false);
        StartCoroutine(WaitAttack());

    }

    public IEnumerator BossAttack2()
    {

        yield return new WaitForSeconds(3);
        anim.SetBool("attack2", true);
        yield return new WaitForSeconds(3);
        anim.SetBool("attack2", false);
        StartCoroutine(WaitAttack2());

    }

    public IEnumerator WaitAttack()
    {
        if (!bossFase2)
        {
            yield return new WaitForSeconds(6);
            StartCoroutine(BossAttack1());
        }
        
    }

    public IEnumerator WaitAttack2()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(BossAttack2());
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
                
                Destroy(bullet.gameObject);
                Instantiate(bulletDestroy, bullet.transform.position, Quaternion.identity);
                aM.enemyDamageSFXaudiosource.clip = aM.enemyDamage;
                aM.enemyDamageSFXaudiosource.outputAudioMixerGroup = aM.enemyDamageSFXaudiomixergroup;
                aM.enemyDamageSFXaudiosource.Play();



            }
          
        }

            
    }

    void DestroyBoss()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        //StartCoroutine(CreditosFinales());
        scoreGame.Credits();

        aM.boomSFXaudiosource.clip = aM.boomsfx;
        aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
        aM.boomSFXaudiosource.Play();

        scoreGame.score += 1000;

        aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
        aM.BGMaudiosource.Stop();


        //player = GetComponent<ship>();
        player.invincible = true;
        aM.BGMaudiosource.clip = aM.creditosBGM;
        aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
        aM.BGMaudiosource.Play();
        neverDone = false;

        scoreGame.scoreUI.gameObject.SetActive(false);

        Destroy(gameObject);
    }

   


}
