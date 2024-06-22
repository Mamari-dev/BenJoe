using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public delegate void ResetEnemyDelegate();
    public event ResetEnemyDelegate ResetEnemyEvent;

    private void Awake()
    {
        Instance = this;
    }

    public void ResetEnemy()
    {
        ResetEnemyEvent?.Invoke();
    }

    private void OnDisable()
    {
        ResetEnemyEvent = null;   
    }
}
