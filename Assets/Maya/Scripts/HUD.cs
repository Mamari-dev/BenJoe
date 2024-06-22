using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer = GameManager.Instance.Timer;
    }
}
