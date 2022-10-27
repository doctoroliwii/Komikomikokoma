using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRIghtToLeft3: MonoBehaviour
{
    public float moveSpeed = 3;

    public GameObject bulletDestroy;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;

        if (pos.x < -9)
        {
            Destroy(gameObject);
        }

        if (pos.x < 15.0f)
        {
            moveSpeed = 0;
        }

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        bullet bullet = collision.GetComponent<bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                
                
                Destroy(bullet.gameObject);
                Instantiate(bulletDestroy, bullet.transform.position, Quaternion.identity);
                



            }

        }


    }
}
