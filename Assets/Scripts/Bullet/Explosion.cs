using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //��ȡ������
    private Animator animator;
    //��ȡ��������
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
