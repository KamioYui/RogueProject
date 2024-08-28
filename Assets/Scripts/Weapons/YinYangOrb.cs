using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class YinYangOrb : MonoBehaviour
{
    //����ʹ�ü��
    public float interval;
    //�ӵ�
    public GameObject bulletPrefab;
    //ǹ��λ��
    public Transform muzzlePos;
    //���λ��
    private Vector2 mousePos;
    //����
    private Vector2 direction;
    //��ʱ��
    private float timer;
    //������
    private Animator animator;
    //��¼����yֵ
    private float flipY;
    //��ȡ����
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
        //��ȡ���λ�ã����������ת��Ϊ��������
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //��ת����
        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3 (flipY, -flipY, 1);
        else
            transform.localScale = new Vector3(flipY, flipY, 1);
        //����
        Shoot();
    }

    void Shoot()
    {
        //��ȡ����
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        //���������ĳ���
        //transform.right = direction;

        //�ж��Ƿ��ʱ
        if(timer != 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0) 
                timer = 0;
        }
        //�Ƿ��¿����inputControl.Gameplay.Fire.WasPressedThisFrame()
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
        //����Ԥ���嵽ǹ��
        //GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzlePos.position;

        float angle = Random.Range(-5f, 5f);
        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angle, Vector3.forward) * direction);

        bullet.transform.right = direction;
        //�����ӵ�����
        bullet.GetComponent<Bullet>().SetSpeed(direction);
    }
}
