using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float MovementSpeed = 25;
    public void Update()
    {
        if (!LevelBuilderManager.Paused)
        {
            float speedMultiplier = Input.GetKey(KeyCode.LeftShift)
                ? MovementSpeed * 1.5f : MovementSpeed;
            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.Translate(Time.deltaTime * Vector3.up * speedMultiplier);
            }
            if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.Translate(Time.deltaTime * Vector3.down * speedMultiplier);
            }
            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.Translate(Time.deltaTime * Vector3.left * speedMultiplier);
            }
            if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.Translate(Time.deltaTime * Vector3.right * speedMultiplier);
            }
        }

    }
}
