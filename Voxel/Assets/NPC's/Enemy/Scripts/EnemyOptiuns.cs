using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyOptiuns : MonoBehaviour
{
    public Transform Target;
    public Transform Player;
    public Transform ForwardTarget;
    public Transform LeftTarget;
    public Transform RightTarget;
    public Transform positionWolck;

    public Animator animatorBody;
    public Animator animatorLeg;
    public CharacterController controller;
    private EnemiMuwment EnemiMuwment;
    private Transform[] child;

    public float jumpHeight;
    public float MoveSpeed;
    public float healthyMoveSpeed;
    public float crippleMoveSpeed;
    public float RotationSmoothTime = 0.12f;

    private float mediateMowecpeed;

    public Transform Body;
    public Transform LegL;
    public Transform LegR;

    public bool isStop;

    public float coldawn = 1;
    public float Damage = 1;

    private void Start()
    {
        MoveSpeed = healthyMoveSpeed;

        child = gameObject.GetComponentsInChildren<Transform>(true);
        controller = GetComponent<CharacterController>();
    }

    public void TakeHit()
    {
        animatorBody.Rebind();
        animatorLeg.Rebind();

        if (cripl(Body))
        {
            Deat();
        }
        else if(cripl(LegL) && cripl(LegR))
        {
            MoveSpeed = 0;
            controller.height = 0;
        }
        else if(cripl(LegL) || cripl(LegR))
        {
            MoveSpeed = crippleMoveSpeed;
        }

        if(EnemiMuwment != null)
            EnemiMuwment.SeaAnimation();
    }
    private bool cripl(Transform leg)
    {
        while (leg != null)
        {
            leg = leg.parent;

            if (leg == transform)
            {
                return false;
            }
        }
        return true;
    }
    public void SetPausaMow(bool isPause)
    {
        if(MoveSpeed != 0.03f)
            mediateMowecpeed = MoveSpeed;

        if (isPause)
        {
            MoveSpeed = 0.03f;
        }
        else
        {
            MoveSpeed = mediateMowecpeed;
        }
    }
    private void Deat()
    {
        Rigidbody[] childRigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in childRigidbodies)
        {
            rb.isKinematic = false;
            rb.gameObject.transform.parent = null;
        }

        animatorBody.enabled = false;
        animatorLeg.enabled = false;

        Invoke("InvokeDeat", 3);
    }
    private void InvokeDeat()
    {
        foreach (Transform go in child)
        {
            Destroy(go.gameObject);
        }
        Destroy(gameObject);
    }
}
