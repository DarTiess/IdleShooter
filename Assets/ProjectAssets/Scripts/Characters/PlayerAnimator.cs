using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator= GetComponent<Animator>();
    }

    public void MoveAnimation(float speed)
    {
         _animator.SetFloat("IsMove",speed);
    }
}
