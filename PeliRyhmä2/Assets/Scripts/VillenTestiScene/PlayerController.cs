using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed;

    public float jumpForce;
    public float gravity;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Animator animator;
    public Transform model; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;
        animator.SetFloat("speed", Mathf.Abs(hInput));

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        direction.y += gravity * Time.deltaTime;
        if (isGrounded)
        {
            
            if (Input.GetButton("Horizontal") && Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
            }
        }
        if(hInput != 0)
        {
            Quaternion newrRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
            model.rotation = newrRotation;
        }

        controller.Move(direction * Time.deltaTime);

    }
}
