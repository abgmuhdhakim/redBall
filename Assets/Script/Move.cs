using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Move : MonoBehaviour
{
    public float forceValue;
    public float jumpValue;
    public float coin;
    public float health;
    public TextMeshProUGUI OutputText;
    public TextMeshProUGUI OutputText2;
    private Rigidbody rb;
    AudioSource audios;
    bool yes;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip hit;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] GameObject Ouch;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audios = GetComponent<AudioSource>();
        health = 10f;
        yes = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        //(for kinetic)transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical")* speed * Time.deltaTime);

        //Jump for pc
        if(Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
            audios.PlayOneShot(jumpSound);
        }

        //quit game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        OutputText.text = coin.ToString();
        OutputText2.text = health.ToString();
        if(yes == true)
        {
            Invoke("damm",0.1f);
        }
        //Jump for mobile(for screen)
        /*if (Input.touchCount == 1)
            if(Input.touches[0].phase == TouchPhase.Began && Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
                audiosource.Play();
            }*/
    }
    
    void damm()
    {
        Ouch.SetActive(false);
        yes = false;
    }

    public void jump()
    {   
        if(Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
                audios.PlayOneShot(jumpSound);
            }
    }

    
    private void FixedUpdate()
    {
        //Move for pc
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * forceValue);

        //Move for mobile
        rb.AddForce(new Vector3( joystick.Horizontal,0,joystick.Vertical) * forceValue);

        //(for mobile rotation)rb.AddForce(new Vector3(Input.acceleration.x, 0, Input.acceleration.y) * forceValue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            health--;
            Ouch.SetActive(true);
            yes = true;
            audios.PlayOneShot(hit);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Reward")
        {
            audios.PlayOneShot(coinSound);
            Destroy(collision.gameObject);
            coin ++;
        }
    }
}
