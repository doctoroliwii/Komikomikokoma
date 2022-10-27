using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{

    private void Start()
    {
        RandomRotation();
    }

    void RandomRotation()
    {
        Quaternion randRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = randRotation;
    }
    void ExplosionDone()
    {
        Destroy(gameObject);
    }
}
