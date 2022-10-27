using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_cactus : MonoBehaviour
{
    public float movSpeed = 3;


    // Start is called before the first frame update
    void Start()
    {
        movSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= movSpeed * Time.fixedDeltaTime;

        if (pos.x < -18)
        {
            Destroy(gameObject);
        }

        if (pos.x < 18)
        {
            movSpeed = 3;
        }


        transform.position = pos;
    }
}
