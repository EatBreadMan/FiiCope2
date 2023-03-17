using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractableSprite : MonoBehaviour
{
    public SpriteRenderer glow;
    public SpriteRenderer InteractKey;
    private bool inRange = false;
    private void Update()
    {
        if (inRange = true && Input.GetKey(KeyCode.E))
            LoadNextScene();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            glow.enabled = true; 
            InteractKey.enabled = true;
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            glow.enabled = false;
            InteractKey.enabled = false;
        }
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
