using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
This code is adapted from code provided by the instructor.
*/

public class player : MonoBehaviour
{
    public float speed = 10;
    public Camera myCamera;
    public float mouseDeltaMultiplier;
    private float myCamXRot;
    private float myCamYRot;

    // The ball that will be used to start the machine
    public GameObject startingBall;

    // The game object of the UI text to disable once the machine starts
    public GameObject uiText;

    private Rigidbody rb;

    private float movementX; // left/right arrow or A/D
    private float movementY; // up/down arrow or W/S
    private float rotationX; // mouse X movement (left or right)
    private float rotationY; // mouse Y movement (front or back)
    private float heightMovement; // range between -1 (down) to 1 (up)
    private float rollAmount; // range between -1 (left) to 1 (right)


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // hide cursor when playing the game

        myCamXRot = 0;
        myCamYRot = 0;
    }

    // On<Action> functions are defined in the InputActions Asset

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnLook(InputValue lookValue)
    {
        Vector2 lookVector = lookValue.Get<Vector2>();
        myCamXRot -= lookVector.y * mouseDeltaMultiplier;
        myCamYRot += lookVector.x * mouseDeltaMultiplier;

        myCamXRot = Mathf.Clamp(myCamXRot, -90f, 90f); // Prevent looking past straight up and straight down
    }

    private void OnMoveUpDown(InputValue heightValue)
    {
        Vector2 heightVector = heightValue.Get<Vector2>();
        // Debug.Log(heightVector);
        heightMovement = heightVector.y;
    }

    private void OnRoll(InputValue rollValue)
    {
        Vector2 rollVector = rollValue.Get<Vector2>();
        // Debug.Log(rollVector);
        rollAmount = rollVector.x;
    }

    private void OnFire(InputValue fireValue)
    {
        uiText.SetActive(false);
        startingBall.GetComponent<Rigidbody>().useGravity = true;
    }

    private void FixedUpdate()
    {        
        // Update camera
        myCamera.transform.rotation = Quaternion.Euler(myCamXRot, myCamYRot, 0);

        // Update movement
        Vector3 movement = new Vector3(movementX, heightMovement, movementY);
        Quaternion movRotationQuat = Quaternion.Euler(0, myCamYRot, 0); // Camera's rotation about y axis
        rb.AddForce(movRotationQuat * (movement * speed)); // World space
    }
}
