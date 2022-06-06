using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float forceValue;
    public float jumpValue;
    private Rigidbody rb;
    private AudioSource audiosource;
    [SerializeField] private FixedJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //(for kinetic)transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical")* speed * Time.deltaTime);

        //Jump for pc
        if(Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
            audiosource.Play();
        }

        //quit game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Jump for mobile(for screen)
        /*if (Input.touchCount == 1)
            if(Input.touches[0].phase == TouchPhase.Began && Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
                audiosource.Play();
            }*/
    }

    public void jump()
    {   
        if(Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
                audiosource.Play();
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
            print("Collision");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Reward")
        {
            print("Collision");
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("I'm inside the area!");
    }
}
