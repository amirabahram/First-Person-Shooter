using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Weapon weapon; //Refrence
    private CharacterController controller;
    public float speed = 12f;
    Vector3 velocity = Vector3.zero;
    float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance=0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHieght = 1f;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
    
        MoveThePlayer();

    }
    void MoveThePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHieght);
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(x!=0 || z!=0)
        {
            if (!weapon.GetReloadingState())
            {
                weapon.GetAnim().Play("Armature|walk");
            }
        }
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move*speed*Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);
    }
}
