using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    public int health;
    public float speed = 2f;
    public float sensitivity;
    public Transform body;
    private float rotateX;
    public Transform check;
    public bool isGrounded;
    public float radius;
    public LayerMask mask;
    private Vector3 velocity;
    private CharacterController characterController;
    private float targetRotation;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(check.position, radius, mask);
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var look = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;

        rotateX -= look.y;
        rotateX = Mathf.Clamp(rotateX, -90, 90);
        body.localRotation = Quaternion.Euler(rotateX, 0, 0);
        if(move != Vector3.zero)
        {
            var inputDir = move.normalized;
            targetRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            var targetDir = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
            characterController.Move(targetDir.normalized * speed * Time.deltaTime);
        }
        transform.Rotate(Vector3.up * look.x);
        if (!isGrounded)
        {
            velocity.y += Time.deltaTime * -9.8f;
        }
        else
        {
            if(velocity.y < 0)
                velocity.y = -1f;
            if (Input.GetButton("Jump"))
            {
                velocity.y = 1.8f;
            }
        }
        characterController.Move(velocity * Time.deltaTime * 5f);
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(check.position, radius);
    }
}
