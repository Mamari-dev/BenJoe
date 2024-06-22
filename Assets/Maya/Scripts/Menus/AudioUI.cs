using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUI : MonoBehaviour
{
    private bool masterOn;
    private bool musicOn;
    private bool soundOn;
    private bool environmentOn;
    private bool melodyOn;

    #region Input Handling

    public void OnToggleMaster(bool _enable)
    {
        masterOn = _enable;

        if (masterOn)
        {
            // deactivate others but without changing bool
        }
        else
        {
            // put others back to what they where (bools)
        }
    }

    public void OnToggleMusic(bool _enable)
    {
        musicOn = _enable;
    }

    public void OnToggleSound(bool _enable)
    {
        soundOn = _enable;
    }

    #endregion
}
