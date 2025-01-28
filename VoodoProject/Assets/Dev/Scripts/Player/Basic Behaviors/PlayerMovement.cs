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
    [SerializeField] Vector2 _ditectorOffset;

    private Vector2 playerDimention;
    private PlayerDirrection _playerDirrection;
    private Vector2 _movmentInput;
    private int _dirrectionX=1, _dirrectionY;
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
        //Move();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(_movmentInput.x, _movmentInput.y, 0).normalized;
        transform.position += dir * _speed * Time.deltaTime;
    }

    void CheckInput()
    {
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

            _playerDirrection = PlayerDirrection.Side;
            _newPos = new Vector3(_ditectorOffset.x * _dirrectionX, 0, 0);
            _ditector.eulerAngles = Vector3.zero;
            _ditector.localPosition = _newPos;
        }

        //Up-Down
        else if (absX < absY)
        {
            //UP
            if (_movmentInput.y > 0)
            {
                _dirrectionY = Mathf.CeilToInt(_movmentInput.y);
                _playerDirrection = PlayerDirrection.Up;
                _ditector.localEulerAngles = new Vector3(0, 0, 90);
            }
            //Down
            else if (_movmentInput.y < 0)
            {
                _dirrectionY = Mathf.FloorToInt(_movmentInput.y);
                _playerDirrection = PlayerDirrection.Dowm;
                _ditector.localEulerAngles = new Vector3(0, 0, -90);
            }

            _newPos = new Vector3(0, _ditectorOffset.y * _dirrectionY, 0);
            _ditector.localPosition = _newPos;
        }
    }

}
