using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Controller
{
    private Vector3 moveVector;
    [SerializeField] private FloatingJoystick joystick;



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Move();
    }

    private void Move()
    {
        if (GameManager.instance.isFinish == true) return;
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * speed * Time.deltaTime;
        moveVector.z = joystick.Vertical * speed * Time.deltaTime;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            ChangeAnim("Run");
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            ChangeAnim("Idle");
        }

        rb.MovePosition(rb.position + moveVector);
    }


}
