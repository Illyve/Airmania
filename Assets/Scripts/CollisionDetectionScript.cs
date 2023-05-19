using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionScript : MonoBehaviour
{
    public Transform explosionPrefab;
    public Rigidbody rb;
    public GameObject shatteredAirplane;
    Boolean hasHit = false;
    // Start is called before the first frame update
    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }

    // Checks for collision and then transforms into an explosion particle effect.
    // Finds the contact point and rotates the airplane in response to collision 
    // deletes the object afterwards
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destructible") && hasHit == false)
        {
            Debug.Log("Hit A Destructible!");
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(explosionPrefab, position, rotation);
            Instantiate(explosionPrefab, rb.position, rb.rotation);
            Instantiate(shatteredAirplane, rb.position, rb.rotation);
            GetComponent<Rigidbody>().AddExplosionForce(10000, position, 5000, 5.0f);
            hasHit = true;
            Destroy(gameObject);          
        }
    }
}
