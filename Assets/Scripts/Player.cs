using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float x = 0f;
    public float y = 0f;
    public float vx = 0f;
    public float vy = 0f;
    //public float speed = 2f;
    public int key = 0;
    
    private Transform playerTransform;
    private Rigidbody2D rb;
    //private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        //collider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float Sideways = Input.GetAxis("Horizontal");
        float UpDown = Input.GetAxis("Vertical");
        Movement();
    }

    void Movement()
    {
        vx *= 0.95f;
        vy *= 0.95f;
        playerTransform.transform.Translate( vx * Time.deltaTime * Vector3.right);
        playerTransform.transform.Translate(vy * Time.deltaTime * Vector3.up);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vx -= 0.5f;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            vx += 0.5f;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            vy += 0.5f;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            vy -= 0.5f;
        }

        //x += vx;
        //y += vy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boid"))
        {
            Destroy(this);
            Debug.Log("Touch me!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other);
            key = 1;
            Debug.Log("Key Got!");
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Door"))
        {
            if (key == 1)
            {
                Destroy(this);
                Debug.Log("Did Door");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}