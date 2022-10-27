using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmove : MonoBehaviour
{
    public float moveSpeed = 3;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;

        if (pos.x < -18)
        {
            Destroy(gameObject);
        }

        if (pos.x < 25)
        {
            moveSpeed = 2;
        }


        transform.position = pos;
    }
}
