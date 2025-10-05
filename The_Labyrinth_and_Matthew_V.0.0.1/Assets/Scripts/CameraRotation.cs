using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject Player;
    private Rigidbody rb;
    public float speed;

    private float MouseX;
    public float mouseSpeed;

    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, MouseX, 0);
    }
}
