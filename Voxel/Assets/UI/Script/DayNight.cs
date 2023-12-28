using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

        if (transform.rotation.eulerAngles.x > 270 && transform.rotation.eulerAngles.x < 360)
            EventSystem.DoNight(true);
        else 
            EventSystem.DoNight(false);
    }
}
