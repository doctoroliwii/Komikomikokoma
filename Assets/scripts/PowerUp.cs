using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool activateShield;
    public bool addGuns;
    public bool machineGun;

    public GameObject powerUpShine1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void shine1()
    {
        Instantiate(powerUpShine1, transform.position, Quaternion.identity);
    }
}
