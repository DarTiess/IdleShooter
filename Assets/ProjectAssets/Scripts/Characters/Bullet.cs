using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _attackPower;
    private string _parentName;

    public void SetAttackPower(int attackPower, string parentName)
    {
        _attackPower = attackPower;
        _parentName = parentName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            && other.gameObject.tag != _parentName)
        {
            other.gameObject.GetComponent<IHealth>().TakeDamage(_attackPower);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
        }

    }

}
