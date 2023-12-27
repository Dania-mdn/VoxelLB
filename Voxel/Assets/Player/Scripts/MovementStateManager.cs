using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public PlayerOptions PlayerOptions;

    #region Movement
    public RunningAnimation runningAnimation;
    public Transform Ass;
    public float currentMoveSpeed;

    [HideInInspector] public Vector3 dir;
    private Vector3 dirr;
    [HideInInspector] public float hzInput, vInput;
    CharacterController controller;
    #endregion

    #region Gravity
    private float gravity = -9.81f;
    Vector3 velocity;
    #endregion

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetDirectionAndMove();

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // —брасываем вертикальную скорость, когда персонаж находитс€ на земле
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            Jump();
        }
        // ѕримен€ем гравитацию
        velocity.y += gravity * Time.deltaTime;

    }


    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!EventSystem.readyTeleport)
            {
                EventSystem.DoEmptiMana();
            }
            else
            {
                currentMoveSpeed = PlayerOptions.teleportArrou;
                EventSystem.DoAttack(PlayerOptions.ManaForTeleport);
            }
        }
        else
        {
            currentMoveSpeed = PlayerOptions.walkSpeed;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && controller.isGrounded)
        {
            currentMoveSpeed = 0f;
            runningAnimation.direction = 8;
        }

        if (controller.isGrounded)
            dirr = dir;

        controller.Move(dirr.normalized * currentMoveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        //разворот таза в зависимости от направлни€
        if (vInput == 0)
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
    void Jump()
    {
        // –ассчитываем вертикальную скорость дл€ прыжка
        velocity.y = Mathf.Sqrt(PlayerOptions.jumpHeight * -2f * gravity);
    }
}
