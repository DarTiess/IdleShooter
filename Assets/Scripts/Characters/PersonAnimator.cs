using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PersonAnimator : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IS_MOVE = Animator.StringToHash("IsMove");
    private static readonly int LOSE = Animator.StringToHash("Lose");
    private static readonly int GET_TREE = Animator.StringToHash("GetTree");
    private static readonly int IS_FLYING = Animator.StringToHash("IsFlying");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(float speed)
    {
        _animator.SetFloat(IS_MOVE, speed);
    }

    public void DeadAnimation()
    {
        _animator.SetTrigger(LOSE);
    }

    public void GetTreeAnimation()
    {
        _animator.SetBool(GET_TREE, true);
    }
    public void EndTreeAnimation()
    {
        _animator.SetBool(GET_TREE, false);
    }

    public void FlyingAnimaton(float speed)
    {
        _animator.SetFloat(IS_FLYING, speed);
    }

}
