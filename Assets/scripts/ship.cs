using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ship : MonoBehaviour
{

    Gun[] guns;

    public float moveSpeed = 9;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    public GameManager gameM;
    GameObject shield;
    public SpriteRenderer shieldColor;

    bool shoot;

    public Animator anim;

    public GameObject explosion;
    public GameObject upgradeFX;
    public GameObject damagePlayer;
    
    public AudioManager aM;
    public CameraShake cameraShake;
    public GameManager scoreGame;

    public int powerUpGunLevel = 0;
    public bool autoShoot = false;
    public bool invincible = false;

    public int ShieldLevel = 0;
    public int autoShootLevel = 0;

    public float shootIntervalSeconds = 0.1f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    [Header("Iframe Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D damageCollider;
    public Collider2D upgradeCollider;
    public SpriteRenderer mySprite;
    public GameObject spriteX;
    public SpriteRenderer upgradeSprite;

    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;





    // Start is called before the first frame update
    void Start()
    {
        gameM = GameManager.FindObjectOfType<GameManager>();
        shield = transform.Find("Shield").gameObject;
        guns = transform.GetComponentsInChildren<Gun>();
        //shieldColor = shield.GetComponent<SpriteRenderer>(); 
        DeactivateShield();
        foreach (Gun gun in guns)
        {
            gun.isActive = true;
            if (gun.powerUpLevelRequirement != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }
        
        damageCollider.enabled = true;

        originalMaterial = mySprite.material;

        autoShootLevel = 0;
        powerUpGunLevel = 0;
        ShieldLevel = 0;

    }

    // Update is called once per frame
    void Update()
    {
        #region Controles
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

     
        #endregion


        #region Disparo
        if (autoShoot == false)
        {
            shoot = Input.GetKeyDown(KeyCode.X);
            aM.SFXaudiosource.volume = 0.9f;
            if (shoot)
            {
                anim.SetTrigger("shooting");
                shoot = false;
                foreach (Gun gun in guns)
                {

                    if (gun.gameObject.activeSelf)
                    {
                        gun.Shoot();
                    }




                }
            }
        }

        if (autoShoot == true)
        {
            shoot = Input.GetKeyDown(KeyCode.X);
            aM.SFXaudiosource.volume = 0.9f;
            if (shoot)
            {
                anim.SetTrigger("shooting");
                shoot = false;
                foreach (Gun gun in guns)
                {

                    if (gun.gameObject.activeSelf)
                    {
                        gun.Shoot();
                    }




                }
            }
        }


        if (autoShoot == true)
        {
            
            shoot = Input.GetKey(KeyCode.X);
            aM.SFXaudiosource.volume = 0.3f;
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSeconds)
                {
                    
                    if (shoot)
                    {
                        anim.SetTrigger("shooting");
                        shoot = false;
                        foreach (Gun gun in guns)
                        {

                            if (gun.gameObject.activeSelf)
                            {
                                gun.Shoot();
                            }




                        }
                        shootTimer = 0;
                    }
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }

            }

           




            }
        #endregion

        #region Escudo
        if (ShieldLevel <= 0 & HasShield())
        {
            DeactivateShield();
            ShieldLevel = 0;
        }

        if (ShieldLevel >= 1 & HasShield())
        {
            shieldColor.color = new Color(0,255,190,0.6f);  
        }

        if (ShieldLevel >= 2)
        {
            shieldColor.color = new Color(0,255,0,0.6f);
            
        }

        if (ShieldLevel >= 3)
        {
            shieldColor.color = new Color(255, 108, 0, 0.6f);
        }

        if (ShieldLevel >= 4)
        {
            shieldColor.color = new Color(255, 0, 108, 0.6f);
        }


        #endregion

        foreach (Gun gun in guns)
        {
            if (gun.powerUpLevelRequirement > powerUpGunLevel)
            {
                gun.gameObject.SetActive(false);
            }

            if (gun.powerUpLevelRequirement <= powerUpGunLevel)
            {
                gun.gameObject.SetActive(true);
            }


        }

        if (powerUpGunLevel < 0)
        {
            powerUpGunLevel = 0;
        }

        if (powerUpGunLevel > 2)
        {
            powerUpGunLevel = 2;
        }


        #region MACHINE GUN
        if (autoShootLevel <= 0)
        {
            autoShoot = false;
            autoShootLevel = 0;
            shootIntervalSeconds = 0.18f;
        }

        if (autoShootLevel >= 1 & autoShoot == true)
        {
            shootIntervalSeconds = 0.18f;
        }

        if (autoShootLevel >= 2 & autoShoot == true)
        {
            shootIntervalSeconds = 0.09f;
        }

        if (autoShootLevel >= 3 & autoShoot == true)
        {
            shootIntervalSeconds = 0.045f;
        }

        if (autoShootLevel > 3)
        {
            autoShootLevel = 3;
        }

        #endregion
    }



    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x -= moveAmount;
        }

        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }
        pos += move;

        if (pos.x <= 0.5f)
        {
            pos.x = 0.5f;
        }

        if (pos.x >= 17.5f)
        {
            pos.x = 17.5f;
        }

        if (pos.y <= 0.5f)
        {
            pos.y = 0.5f;
        }

        if (pos.y >= 8.5f)
        {
            pos.y = 8.5f;
        }

        transform.position = pos;
    }

    #region UPGRADES
    void ActivateShield()
    {
        shield.SetActive(true);
    }

    void DeactivateShield()
    {
       
        shield.SetActive(false);      
        
        
        
        foreach (Gun gun in guns)
        {
            if (gun.powerUpLevelRequirement != powerUpGunLevel)
            {
                gun.gameObject.SetActive(false);
            }
        }

        
        




    }

    bool HasShield()
    {
        return shield.activeSelf;
    }

    void AddGuns()
    {
        powerUpGunLevel++;
        foreach (Gun gun in guns)
        {
            if (gun.powerUpLevelRequirement == powerUpGunLevel)
            {
                gun.gameObject.SetActive(true);
            }


        }
    }

    void ActivateMachineGun()


    {
        autoShoot = true;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible)
        {
            bullet bullet = collision.GetComponent<bullet>();
            if (bullet != null)
            {



                if (bullet.isEnemy)
                {
                    StartCoroutine(cameraShake.Shake(.18f, .3f));
                    StartCoroutine(FlashCo());
                    if (HasShield())
                    {
                        ShieldLevel -= 1;
                        autoShootLevel -= 1;
                        powerUpGunLevel -= 1;
                        //DeactivateShield();
                        Destroy(bullet.gameObject);
                        aM.powerUpSFXaudiosource.clip = aM.powerDownSFX;
                        aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
                        aM.powerUpSFXaudiosource.Play();

                    }
                    else
                    {
                        gameM.Die();
                        Destroy(gameObject);
                        Destroy(bullet.gameObject);

                        aM.boomSFXaudiosource.clip = aM.boomsfx;
                        aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                        aM.boomSFXaudiosource.Play();
                        aM.BGMaudiosource.clip = aM.BGM;
                        aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
                        aM.BGMaudiosource.Stop();
                        Instantiate(explosion, transform.position, Quaternion.identity);
                    }




                }


            }

            Destructable destructable = collision.GetComponent<Destructable>();
            if (destructable != null)
            {
                StartCoroutine(cameraShake.Shake(.18f, .3f));
                StartCoroutine(FlashCo());
                if (damagePlayer.activeSelf != true)
                {
                    damagePlayer.gameObject.SetActive(true);
                }

                if (damagePlayer.activeSelf == true)
                {
                    damagePlayer.gameObject.SetActive(false);
                    damagePlayer.gameObject.SetActive(true);

                }
                if (HasShield())
                {
                    ShieldLevel -= 1;
                    autoShootLevel -= 1;
                    powerUpGunLevel -= 1;
                    //DeactivateShield();
                    Destroy(destructable.gameObject);
                    Instantiate(explosion, destructable.transform.position, Quaternion.identity);
                    aM.boomSFXaudiosource.clip = aM.boomsfx;
                    aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                    aM.boomSFXaudiosource.Play();
                    aM.powerUpSFXaudiosource.clip = aM.powerDownSFX;
                    aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
                    aM.powerUpSFXaudiosource.Play();
                }
                else
                {
                    gameM.Die();

                    Destroy(gameObject);

                    aM.boomSFXaudiosource.clip = aM.boomsfx;
                    aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                    aM.boomSFXaudiosource.Play();
                    aM.BGMaudiosource.clip = aM.BGM;
                    aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
                    aM.BGMaudiosource.Stop();

                    Instantiate(explosion, transform.position, Quaternion.identity);

                    Destroy(destructable.gameObject);
                    Instantiate(explosion, destructable.transform.position, Quaternion.identity);




                }



                //Destroy(bullet.gameObject);

            }

            enemy2hp enemy2 = collision.GetComponent<enemy2hp>();
            if (enemy2 != null)
            {
                StartCoroutine(cameraShake.Shake(.18f, .3f));
                StartCoroutine(FlashCo());
                if (damagePlayer.activeSelf != true)
                {
                    damagePlayer.gameObject.SetActive(true);
                }

                if (damagePlayer.activeSelf == true)
                {
                    damagePlayer.gameObject.SetActive(false);
                    damagePlayer.gameObject.SetActive(true);

                }
                if (HasShield())
                {
                    ShieldLevel -= 1;
                    autoShootLevel -= 1;
                    powerUpGunLevel -= 1;
                    //DeactivateShield();
                    Destroy(enemy2.gameObject);
                    Instantiate(explosion, enemy2.transform.position, Quaternion.identity);
                    aM.boomSFXaudiosource.clip = aM.boomsfx;
                    aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                    aM.boomSFXaudiosource.Play();
                    aM.powerUpSFXaudiosource.clip = aM.powerDownSFX;
                    aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
                    aM.powerUpSFXaudiosource.Play();


                }
                else
                {

                    gameM.Die();
                    Destroy(gameObject);
                    Destroy(enemy2.gameObject);

                    aM.boomSFXaudiosource.clip = aM.boomsfx;
                    aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                    aM.boomSFXaudiosource.Play();
                    aM.BGMaudiosource.clip = aM.BGM;
                    aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
                    aM.BGMaudiosource.Stop();

                    Instantiate(explosion, transform.position, Quaternion.identity);
                }


            }

            MoveRIghtToLeft2 cactus = collision.GetComponent<MoveRIghtToLeft2>();
            if (cactus != null)
            {
                StartCoroutine(cameraShake.Shake(.18f, .3f));
                StartCoroutine(FlashCo());
                if (damagePlayer.activeSelf != true)
                {
                    damagePlayer.gameObject.SetActive(true);
                }

                if (damagePlayer.activeSelf == true)
                {
                    damagePlayer.gameObject.SetActive(false);
                    damagePlayer.gameObject.SetActive(true);

                }
                if (HasShield())
                {
                    ShieldLevel -= 1;
                    autoShootLevel -= 1;
                    powerUpGunLevel -= 1;
                    //DeactivateShield();

                    Instantiate(explosion, transform.position, Quaternion.identity);
                    aM.boomSFXaudiosource.clip = aM.boomsfx;
                    aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                    aM.boomSFXaudiosource.Play();
                    aM.powerUpSFXaudiosource.clip = aM.powerDownSFX;
                    aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
                    aM.powerUpSFXaudiosource.Play();


                }
                else
                {

                    gameM.Die();
                    Destroy(gameObject);

                    aM.boomSFXaudiosource.clip = aM.boomsfx;
                    aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                    aM.boomSFXaudiosource.Play();
                    aM.BGMaudiosource.clip = aM.BGM;
                    aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
                    aM.BGMaudiosource.Stop();

                    Instantiate(explosion, transform.position, Quaternion.identity);

                }

                MoveRIghtToLeft3 patoboss = collision.GetComponent<MoveRIghtToLeft3>();
                if (patoboss != null)
                {
                    StartCoroutine(cameraShake.Shake(.18f, .3f));
                    StartCoroutine(FlashCo());
                    if (damagePlayer.activeSelf != true)
                    {
                        damagePlayer.gameObject.SetActive(true);
                    }

                    if (damagePlayer.activeSelf == true)
                    {
                        damagePlayer.gameObject.SetActive(false);
                        damagePlayer.gameObject.SetActive(true);

                    }
                    if (HasShield())
                    {

                        ShieldLevel -= 1;
                        autoShootLevel -= 1;
                        powerUpGunLevel -= 1;
                        

                        //DeactivateShield();

                        Instantiate(explosion, transform.position, Quaternion.identity);
                        aM.boomSFXaudiosource.clip = aM.boomsfx;
                        aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                        aM.boomSFXaudiosource.Play();
                        aM.powerUpSFXaudiosource.clip = aM.powerDownSFX;
                        aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
                        aM.powerUpSFXaudiosource.Play();


                    }
                    else
                    {

                        gameM.Die();
                        Destroy(gameObject);

                        aM.boomSFXaudiosource.clip = aM.boomsfx;
                        aM.boomSFXaudiosource.outputAudioMixerGroup = aM.boomSFXaudiomixergroup;
                        aM.boomSFXaudiosource.Play();
                        aM.BGMaudiosource.clip = aM.BGM;
                        aM.BGMaudiosource.outputAudioMixerGroup = aM.BGMaudiomixergroup;
                        aM.BGMaudiosource.Stop();

                        Instantiate(explosion, transform.position, Quaternion.identity);


                    }


                }
            }
        }
        

        #region POWERUPS
        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp)
        {


            if (upgradeFX.activeSelf != true)
            {
                upgradeFX.gameObject.SetActive(true);
            }

            if (upgradeFX.activeSelf == true)
            {
                upgradeFX.gameObject.SetActive(false);
                upgradeFX.gameObject.SetActive(true);

            }


            StartCoroutine(PlayerColors());
            
            StartCoroutine(gameM.TextFlash());
            
            scoreGame.score += 250;
            if (powerUp.activateShield)
            {
                ActivateShield();
                upgradeSprite.color = Color.cyan;
                ShieldLevel++;
                aM.powerUpSFXaudiosource.clip = aM.powerUpsfx;
                aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
                aM.powerUpSFXaudiosource.Play();
            }
            if (powerUp.addGuns)
            {
                AddGuns();
                upgradeSprite.color = Color.magenta;
                aM.powerUpSFXaudiosource.clip = aM.powerUpsfx;
                aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
                aM.powerUpSFXaudiosource.Play();
            }

        if (powerUp.machineGun)
            {
                ActivateMachineGun();
                upgradeSprite.color = Color.green;
                aM.powerUpSFXaudiosource.clip = aM.powerUpsfx;
                aM.powerUpSFXaudiosource.outputAudioMixerGroup = aM.powerUpSFXaudiomixergroup;
                aM.powerUpSFXaudiosource.Play();
                autoShootLevel++;
            }
           
            Destroy(powerUp.gameObject);
            
        }
        #endregion

    }



    public IEnumerator FlashCo()
    {
        int temp = 0;
        //damageCollider.enabled = false;
        invincible = true;
        while (temp < numberOfFlashes)
        {
            spriteX.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
            spriteX.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            temp++;


        }
        invincible = false;
        //damageCollider.enabled = true;
    }

    public IEnumerator PlayerColors()
    {
        mySprite.material = flashMaterial;
        mySprite.color = Color.yellow;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.magenta;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.cyan;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.yellow;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.magenta;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.cyan;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        mySprite.material = originalMaterial;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.magenta;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.cyan;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        mySprite.material = flashMaterial;
        mySprite.color = Color.yellow;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.cyan;
        yield return new WaitForSeconds(0.05f);
        mySprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        mySprite.material = originalMaterial;
        yield return new WaitForSeconds(0.05f);
    }


}
