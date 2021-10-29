using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirsdPersonMovementScript : MonoBehaviour
{

    //criacao de variaveis

    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;


    //animação

    public Animator animator;
    int moveXAnimatorParameterId;
    int moveZAnimatorParameterId;
    void Update()
    {
        animator = GetComponent<Animator>();
        moveXAnimatorParameterId = Animator.StringToHash("MoveX");
        moveZAnimatorParameterId = Animator.StringToHash("MoveZ");

        float horizontal = Input.GetAxisRaw("Horizontal"); //input da tecla horizontal
        if(horizontal != 0f);
        {
            animator.SetFloat(moveXAnimatorParameterId, horizontal);
        }
        float vertical = Input.GetAxisRaw("Vertical"); //input da tecla vertical
        if(vertical != 0f);
        {
            animator.SetFloat(moveZAnimatorParameterId, vertical);
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //mivimentacao

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
