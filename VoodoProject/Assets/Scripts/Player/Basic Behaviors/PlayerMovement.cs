using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Joystick _joystick;
    Vector2 _movmentInput;

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(_movmentInput.x, _movmentInput.y, 0).normalized;
        transform.position += dir * _speed * Time.fixedDeltaTime;
    }

    void CheckInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _joystick.transform.position = touch.position;
            }
            _movmentInput.x= _joystick.Horizontal;
            _movmentInput.y= _joystick.Vertical;
        }
    }

}
