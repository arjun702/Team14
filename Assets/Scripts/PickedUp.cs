using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour
{
    public bool pickedUp = false;
    public Vector3 pivot = new Vector3(0f, 0f, 0f);

    public float force = 1000f;

    public GameObject Ray;
    public GameObject Hand;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp)
        {
            transform.position = Ray.GetComponent<RayCastPointer>().rayPivot;

            if(Input.GetKeyDown(KeyCode.L))
            {
                rb.AddForce(-1 * Hand.transform.up * force);
                pickedUp = false;

                UnfreezePosition();
            }
        }
    }

    void UnfreezePosition()
    {
        // Unfreeze the position along the X axis
        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        
        // Unfreeze the position along the Y axis
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        
        // Unfreeze the position along the Z axis
        rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
    }

}
