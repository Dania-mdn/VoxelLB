using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHendler : MonoBehaviour
{
    public Cut cut;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
        {
            Debug.Log("attack");
            cut.cut = true;
            animator.Play("attack");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.A) == true)
        {
            Debug.Log("left");
            cut.cut = true;
            //animator.Play("left");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.D) == true)
        {
            Debug.Log("right");
            cut.cut = true;
            //animator.Play("right");
        }
    }
    public void SetCut()
    {
        cut.cut = false;
    }
}
