using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController charCntrl;
    [Tooltip("The speed at which the character will move.")]
    public float speed = 5f;
    [Tooltip("The camera representing where the character is looking.")]
    public GameObject cameraObj;
    [Tooltip("Should be checked if using the Bluetooth Controller to move. If using keyboard, leave this unchecked.")]
    public bool joyStickMode;
    [Tooltip("Rotation speed of the character.")]
    public float rotationSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        charCntrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal and vertical movements
        float horComp = Input.GetAxis("Horizontal");
        float vertComp = Input.GetAxis("Vertical");

        if (joyStickMode)
        {
            horComp = Input.GetAxis("Vertical");
            vertComp = Input.GetAxis("Horizontal") * -1;
        }

        Vector3 moveVect = Vector3.zero;

        // Get look direction
        Vector3 cameraLook = cameraObj.transform.forward;
        cameraLook.y = 0f;
        cameraLook = cameraLook.normalized;

        Vector3 forwardVect = cameraLook;
        Vector3 rightVect = Vector3.Cross(forwardVect, Vector3.up).normalized * -1;

        moveVect += rightVect * horComp;
        moveVect += forwardVect * vertComp;

        moveVect *= speed;

        // Rotate the character to face the same direction as the camera in the X and Z plane
        if (moveVect.magnitude > 0.1f || Input.GetAxis("Mouse X") != 0) // Check if moving or camera rotates
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(forwardVect.x, 0f, forwardVect.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        charCntrl.SimpleMove(moveVect);
    }
}
