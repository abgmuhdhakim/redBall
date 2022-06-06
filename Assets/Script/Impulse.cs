using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : MonoBehaviour
{
    public GameObject stone;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(stone, transform.position, Random.rotation);
        }
    }
}
