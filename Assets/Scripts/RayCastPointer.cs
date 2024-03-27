using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RayCastPointer : MonoBehaviour
{
    public float maxRayDistance = 10f; // Maximum length of the ray trace line

    private LineRenderer lineRenderer;
    public GameObject Hand;
    public GameObject HandPivot;
    public GameObject Character;
    public GameObject Dart;

    public Vector3 rayPivot = new Vector3(0f, 0f, 0f);

    private CharacterMovement charMovement;



    // public event Action<GameObject> OnLineHitObject;

    void Start()
    {
        // Ensure we have LineRenderer component attached
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set up initial line renderer properties
        lineRenderer.positionCount = 2; // Two points for a straight line
        lineRenderer.startWidth = 0.2f; // Adjust as necessary
        lineRenderer.endWidth = 0.02f; // Adjust as necessary
        lineRenderer.material.color = Color.white; // Set the color to white

        // Remove the shadow from the line renderer material
        lineRenderer.receiveShadows = false;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        charMovement = Character.GetComponent<CharacterMovement>();
    }

    void Update()
    {
        // Vector3 cameraPosition = Camera.main.transform.position;

        // Vector3 directionToCamera = cameraPosition - transform.position;

        HandPivot.transform.rotation = Camera.main.transform.rotation;

        // Get the hand's position
        Vector3 handPosition = Hand.transform.position;

        // Get the hand's forward direction
        Vector3 handForward = -1 * Hand.transform.up;

        // Calculate the end position of the ray
        Vector3 rayEnd = handPosition + handForward * maxRayDistance;

        lineRenderer.SetPosition(0, handPosition);
        lineRenderer.SetPosition(1, rayEnd);

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(handPosition, handForward, out hit, maxRayDistance))
        {
            // End the last point of ray at object hit point
            // rayEnd = hit.point;

            rayPivot = rayEnd;

            // If the ray hits something, set the end position of the line to the point of intersection
            lineRenderer.SetPosition(0, handPosition);
            lineRenderer.SetPosition(1, rayEnd);

            // Detect which object the end of the line touches
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Hit Object: " + hitObject.name);

            // Enable the Outline component if it exists
            Outline outline = hitObject.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
            }

            // Disable Outlines
            if(hitObject.tag != "Outline Objects")
            {
                // Find all game objects with the tag "OutlineObjects"
                GameObject[] outlineObjects = GameObject.FindGameObjectsWithTag("Outline Objects");

                // Loop through each object found
                foreach (GameObject obj in outlineObjects)
                {
                    // Check if the object has an "Outline" component
                    Outline outlineComponent = obj.GetComponent<Outline>();
                    if (outlineComponent != null)
                    {
                        // Disable the "Outline" component
                        outlineComponent.enabled = false;
                    }
                }
            }

            if(hitObject.name == "Dart" && Input.GetKeyDown(KeyCode.P))
            {
                Dart.GetComponent<PickedUp>().pivot = rayEnd;
                Dart.GetComponent<PickedUp>().pickedUp = true;
            }

        }
 
    }

}
