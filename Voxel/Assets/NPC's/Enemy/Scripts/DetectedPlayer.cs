using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedPlayer : MonoBehaviour
{
    public EnemyOptiuns enemyOptiuns;
    public EnemySword enemySword;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enemyOptiuns.Player = other.gameObject.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enemyOptiuns.Player = null;
    }
    public void Cuttrue()
    {
        enemySword.cut = true;
    }
    public void Cutfalse()
    {
        enemySword.cut = false;
    }
}
