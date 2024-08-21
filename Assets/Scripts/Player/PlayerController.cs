using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputDirection;
    public PlayerInputAction inputControl;
    private Rigidbody2D rb;
    public float speed;

    private void Awake()
    {
        inputControl = new PlayerInputAction();

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        //获取方向输入
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //速度更新 基准速度x相对时间x输入方向
        rb.velocity = new Vector2(speed * Time.deltaTime * inputDirection.x ,speed*Time.deltaTime*inputDirection.y);

        //人物方向变量
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }
        else if (inputDirection.x < 0)
        {
            faceDir = -1;
        }
        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
}
