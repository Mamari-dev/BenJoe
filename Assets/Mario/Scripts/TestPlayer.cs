using UnityEngine;

public class TestPlayer : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform enemyContainerTransform;
    [SerializeField] private float currentTime;

    public void GetDamage(float value)
    {
        currentTime -= value;
    }

    public void GetEnemy(Transform enemyTransform)
    {
        enemyTransform.SetParent(enemyContainerTransform);
    }
}
