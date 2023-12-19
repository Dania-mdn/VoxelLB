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

    public GameObject ArrowTarget;

    private void Start()
    {
        SpuwnArrow();
    }
    void Update()
    {
        if(CinemachineVirtualCamera.Priority != 11)
            CinemachineVirtualCamera.Priority = 11;

        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -40, 50); 
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.enabled = false;
            CinemachineVirtualCamera.Priority = 1;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireArrow();
        }
    }

    private void LateUpdate()
    {
        BodyDirection.localEulerAngles = new Vector3(yAxis, xAxis, BodyDirection.localEulerAngles.z);
    }
    public void SpuwnArrow()
    {
        SpawningArrow = Instantiate(Arrow, SpawnArrow.position, SpawnArrow.rotation, SpawnArrow);
        ArrowTarget.SetActive(true);
    }
    public void FireArrow()
    {
        if (SpawningArrow != null)
        {
            SpawningArrow.GetComponent<Arrow>().FireArrow();
            SpawningArrow = null;
        }
        ArrowTarget.SetActive(false); 
        SpuwnArrow();
    }
}
