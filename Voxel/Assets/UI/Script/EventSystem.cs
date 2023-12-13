using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static event Action<float> Attack;
    public static bool readyAction = true;
    public static event Action EndGame;
    
    public static void DoAttack(float DeltaMana)
    {
        Attack?.Invoke(DeltaMana);
    }
    public static void SetReadyAction(bool ReadyAction)
    {
        readyAction = ReadyAction;
    }
    public static void DoEndGame()
    {
        EndGame?.Invoke();
    }
}
