using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBowAttack : MonoBehaviour
{
    public EnemyOptiuns enemiOptions;
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
    }
    public void SpuwnArrow()
    {
        SpawningArrow = Instantiate(Arrow, SpawnArrow.position, SpawnArrow.rotation, transform);
    }
    public void Fiere()
    {
        SpawningArrow.GetComponent<Arrow>().FireArrow();
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
