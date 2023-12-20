using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isFight;

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Player = other.gameObject.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Player = null;
    }
}
