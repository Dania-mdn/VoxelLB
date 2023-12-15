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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
