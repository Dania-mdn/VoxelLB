using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOptiuns : MonoBehaviour
{
    public Transform Player;

    public Animator animatorBody;
    public Animator animatorLeg;
    private CharacterController controller;
    private EnemiMuwment EnemiMuwment;
    private Transform[] child;

    public float jumpHeight;
    public float MoveSpeed;
    public float healthyMoveSpeed;
    public float crippleMoveSpeed;
    public float RotationSmoothTime = 0.12f;

    private float mediateMowecpeed;

    public GameObject Body;
    public GameObject LegL;
    public GameObject LegR;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        MoveSpeed = healthyMoveSpeed;

        child = gameObject.GetComponentsInChildren<Transform>(true);
    }

    public void TakeHit()
    {
        animatorBody.Rebind();
        animatorLeg.Rebind();

        if(Body.transform.root.name != gameObject.name)
        {
            Deat();
        }
        else if(LegL.transform.root.name != gameObject.name && LegR.transform.root.name != gameObject.name)
        {
            MoveSpeed = 0;
            controller.height = 0;
        }
        else if(LegL.transform.root.name != gameObject.name || LegR.transform.root.name != gameObject.name)
        {
            MoveSpeed = crippleMoveSpeed;
        }

        if(EnemiMuwment != null)
        EnemiMuwment.SeaAnimation();
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
        }

        gameObject.transform.DetachChildren();
        Invoke("InvokeDeat", 3);
    }
    private void InvokeDeat()
    {
        foreach (Transform go in child)
        {
            Destroy(go.gameObject);
        }
    }
}
