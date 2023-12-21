using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedPlayer : MonoBehaviour
{
    public EnemyOptiuns enemyOptiuns;

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
}
