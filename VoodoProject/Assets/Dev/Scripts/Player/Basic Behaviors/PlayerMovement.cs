using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static EnumHandler;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] VariableJoystick _joystick;

    [Header("Ditector")]
    [SerializeField] Transform _ditector;
    [SerializeField] float _ditectorOffset;
    private Vector2 playerDimention;
    private PlayerDirrection _playerDirrection;
    private Vector2 _movmentInput;
    private int _dirrectionX, _dirrectionY;
    private Vector3 _newPos;

    public PlayerDirrection PlayerDirrection => _playerDirrection;
    public float MovementSpeed { get => _speed; set => _speed = value; }

    private void Start()
    {
        playerDimention = new Vector2(transform.localScale.x, transform.localScale.y);
    }
    private void Update()
    {
        CheckInput();
        Move();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(_movmentInput.x, _movmentInput.y, 0).normalized;
        transform.position += dir * _speed * Time.deltaTime;
    }

    void CheckInput()
    {

        //Touch touch = Input.GetTouch(0);
        //if (touch.phase == TouchPhase.Began)
        //{
        //    Vector3 pointWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
        //    _joystick.transform.position = new Vector3(pointWorldPos.x, pointWorldPos.y,-5);
        //}
        _movmentInput.x = _joystick.Horizontal;
        _movmentInput.y = _joystick.Vertical;
        SetDitector();
    }

    void SetDitector()
    {
        float absX = Mathf.Abs(_movmentInput.x);
        float absY = Mathf.Abs(_movmentInput.y);

        //Right-Left
        if (absX > absY)
        {
            if (_movmentInput.x > 0)
            {
                _dirrectionX = Mathf.CeilToInt(_movmentInput.x);
                transform.localScale = new Vector3(playerDimention.x, playerDimention.y, 1);
            }
            else if (_movmentInput.x < 0)
            {
                transform.localScale = new Vector3(-playerDimention.x, playerDimention.y, 1);
            }
            _newPos = new Vector3(_ditectorOffset * _dirrectionX, 0, 0);
            _ditector.localPosition = _newPos;
            _playerDirrection = PlayerDirrection.Side;
        }
        //Up-Down
        else if (absX < absY)
        {
            if (_movmentInput.y > 0)
            {
                _dirrectionY = Mathf.CeilToInt(_movmentInput.y);
                _playerDirrection = PlayerDirrection.Up;
            }
            else if (_movmentInput.y < 0) 
            { 
                _dirrectionY = Mathf.FloorToInt(_movmentInput.y); 
                _playerDirrection = PlayerDirrection.Dowm;
            }

            _newPos = new Vector3(0, _ditectorOffset * _dirrectionY, 0);
            _ditector.localPosition = _newPos;
        }
    }



}
