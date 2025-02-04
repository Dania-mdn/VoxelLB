using UnityEngine;
using UnityEngine.UI;

public class WeaponHendler : MonoBehaviour
{
    public PlayerOptions PlayerOptions;

    public Sword cut;
    public Hummer hummer;

    private Animator animator;

    public GameObject SwordHand;
    public GameObject BowHand;
    public GameObject HammerHand;
    public GameObject Swordback;
    public GameObject Bowback;
    public GameObject Hammerback;

    public GameObject BowrLeftHand;
    private int NumberWeapon = 1;
    public GameObject[] UIWeapon;

    public GameObject Arrow;
    public Transform SpawnArrow;
    private GameObject SpawningArrow;

    public GameObject ArrowTarget;

    private float currentMousePosition;
    private float lastMousePosition;
    private float mouseDelta;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if(NumberWeapon == 1)
        {
            SwordHand.SetActive(true);
            BowHand.SetActive(false);
            HammerHand.SetActive(false);

            Swordback.SetActive(false);
            Bowback.SetActive(true);
            Hammerback.SetActive(true);
        }

        SetWeaponUI(NumberWeapon);
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "idle" && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "HoldBow") return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (NumberWeapon == 2 || NumberWeapon == 3)
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
            if (NumberWeapon == 1 || NumberWeapon == 3)
            {
                animator.Play("chanche");
                NumberWeapon = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (NumberWeapon == 1 || NumberWeapon == 2)
            {
                if (BowrLeftHand.activeSelf == true)
                {
                    BowHand.SetActive(true);
                    BowrLeftHand.SetActive(false);
                }
                animator.Play("chanche");
                NumberWeapon = 3;
            }
        }

        if (NumberWeapon == 1 || NumberWeapon == 3)
        {
            currentMousePosition = Input.GetAxisRaw("Mouse X");
            mouseDelta = (currentMousePosition - lastMousePosition) * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Mouse0) && mouseDelta == 0)
            {
                if (!EventSystem.readySword) 
                {
                    EventSystem.DoEmptiMana();
                }
                else
                {
                    cut.isCut = true;
                    animator.Play("attack");
                    if (NumberWeapon == 1) EventSystem.DoAttack(PlayerOptions.ManaForSword);
                    else if (NumberWeapon == 3) EventSystem.DoAttack(PlayerOptions.ManaForHammer);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && mouseDelta > 0)
            {
                if (!EventSystem.readySword)
                {
                    EventSystem.DoEmptiMana();
                }
                else
                {
                    cut.isCut = true;
                    animator.Play("left");
                    if (NumberWeapon == 1) EventSystem.DoAttack(PlayerOptions.ManaForSword);
                    else if (NumberWeapon == 3) EventSystem.DoAttack(PlayerOptions.ManaForHammer);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && mouseDelta < 0)
            {
                if (!EventSystem.readySword)
                {
                    EventSystem.DoEmptiMana();
                }
                else
                {
                    cut.isCut = true;
                    animator.Play("right");
                    if (NumberWeapon == 1) EventSystem.DoAttack(PlayerOptions.ManaForSword);
                    else if (NumberWeapon == 3) EventSystem.DoAttack(PlayerOptions.ManaForHammer);
                }
            }

            lastMousePosition = currentMousePosition;
        }
        else if(NumberWeapon == 2)
        {
            if (!EventSystem.readyBow) return;

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
    }
    public void SetCutfalse()
    {
        cut.isCut = false; 
        hummer.isBAAAM = false;
    }
    public void SetCuttrue()
    {
        cut.isCut = true;
        hummer.isBAAAM = true;
    }
    public void SetChanche()
    {
        if (NumberWeapon == 1)
        {
            SwordHand.SetActive(true);
            BowHand.SetActive(false);
            HammerHand.SetActive(false);

            Swordback.SetActive(false);
            Bowback.SetActive(true);
            Hammerback.SetActive(true);
        }
        else if(NumberWeapon == 2)
        {
            SwordHand.SetActive(false);
            BowHand.SetActive(true);
            HammerHand.SetActive(false);

            Swordback.SetActive(true);
            Bowback.SetActive(false);
            Hammerback.SetActive(true);
        }
        else if(NumberWeapon == 3)
        {
            SwordHand.SetActive(false);
            BowHand.SetActive(false);
            HammerHand.SetActive(true);

            Swordback.SetActive(true);
            Bowback.SetActive(true);
            Hammerback.SetActive(false);
        }

        SetWeaponUI(NumberWeapon);
    }
    private void SetWeaponUI(int Weapon)
    {
        for(int i = 0; i < UIWeapon.Length; i++)
        {
            if (i != Weapon)
            {
                UIWeapon[i].transform.localScale = Vector3.one;
                UIWeapon[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
            }
            else
            {
                UIWeapon[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                UIWeapon[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }
    public void SetArmored()
    {
        animator.Play("Armored");
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
        SpawningArrow = Instantiate(Arrow, SpawnArrow.position, SpawnArrow.rotation, SpawnArrow.transform);
        ArrowTarget.SetActive(true);
    }
    public void FireArrow()
    {
        if(SpawningArrow != null)
        {
            SpawningArrow.GetComponent<Arrow>().FireArrow();
            SpawningArrow = null; 
            EventSystem.DoAttack(PlayerOptions.ManaForBow);
        }
        ArrowTarget.SetActive(false);
    }
}
