using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    public InventoryObject Inventory;
    public CharacterController characterController;
    public Animator animator;
    public Transform cam;

    public float speed = 1f;
    public float gravity = -20f;
    public float jumpSpeed = 15;

    public float TurnSmothTime = 0.1f;
    private float turnSmothVelocity;

    Vector3 moveVelocity;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (characterController.isGrounded) 
        {
            animator.SetBool("IsGrounded", false);
            moveVelocity = transform.forward * speed * Time.deltaTime;
            if (Input.GetButtonDown("Jump")) 
            {
                animator.SetBool("IsGrounded", true);
                moveVelocity.y = jumpSpeed;
            }
        }
        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        if (direction.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk(direction);
        }
        else if (direction.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift)) 
        {
            Run(direction);
        }
        else if(direction.magnitude == 0)
        {
            Idle();
        }
    }

    private void Idle() 
    {

        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);

    }
    private void Walk(Vector3 direction)
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", true);
        var moveDir = CalculateNumbers(direction);
        characterController.Move(moveDir.normalized * speed * Time.deltaTime);
    }
    private void Run(Vector3 direction)
    {
        animator.SetBool("Run", true);
        animator.SetBool("Walk", false);
        var moveDir = CalculateNumbers(direction);
        characterController.Move(moveDir.normalized * (speed + 2f) * Time.deltaTime);
    }
    private Vector3 CalculateNumbers(Vector3 direction)
    {
        float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmothVelocity, TurnSmothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
        return moveDir;
    }
    //public void Save()
    //{
    //    Inventory.Save();
    //}
    //public void Load()
    //{
    //    Inventory.Load();
    //}
    private void OnApplicationQuit()
    {
        Inventory.Container.Items = new InventorySlot[40];
    }
}