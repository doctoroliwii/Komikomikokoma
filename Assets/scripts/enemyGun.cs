using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGun : MonoBehaviour
{   
    public bullet bullet;
    Vector2 direction;

    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = (transform.localRotation * Vector2.right).normalized;

        if (gameObject.transform.position.x < 16)
        {
            autoShoot = true;
        }

        if (gameObject.transform.position.x > 16)
        {
            autoShoot = false;
        }



            if (autoShoot)
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSeconds)
                {
                    Shoot();                  
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }

            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        bullet goBullet = go.GetComponent<bullet>();
        goBullet.direction = direction;
    }
}
