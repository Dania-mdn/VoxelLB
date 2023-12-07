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
        PlayerPrefs.SetInt("NumberWeapon", 1);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("NumberWeapon") == 1)
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
        else if(PlayerPrefs.GetInt("NumberWeapon") == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.Play("BowAttack");
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                animator.SetBool("BowFire", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(PlayerPrefs.GetInt("NumberWeapon") == 2)
            {
                if (BowrLeftHand.activeSelf == true)
                {
                    BowHand.SetActive(true);
                    BowrLeftHand.SetActive(false);
                }
                animator.Play("chanche");
                PlayerPrefs.SetInt("NumberWeapon", 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerPrefs.GetInt("NumberWeapon") == 1)
            {
                animator.Play("chanche");
                PlayerPrefs.SetInt("NumberWeapon", 2);
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

    public void SetBowReadyFire()
    {
        animator.SetBool("BowFire", true);
    }
    public void SetchancheHendBow()
    {
        if(BowHand.activeSelf == true)
        {
            BowHand.SetActive(false);
            BowrLeftHand.SetActive(true);
        }
    }
}
