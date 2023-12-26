using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBowAttack : MonoBehaviour
{
    public EnemyOptiuns enemiOptions;
    public GameObject BodyGroup;
    public GameObject Arrow;
    public Transform SpawnArrow;
    private GameObject SpawningArrow;

    private float time;
    private void Update()
    {
        if (enemiOptions.Player == null) return;

        if(time >= 0)
        {
            time = time - Time.deltaTime;
        }
        else
        {
            time = enemiOptions.Firerate;
            enemiOptions.animatorBody.Play("BowAtack");
        }

        if (enemiOptions.Angle < 60 && enemiOptions.Target == enemiOptions.Player)
            BodyGroup.transform.LookAt(enemiOptions.Player);
        else
            BodyGroup.transform.rotation = Quaternion.Euler(0f, gameObject.transform.eulerAngles.y, 0f);
    }
    public void SpuwnArrow()
    {
        SpawningArrow = Instantiate(Arrow, SpawnArrow.position, SpawnArrow.rotation, SpawnArrow);
    }
    public void Fiere()
    {
        SpawningArrow.GetComponent<ArrowEnemy>().FireArrow();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enemiOptions.Player = other.gameObject.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enemiOptions.Player = null;
    }
}
