using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float limt_look_x=180f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            Move();
            jumpping();
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        // Quay camera
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseX, 0);

        float currentRotationX = Camera.main.transform.localEulerAngles.x;
        float newRotationX = currentRotationX - mouseY;

        // Gi?i h?n góc quay c?a camera
        if (newRotationX > limt_look_x)
        {
            newRotationX -= 360f;
        }
        newRotationX = Mathf.Clamp(newRotationX, -60f, 60f);
        if (newRotationX < -limt_look_x)
        {
            newRotationX += 360f;
        }

        Camera.main.transform.localEulerAngles = new Vector3(newRotationX, 0, 0);
    }



    protected void Move()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

       
    }
    protected void jumpping()
    {
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Scope")
        {
            Destroy(other.gameObject);
            /*GameObject.Find("Carbine").GetComponent<Transform>().transform.AddComponent<ScopeDecorator>();*/

            Weapon currentWeapon = GetComponent<Weapon>();
            ScopeDecorator scopedGun = GameObject.Find("Carbine").GetComponent<Transform>().transform.AddComponent<ScopeDecorator>();
            scopedGun.Initialize(currentWeapon, Camera.main);
        }
    }
}
