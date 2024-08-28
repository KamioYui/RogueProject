using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //获取动画器
    private Animator animator;
    //获取动画进度
    private AnimatorStateInfo info;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime >= 1)
        {
            //Destroy(gameObject);
            ObjectPool.Instance.PushObject(gameObject);
        }

    }
}
