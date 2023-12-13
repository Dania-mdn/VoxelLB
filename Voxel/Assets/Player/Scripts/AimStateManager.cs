using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    [SerializeField] float mouseSense = 1;   
    [SerializeField] Transform BodyDirection;
    float xAxis, yAxis;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -40, 50);
    }

    private void LateUpdate()
    {
        BodyDirection.localEulerAngles = new Vector3(yAxis, BodyDirection.localEulerAngles.y, BodyDirection.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }
}
