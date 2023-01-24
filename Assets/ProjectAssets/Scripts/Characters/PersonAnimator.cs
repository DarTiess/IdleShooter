using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PersonAnimator : MonoBehaviour
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

    public void DeadAnimation()
    {
        _animator.SetTrigger("Lose");
    }

    public void GetTreeAnimation()
    {
        _animator.SetBool("GetTree", true);
    }
    public void EndTreeAnimation()
    {
        _animator.SetBool("GetTree", false);
    }

}
