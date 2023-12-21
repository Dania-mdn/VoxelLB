using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHous : MonoBehaviour
{
    public Transform[] positionWolck;
    private int positionCount;
    private int positionCountMediate = 0;

    public EnemyOptiuns[] Enemy;

    private float time;
    public float coldawn = 5;

    private void Start()
    {
        positionCount = 0;

        for(int i = 0; i < Enemy.Length; i++)
        {
            SetPositionEnemy(Enemy[i], positionWolck[positionCount]); 

            if (positionCount < positionWolck.Length - 1)
                positionCount++;
            else
                positionCount = 0;
        }

        time = coldawn;
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

            if (positionCountMediate < positionWolck.Length - 1)
                positionCountMediate++;
            else
                positionCountMediate = 0;

            positionCount = positionCountMediate;

            for (int i = 0; i < Enemy.Length; i++)
            {
                SetPositionEnemy(Enemy[i], positionWolck[positionCount]);

                if (positionCount < positionWolck.Length - 1)
                    positionCount++;
                else
                    positionCount = 0;
            }
        }
    }
    private void SetPositionEnemy(EnemyOptiuns EnemyOptiuns, Transform position)
    {
        EnemyOptiuns.positionWolck = position;
    }
}
