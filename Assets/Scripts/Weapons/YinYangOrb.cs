using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class YinYangOrb : MonoBehaviour
{
    //武器使用间隔
    public float interval;
    //子弹
    public GameObject bulletPrefab;
    //枪口位置
    public Transform muzzlePos;
    //鼠标位置
    private Vector2 mousePos;
    //方向
    private Vector2 direction;
    //计时器
    private float timer;
    //动画器
    private Animator animator;
    //记录武器y值
    private float flipY;
    //获取按键
    public PlayerInputAction inputControl;

    void Start()
    {
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        flipY = transform.localScale.y;
        inputControl = new PlayerInputAction();
    }

    void Update()
    {
        //获取鼠标位置，以相机中心转换为世界坐标
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //翻转武器
        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3 (flipY, -flipY, 1);
        else
            transform.localScale = new Vector3(flipY, flipY, 1);
        //开火
        Shoot();
    }

    void Shoot()
    {
        //获取方向
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        //更新武器的朝向
        //transform.right = direction;

        //判断是否计时
        if(timer != 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0) 
                timer = 0;
        }
        //是否按下开火键inputControl.Gameplay.Fire.WasPressedThisFrame()
        if (Mouse.current.leftButton.IsPressed())
        {
            Debug.Log("Fire");
            if (timer == 0)
            {
                timer = interval;
                Fire();
            }
        }

    }

    void Fire()
    {
        animator.SetTrigger("Shoot");
        //生成预制体到枪口
        //GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzlePos.position;

        float angle = Random.Range(-5f, 5f);
        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angle, Vector3.forward) * direction);

        bullet.transform.right = direction;
        //设置子弹方向
        bullet.GetComponent<Bullet>().SetSpeed(direction);
    }
}
