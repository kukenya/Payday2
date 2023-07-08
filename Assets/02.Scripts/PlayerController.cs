using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Controls control;
    Vector2 moveDir = Vector2.zero;
    Vector2 lookDir = Vector2.zero;
    private float xRotation = 0f;

    CharacterController characterController;
    public float speed = 5;
    public float runSpeed = 10;
    public float gravity = 5f;
    private Vector3 sumVector, xVector, zVector;

    public float upDownRange = 90;
    public float mouseSensitivity = 10f;

    private void Awake()
    {
        control = new Controls();
    }

    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = control.Player.Look.ReadValue<Vector2>();
        moveDir = control.Player.Move.ReadValue<Vector2>();
        float mouseX = lookDir.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookDir.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
        xVector = transform.forward * speed * Time.deltaTime * moveDir.y;
        zVector = transform.right * speed * Time.deltaTime * moveDir.x;
        sumVector = xVector + zVector;
        sumVector.y -= gravity * Time.deltaTime;

        characterController.Move(sumVector);
    }
}
