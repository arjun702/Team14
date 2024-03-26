using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDart : MonoBehaviour
{
private Rigidbody rb;
    
    public float force = 1000f;
    public string dartBoardTag = "DartBoard";

    private bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
        
        // Check if the collision is with the dart board
        if (collision.gameObject.CompareTag(dartBoardTag))
        {
            // Get the contact point where the collision occurred
            // ContactPoint contact = collision.contacts[0];
            // Vector3 hitPosition = contact.point;
            
            // // Now you have the hit position on the dart board
            // Debug.Log("Dart hit position: " + hitPosition);

        }

        foreach (ContactPoint contact in collision.contacts)
        {
            if (collision.gameObject.CompareTag(dartBoardTag))
            {
                // Calculate the vector from the center of the collided object to the contact point
                Vector3 vectorToContactPoint = contact.point - collision.transform.position;
                    
                // Calculate the Euclidean distance (magnitude) of the vector
                float distance = vectorToContactPoint.magnitude;

                Debug.Log("Distance from center to collision point: " + distance);
            }
        }
        
    }
}
