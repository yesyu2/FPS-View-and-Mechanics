using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    private CharacterController controller;
    public float speed = 1f;
    private float xRotation = 0f;
    public float mouseSensitivity = 100f;

    //Jump & Gravity
    private Vector3 velocity;
    private bool isGround;
    private float gravity = -9.81f;
    public Transform groundChecker;
    public float groundCheckerRadius;
    public LayerMask obstacleLayer;
    public float gravityDivide = 100f;
    public float jumpHeight= 0.1f;
    public float jumpSpeed = 30;
    private float aTimer;


    //Flashlight
    public GameObject flashlight;

    private bool is_on;
    

    private void Awake() 
    {
         is_on = true;

         controller = GetComponent<CharacterController>();

        //Cursor
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;


         
    }
    
    private void Update() 
    {

        //Check character is grounded
        isGround = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, obstacleLayer);

        //Movement
        Vector3 moveInputs = Input.GetAxis("Horizontal")* transform.right + Input.GetAxis("Vertical")* transform.forward;
        Vector3 moveVelocity = moveInputs * Time.deltaTime * speed;
        controller.Move(moveVelocity);

        //CameraController
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Jump & Gravity
        

        if(!isGround)
        {
            velocity.y += gravity * Time.deltaTime / gravityDivide;
            aTimer += Time.deltaTime /3;
            speed = Mathf.Lerp(10, jumpSpeed, aTimer);
        }
        else
        {
            velocity.y = -0.05f;
            speed = 10;
            aTimer = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity / gravityDivide);
        }
        controller.Move(velocity);

        if(Input.GetKey(KeyCode.F))
        {
            if(is_on)
            {
                 flashlight.GetComponent<Light>().enabled = false;
                 is_on= false;
            }
           
        }
        else
        {
            flashlight.GetComponent<Light>().enabled = true;
            is_on = true;
        }
    }

}
