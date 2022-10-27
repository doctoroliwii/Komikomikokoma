using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public GameObject bulletDestroy;
    
    public Vector2 direction = new Vector2(1,0);
    public float speed = 3;

    public Vector2 velocity;

    public bool isEnemy = false;

    public GameObject stelafx;


    // Start is called before the first frame update
    void Start()
    {   
        if (!isEnemy)
        {
            Destroy(gameObject, 3);
        }

        if (isEnemy)
        {
            Destroy(gameObject, 9);
        }

        StartCoroutine(stela());
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }

    public IEnumerator stela()
    {
        

        while (gameObject != null)
        {
            
            yield return new WaitForSeconds(0.063f);
            Instantiate(original: stelafx, gameObject.transform.position, Quaternion.identity);
            

        }

    }
}
