using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemiMuwment : MonoBehaviour
{
    private float _targetRotation = 0.0f;
    public GameObject Head;
    private float Angle;

    private CharacterController controller;
    public EnemyOptiuns enemiOptions;
    public float _rotationVelocity;

    private float gravity = -9.81f;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        GetDirectionAndMove(); 
        SeaAnimation();
    }
    private void GetDirectionAndMove()
    {
        Vector3 inputDirection = enemiOptions.Player.transform.position - transform.position;

        _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
            enemiOptions.RotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        Vector3 targetDirection = Quaternion.Euler(0.0f, rotation, 0.0f) * Vector3.forward;

        controller.Move(targetDirection.normalized * enemiOptions.MoveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        Angle = Vector3.Angle(transform.forward, inputDirection);
        if (Angle < 70)
            Head.transform.LookAt(enemiOptions.Player);
        else
            Head.transform.rotation = Quaternion.Euler(0f, gameObject.transform.eulerAngles.y, 0f);
    }
    public void SeaAnimation()
    {
        if(enemiOptions.MoveSpeed > enemiOptions.crippleMoveSpeed)
        {
            enemiOptions.animatorLeg.SetBool("isMow", true);
        }
        else
        {
            enemiOptions.animatorLeg.SetBool("isMow", false);
            enemiOptions.animatorLeg.SetBool("crippleMove", true);
        }
    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(enemiOptions.jumpHeight * -2f * gravity);
    }
}
