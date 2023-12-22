using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemiMuwment : MonoBehaviour
{
    private float _targetRotation = 0.0f;
    public GameObject Head;
    private float Angle;
    private float distance;

    private CharacterController controller;
    public EnemyOptiuns enemiOptions;
    public float _rotationVelocity;

    private float gravity = -9.81f;
    Vector3 velocity;


    public Transform[] Ray;
    private RaycastHit hit;
    private bool forward;
    private bool left;
    private bool right;
    private bool both;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (!enemiOptions.isStop)
            AvoidingObstances();

        distance = Vector3.Distance(transform.position, enemiOptions.Target.position);

        if (distance < 1.4f)
        {
            enemiOptions.isStop = true;
            enemiOptions.SetPausaMow(true);
        }
        else
        {
            if (!enemiOptions.isStop)
                enemiOptions.SetPausaMow(false);
        }

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        GetDirectionAndMove();
        SeaAnimation();

    }
    private void AvoidingObstances()
    {
        forward = false;
        left = false;
        right = false;
        both = false;

        for (int i = 0; i < 4;  i++)
        {
            if (Physics.Raycast(Ray[i].transform.position, Ray[i].forward, out hit, 2))
            {
                if (hit.transform != null && hit.transform.tag != "Player")
                {
                    switch (i)
                    {
                        case 0:
                            forward = true;
                            break;
                        case 1:
                            left = true;
                            break;
                        case 2:
                            right = true;
                            break;
                        case 3:
                            both = true;
                            break;
                    }
                }
            }
        }
        if (forward && left == false && right == false || (forward && left && right))
        {
            int i = Random.Range(0, 2);
            if(i == 0)
                enemiOptions.Target = enemiOptions.LeftTarget;
            else
                enemiOptions.Target = enemiOptions.RightTarget;

            enemiOptions.SetPausaMow(true);
        }
        else if (forward && left && right == false)
        {
            enemiOptions.Target = enemiOptions.RightTarget;
            enemiOptions.SetPausaMow(true);
        }
        else if (forward && left == false && right)
        {
            enemiOptions.Target = enemiOptions.LeftTarget;
            enemiOptions.SetPausaMow(true);
        }
        else if(forward == false && (left == true || right == true))
        {
            enemiOptions.Target = enemiOptions.ForwardTarget;
            enemiOptions.SetPausaMow(false);
        }
        else if (forward == false && controller.isGrounded && both && enemiOptions.MoveSpeed > enemiOptions.crippleMoveSpeed)
        {
            Jump();

            if (enemiOptions.Player != null)
                enemiOptions.Target = enemiOptions.Player;
            else
                enemiOptions.Target = enemiOptions.positionWolck;
        }
        else
        {
            if (enemiOptions.Player != null)
                enemiOptions.Target = enemiOptions.Player;
            else
                enemiOptions.Target = enemiOptions.positionWolck;

            enemiOptions.SetPausaMow(false);
        }
    }
    private void GetDirectionAndMove()
    {
        Vector3 inputDirection = enemiOptions.Target.transform.position - transform.position;

        _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
            enemiOptions.RotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        Vector3 targetDirection = Quaternion.Euler(0.0f, rotation, 0.0f) * Vector3.forward;

        controller.Move(targetDirection.normalized * enemiOptions.MoveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        Angle = Vector3.Angle(transform.forward, inputDirection);

        if (Angle < 70 && enemiOptions.Target == enemiOptions.Player)
            Head.transform.LookAt(enemiOptions.Player);
        else
            Head.transform.rotation = Quaternion.Euler(0f, gameObject.transform.eulerAngles.y, 0f);
    }
    
    public void SeaAnimation()
    {
        if(enemiOptions.MoveSpeed > enemiOptions.crippleMoveSpeed)
        {
            enemiOptions.animatorLeg.SetBool("isMow", true);
            enemiOptions.animatorLeg.SetBool("crippleMove", false);
        }
        else if(enemiOptions.MoveSpeed == enemiOptions.crippleMoveSpeed)
        {
            enemiOptions.animatorLeg.SetBool("isMow", false);
            enemiOptions.animatorLeg.SetBool("crippleMove", true);
        }
        else
        {
            enemiOptions.animatorLeg.SetBool("isMow", false);
            enemiOptions.animatorLeg.SetBool("crippleMove", false);
        }
    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(enemiOptions.jumpHeight * -2f * gravity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(Ray[0].transform.position, Ray[0].forward * 1.5f);
        Gizmos.DrawRay(Ray[1].transform.position, Ray[1].forward * 1.5f);
        Gizmos.DrawRay(Ray[2].transform.position, Ray[2].forward * 1.5f);
        Gizmos.DrawRay(Ray[3].transform.position, Ray[3].forward * 1.5f);
    }
}
