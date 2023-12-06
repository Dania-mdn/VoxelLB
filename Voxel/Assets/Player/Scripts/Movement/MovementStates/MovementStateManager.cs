using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    public RunningAnimation runningAnimation;
    public Transform Ass;
    public float currentMoveSpeed;
    public float walkSpeed =4, walkBackSpeed =2;
    public float runSpeed =8, runBackSpeed =5;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hzInput, vInput;
    CharacterController controller;
    #endregion

    #region GroundCheck
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    #endregion

    #region Gravity
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    #endregion

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;

    public int level = 1;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);

    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        //anim.SetFloat("hzInput", hzInput);
        //anim.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);

        //�������� ���� � ����������� �� ����������
        if(vInput == 0)
        {
            if (hzInput < 0)
            {
                Ass.rotation = Quaternion.Euler(0, -65, 0) * transform.rotation;
                runningAnimation.direction = 0;
            }
            else if (hzInput > 0)
            {
                Ass.rotation = Quaternion.Euler(0, 65, 0) * transform.rotation;
                runningAnimation.direction = 4;
            }
            else
            {
                Ass.rotation = Quaternion.Euler(0, 0, 0) * transform.rotation;
                runningAnimation.direction = 8;
            }
        }
        else if(vInput > 0)
        {
            if (hzInput < 0)
            {
                Ass.rotation = Quaternion.Euler(0, -40, 0) * transform.rotation;
                runningAnimation.direction = 1;
            }
            else if (hzInput > 0)
            {
                Ass.rotation = Quaternion.Euler(0, 40, 0) * transform.rotation;
                runningAnimation.direction = 3;
            }
            else
            {
                Ass.rotation = Quaternion.Euler(0, 0, 0) * transform.rotation;
                runningAnimation.direction = 2;
            }
        }
        else if(vInput < 0)
        {
            if (hzInput < 0)
            {
                Ass.rotation = Quaternion.Euler(0, 40, 0) * transform.rotation;
                runningAnimation.direction = 7;
            }
            else if (hzInput > 0)
            {
                Ass.rotation = Quaternion.Euler(0, -40, 0) * transform.rotation;
                runningAnimation.direction = 5;
            }
            else
            {
                Ass.rotation = Quaternion.Euler(0, 0, 0) * transform.rotation;
                runningAnimation.direction = 6;
            }
        }
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if(Physics.CheckSphere(spherePos, controller.radius -0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    public void IncreaseLevel()
    {
        level++;
    }

}
