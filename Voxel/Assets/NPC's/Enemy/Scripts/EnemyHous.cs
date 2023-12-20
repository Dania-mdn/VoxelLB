using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHous : MonoBehaviour
{
    public Transform[] positionWolck;
    private int positionCount;

    public GameObject Enemy;

    private float time;
    public float coldawn = 5;

    private void Start()
    {
        positionCount = 0;
        Enemy.GetComponent<EnemyOptiuns>().positionWolck = positionWolck[positionCount];
    }
    private void Update()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }
        else
        {
            time = coldawn;
            Enemy.GetComponent<EnemyOptiuns>().positionWolck = positionWolck[positionCount];

            if (positionCount < positionWolck.Length - 1)
                positionCount++;
            else
                positionCount = 0;
        }
    }
}
