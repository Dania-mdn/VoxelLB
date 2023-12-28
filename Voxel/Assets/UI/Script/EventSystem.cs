using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static event Action<float> AttackHealth;
    public static event Action<float> Attack;
    public static event Action<bool> Night;
    public static bool readySword = true;
    public static bool readyBow = true;
    public static bool readyHammer = true;
    public static bool readyTeleport = true;
    public static event Action EmptiMana;
    public static event Action EndGame;
    
    public static void DoAttackHealth(float DeltaHealth)
    {
        AttackHealth?.Invoke(DeltaHealth);
    }
    public static void DoAttack(float DeltaMana)
    {
        Attack?.Invoke(DeltaMana);
    }
    public static void DoEndGame()
    {
        EndGame?.Invoke();
    }
    public static void DoEmptiMana()
    {
        EmptiMana?.Invoke();
    }
    public static void DoNight(bool isNight)
    {
        Night?.Invoke(isNight);
    }
    public static void SetReadySword(bool ReadyAction)
    {
        readySword = ReadyAction;
    }
    public static void SetReadyBow(bool ReadyAction)
    {
        readyBow = ReadyAction;
    }
    public static void SetReadyHammer(bool ReadyAction)
    {
        readyHammer = ReadyAction;
    }
    public static void SetReadyTeleport(bool ReadyAction)
    {
        readyTeleport = ReadyAction;
    }
}
