using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinotawrHeadAttack : MonoBehaviour
{
    public EnemySword enemySword;

    public EnemyOptiuns enemiOptions;
    public GameObject Slider;
    public Slider SliderAtack;

    public Transform Ray0;
    private RaycastHit hit; 
    
    public float rangeAttack;
    public bool isTaranReady = false;
    public bool isTaran = false;
    public float time = 0;
    private float coldawn = 0.4f;
    public float time1 = 0;
    private float coldawn1 = 3;

    private void Start()
    {
        Slider.SetActive(false);
    }
    private void Update()
    {
        if(time1 > 0)
        {
            time1 = time1 - Time.deltaTime;
        }
        else
        {
            if (!isTaranReady)
            {
                if (Physics.Raycast(Ray0.position, Ray0.forward, out hit, rangeAttack))
                {
                    if (hit.transform.tag == "Player")
                    {
                        isTaranReady = true;
                        enemiOptions.animatorBody.Play("Taran");
                        enemiOptions.MoveSpeed = 0.3f;
                        enemiOptions.RotationSmoothTime = 0.3f;
                        enemiOptions.isTaran = true; 
                        enemySword.cut = true;
                        Slider.SetActive(true);
                        StartCoroutine(Relocation());
                    }
                }
            }

            if (isTaran)
            {
                if (time > 0)
                {
                    time = time - Time.deltaTime;
                    enemiOptions.MoveSpeed = 30;
                }
                else
                {
                    isTaranReady = false;
                    isTaran = false;
                    enemiOptions.isTaran = false;
                    time1 = coldawn1;
                    enemiOptions.MoveSpeed = 4;
                    enemiOptions.RotationSmoothTime = 0.5f;
                    Slider.SetActive(false);
                }
            }
        }
    }
    IEnumerator Relocation()
    {
        float slider = 0;

        while (slider < 2)
        {
            slider += Time.deltaTime;
            SliderAtack.value = slider;
            yield return null;
        }

        time = coldawn;
        isTaran = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(Ray0.transform.position, Ray0.forward * 20);
    }
}
