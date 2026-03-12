using System.Transactions;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Tooltip("Turn speed (degrees/sec)")]
    public float rotationSpeed = 0.5f;
    public GameObject onCollectEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotationSpeed,0);
    }

    //Runs when an object run into the trigger collider zone of this object and one of these objects
    //has a has a rigidBody Component attached
    private void OnTriggerEnter(Collider other)
    {
        //Run this code only If the 'Other' object that triggers this has the Player Tag
        if (other.CompareTag("Player"))
        {
            //Destroy the Collectible 
            Destroy(gameObject);
            //Instantiate the Particle Effect with the same location and rotation of this Collectible
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }
    }
}
