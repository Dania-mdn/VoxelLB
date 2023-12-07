using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHendler : MonoBehaviour
{
    public Cut cut;

    private Animator animator;

    public GameObject SwordHand;
    public GameObject BowHand;
    public GameObject Swordback;
    public GameObject Bowback;

    public GameObject BowrLeftHand;
    private bool SwordReady = true;

    private int NumberWeapon = 1;

    public GameObject Arrow;
    public Transform SpawnArrow;
    private GameObject SpawningArrow;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if(SwordReady)
        {
            SwordHand.SetActive(true);
            BowrLeftHand.SetActive(false);

            Swordback.SetActive(false);
            Bowback.SetActive(true);
        }
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "idle" && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "HoldBow") return;

        if (NumberWeapon == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
            {
                cut.cut = true;
                animator.Play("attack");
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.A) == true)
            {
                cut.cut = true;
                animator.Play("left");
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.D) == true)
            {
                cut.cut = true;
                animator.Play("right");
            }
        }
        else if(NumberWeapon == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.Play("BowAttack");
                animator.SetBool("BowFire", true);
            }
            else if (Input.GetKey(KeyCode.Mouse0) == false)
            {
                animator.SetBool("BowFire", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(NumberWeapon == 2)
            {
                if (BowrLeftHand.activeSelf == true)
                {
                    BowHand.SetActive(true);
                    BowrLeftHand.SetActive(false);
                }
                animator.Play("chanche");
                NumberWeapon = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (NumberWeapon == 1)
            {
                animator.Play("chanche");
                NumberWeapon = 2;
            }
        }
    }
    public void SetCutfalse()
    {
        cut.cut = false;
    }
    public void SetCuttrue()
    {
        cut.cut = true;
    }
    public void SetChanche()
    {
        if (SwordReady)
        {
            SwordHand.SetActive(false);
            BowHand.SetActive(true);

            Swordback.SetActive(true);
            Bowback.SetActive(false);
        }
        else
        {
            SwordHand.SetActive(true);
            BowHand.SetActive(false);

            Swordback.SetActive(false);
            Bowback.SetActive(true);
        }
        SwordReady = !SwordReady;
    }

    public void SetchancheHendBow()
    {
        if(BowHand.activeSelf == true)
        {
            BowHand.SetActive(false);
            BowrLeftHand.SetActive(true);
        }
    }
    
    public void SpuwnArrow()
    {
        SpawningArrow = Instantiate(Arrow, SpawnArrow.position, SpawnArrow.rotation, BowrLeftHand.transform);
    }
    public void FireArrow()
    {
        SpawningArrow.GetComponent<Arrow>().FireArrow();
        SpawningArrow = null;
    }
}
