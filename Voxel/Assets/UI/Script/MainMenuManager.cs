using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public float sensitivity = 15.0f;
    public Transform PlayerModel;
    float xAxis;

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
            xAxis += Input.GetAxisRaw("Mouse X") * sensitivity;
            PlayerModel.transform.eulerAngles = new Vector3(PlayerModel.transform.eulerAngles.x, -xAxis, PlayerModel.transform.eulerAngles.z);
    }
    public void SetGame()
    {
        SceneManager.LoadScene(0);
    }
}
