using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static event Action<float> Attack;
    public static event Action EndGame;
    public static void DoEndGame()
    {
        EndGame?.Invoke();
    }public static void DoAttack(float DeltaMana)
    {
        Attack?.Invoke(DeltaMana);
    }
}
