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
        if (other.TryGetComponent<IHealth>(out IHealth health)
            && other.gameObject.tag != _parentName)
        {
            health.TakeDamage(_attackPower);
            Hide();
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
           Hide();
        }

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
