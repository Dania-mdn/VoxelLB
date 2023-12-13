using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptions : MonoBehaviour
{
    [Header("UI")]
    public float ManaForWeapon;
    public float MaxMana;
    public float ManaInSecond;

    [Header("Movement")]
    public float jumpHeight;
    public float walkSpeed = 4;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
