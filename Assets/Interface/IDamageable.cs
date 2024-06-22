using UnityEngine;

public interface IDamageable
{
    public void GetEnemy(Transform enemyTransform);

    public void GetDamage(float value);
}
