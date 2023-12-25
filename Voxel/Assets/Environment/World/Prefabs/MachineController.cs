using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineController : MonoBehaviour
{
    [SerializeField] float mouseSense = 1;
    [SerializeField] Transform BodyDirection;
    private float xAxis, yAxis;
    public CinemachineVirtualCamera CinemachineVirtualCamera;
    public Transform SpawnPlayer;

    public GameObject Arrow;
    public Transform SpawnArrow;
    private GameObject SpawningArrow;

    public GameObject False;
    public GameObject True;
    public GameObject ArrowTarget;
    public GameObject E;
    public float Firerate;
    private float TimeLate;

    private void Start()
    {
        SpuwnArrow();
    }
    void Update()
    {
        if(CinemachineVirtualCamera.Priority != 11)
        {
            CinemachineVirtualCamera.Priority = 11;
            ArrowTarget.SetActive(true);
        }

        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -20, 20); 

        if (Input.GetKeyDown(KeyCode.E))
        {
            this.enabled = false;
            CinemachineVirtualCamera.Priority = 1; 
            ArrowTarget.SetActive(false);
        }

        if(TimeLate > 0)
        {
            TimeLate = TimeLate - Time.deltaTime;
            return;
        }
        else
        {
            if (SpawningArrow == null)
            {
                SpuwnArrow();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && SpawningArrow != null)
        {
            FireArrow(); 
            TimeLate = Firerate;
        }
    }

    private void LateUpdate()
    {
        BodyDirection.localEulerAngles = new Vector3(yAxis, xAxis, BodyDirection.localEulerAngles.z);
    }
    public void SpuwnArrow()
    {
        SpawningArrow = Instantiate(Arrow, SpawnArrow.position, SpawnArrow.rotation, SpawnArrow);
        False.SetActive(false);
        True.SetActive(true);
    }
    public void FireArrow()
    {
        if (SpawningArrow != null)
        {
            SpawningArrow.GetComponent<Arrow>().FireArrow();
            SpawningArrow = null;
            False.SetActive(true);
            True.SetActive(false);
        }
    }
}
