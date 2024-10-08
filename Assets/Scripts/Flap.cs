using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;

public class Flap : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject bird;
    private int score = 0;
    [SerializeField] private TMP_Text scoreText;
    public AudioSource audioSource;
    public AudioClip scoreIncrease;
    public AudioClip flap;
    private Animator birdAnimator;
    private bool firstInput = false;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        birdAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        paused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (firstInput == false)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                firstInput = true;
            }
            rb.velocity = new Vector2(0f, 10f);
            birdAnimator.SetTrigger("Flap");

            // bird.transform.Rotate(0, 0, 50);
            // audioSource.PlayOneShot(flap);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            paused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            paused = false;
            Time.timeScale = 1;
        }

        if (Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("Main Menu");
        }

        // if (rb.velocity.y < -15)
        // {
        //     bird.transform.Rotate(0, 0, -50);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Score Area"))
        {
            score++;
            scoreText.SetText(score.ToString());
            audioSource.PlayOneShot(scoreIncrease);
        }
    }
    public void disableCollisions()
    {
    }
}
