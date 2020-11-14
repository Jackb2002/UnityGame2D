using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float MovementSpeed = 5f;
    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Time.deltaTime * Vector3.up * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Time.deltaTime * Vector3.down * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Time.deltaTime * Vector3.left * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Time.deltaTime * Vector3.right * MovementSpeed);
        }

    }
}
