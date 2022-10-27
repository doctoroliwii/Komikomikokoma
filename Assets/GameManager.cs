using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreUI;
    public Destructable enemy1;
    public enemy2hp enemy2;
    public Animator aniM;
    public Animator aniM2;
    

    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
       scoreUI.text = "" + score.ToString("000000000");
       
       //if (Input.GetKeyDown(KeyCode.B))
       // {
       //     SceneManager.LoadScene("boss");
       // }
    }

    public void Die()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        
        SceneManager.LoadScene("Titulo");

    }

    public IEnumerator TextFlash()
    {
        StartCoroutine(TextShake());
        //scoreUI.color = Color.black;
        //yield return new WaitForSeconds(0.05f);
        //scoreUI.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        //scoreUI.color = new Color32(180, 255, 78, 255);
        

    }

    IEnumerator TextShake()
    {
        aniM.SetTrigger("shake");
        yield return new WaitForSeconds(0.05f);
    }

    public void Credits()
    {
        
        StartCoroutine(Final());
        
    }

    IEnumerator Final()
    {
        yield return new WaitForSeconds(3);
        aniM2.SetTrigger("Start");
        yield return new WaitForSeconds(36);
        SceneManager.LoadScene("Titulo");
    }

}
