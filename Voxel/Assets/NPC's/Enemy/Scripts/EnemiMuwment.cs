using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemiMuwment : MonoBehaviour
{
    public Transform Player;
    private float _targetRotation = 0.0f;

    CharacterController controller;
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
    }
    private void GetDirectionAndMove()
    {
        Vector3 inputDirection = Player.transform.position - transform.position;

        _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
            enemiOptions.RotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        Vector3 targetDirection = Quaternion.Euler(0.0f, rotation, 0.0f) * Vector3.forward;

        controller.Move(targetDirection.normalized * enemiOptions.MoveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(enemiOptions.jumpHeight * -2f * gravity);
    }
}
