using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody motorRB = null;
    [SerializeField] private float forwardSpeed = 0f;
    [SerializeField] private float reverseSpeed = 0f;
    [SerializeField] private float turnSpeed = 0f;
    [SerializeField] private float airDrag;
    [SerializeField] private float groundDrag;
    [SerializeField] private LayerMask groundLayer;
    private float moveInput;
    private float turnInput;
    private bool isGrounded = false;

    private void Start() {
        motorRB.transform.parent = null;
    }

    private void Update() {
        //inputs
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        moveInput *= moveInput > 0 ? forwardSpeed : reverseSpeed;

        //Set up the car position to the motor position
        transform.position = motorRB.transform.position;

        //turning the car
        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical"); 
        transform.Rotate(0, newRotation, 0, Space.World);

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        
        if(isGrounded) {
            motorRB.drag = groundDrag;
        } else {
            motorRB.drag = airDrag;
        }
    }

    private void FixedUpdate() {
        if(isGrounded) {
            //move car
            motorRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        } else {
            motorRB.AddForce(transform.up * -38f);
        }
    }
}
