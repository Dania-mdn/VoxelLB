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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && machineController != null && isMachine == false)
        {
            machineController.enabled = true;
            AimStateManager.enabled = false;
            MovementStateManager.enabled = false;
            WeaponHendler.enabled = false;
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
        if (other.gameObject.tag == "Machine")
        {
            machineController = other.gameObject.GetComponent<MachineController>();
            machineController.E.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Machine")
        {
            machineController.E.SetActive(false);
            machineController = null;
        }
    }
}
