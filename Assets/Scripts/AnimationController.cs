using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }
    public  void EndDamage(int damage)
    {
        animator.SetBool("IsDamage", false);

    }
}
