using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //子弹速度
    public float speed;
    //传入爆炸效果
    public GameObject explosionPrefab;
    //获取刚体
    new public Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(Vector2 direction)
    {
        //设置子弹的速度和方向
        rigidbody.velocity = direction * speed;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;

        //Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }
}
