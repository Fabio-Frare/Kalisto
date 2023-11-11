using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentHero : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    public Rigidbody rb;

    public float speed = 3f;
    public float gravity = 10f;
    public float rotSpeed = 180f;
    private float rot;
    private float drag = 10f;
    
    private Vector3 moveDirection;


    void Start()
    {
       controller = GetComponent<CharacterController>(); 
       anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        //handleDrag();
    }

    void Move()
    {
        // se o personagem está no chão...
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {   
                moveDirection = Vector3.forward * speed;
                anim.SetInteger("transition", 1);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                moveDirection = Vector3.zero;
                anim.SetInteger("transition", 0);
            }

        } //else
       // {
            // método para pular
       // }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
       
        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);

        controller.Move(moveDirection * Time.deltaTime);
    }

    void handleDrag()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0 , rb.velocity.z) / (1 + drag / 100) + new Vector3(0, rb.velocity.y, 0);
    }

}
