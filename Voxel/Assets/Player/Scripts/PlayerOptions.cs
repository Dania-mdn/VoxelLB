using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptions : MonoBehaviour
{
    [Header("UI")]
    [Range(0, 10)]
    public float ManaForWeapon;
    [Range(0, 10)]
    public float MaxMana;
    [Range(0, 10)]
    public float ManaInSecond;
    [Range(0, 10)]
    public float HealthForWeapon;
    [Range(0, 10)]
    public float MaxHealth;
    [Range(0, 10)]
    public float HealthInSecond;

    [Header("Movement")]
    [Range(0, 10)]
    public float jumpHeight; 
    [Range(0, 10)]
    public float walkSpeed = 4;
    [Range(100, 300)]
    public float teleportArrou = 150;

    [Header("Components")]
    public AimStateManager AimStateManager;
    public MovementStateManager MovementStateManager;
    public WeaponHendler WeaponHendler;

    [Header("machine")]
    private MachineController machineController;
    private bool isMachine = false;
    public UIManager UIManager;
    public Animator AnimatorBody;
    public Animator AnimatorAss;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && machineController != null && isMachine == false)
        {
            machineController.enabled = true; 
            EnebaPlayerController(false);
            transform.parent = machineController.SpawnPlayer.transform;
            transform.position = machineController.SpawnPlayer.position;
            transform.rotation = machineController.SpawnPlayer.rotation;
            isMachine = true;
            machineController.E.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E) && isMachine == true)
        {
            AimStateManager.enabled = true;
            MovementStateManager.enabled = true;
            WeaponHendler.enabled = true;
            transform.parent = null;
            isMachine = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            machineController = other.gameObject.GetComponent<MachineController>();
            machineController.E.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            machineController.E.SetActive(false);
            machineController = null;
        }
    }
    public bool TakeHit(float Damage)
    {
        if (UIManager.SliderHealth.value < Damage)
        {

            Rigidbody[] childRigidbodies = transform.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in childRigidbodies)
            {
                rb.isKinematic = false;
                rb.gameObject.transform.parent = null;
            }
            transform.DetachChildren();
            Destroy(gameObject);
            AnimatorBody.enabled = false;
            AnimatorAss.enabled = false;
            Time.timeScale = 0.15f;
            EventSystem.DoEndGame();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void EnebaPlayerController(bool Enable)
    {
        AimStateManager.enabled = Enable;
        MovementStateManager.enabled = Enable;
        WeaponHendler.enabled = Enable;
    }
}
