using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<DoorPairStruct> doorPairs;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private float maxTimer;
    private float timer;
    private bool timerIsRunning = false;

    public float Timer { get => timer; }
    public bool TimerIsRunning { get => timerIsRunning; }

    private PairID currentPairID = PairID.None;
    private PanoramaPart currentPanoramaPart = PanoramaPart.None;
    private DistortionLevel currentDistortionLevel = DistortionLevel.None;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        audioManager.DoorPairs = doorPairs;

        audioManager.StartIdleSounds();
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            timer -= Time.deltaTime;

            if(timer < maxTimer * 2 / 3 && currentDistortionLevel == DistortionLevel.None)
            {
                currentDistortionLevel = DistortionLevel.Slightly;
                audioManager.ChangeMelodyDistortion(currentDistortionLevel);
            }
            else if (timer < maxTimer * 1 / 3 && currentDistortionLevel == DistortionLevel.Slightly)
            {
                currentDistortionLevel = DistortionLevel.Highly;
                audioManager.ChangeMelodyDistortion(currentDistortionLevel);
            }

            if (timer <= 0)
            {
                RanOutOfTime();
            }
        }
    }

    public bool TestPairID(PairID newPairID, PanoramaPart panoramaPart)
    {
        //neuer try
        if (currentPairID == PairID.None)
        {
            currentPairID = newPairID;
            currentPanoramaPart = panoramaPart;
            StartTimer();
            return false;
        }

        //memory abgleichen
        if(currentPairID == newPairID && currentPanoramaPart != panoramaPart)
        {
            //player found right pair in time
            FoundInTime();
            return false;
        }
        else
        {
            //player found wrong pair in time
            WrongInTime();
            return true;
        }
    }

    private void StartTimer()
    {
        EnemyManager.Instance.ResetEnemy();
        UIManager.Instance.CollectMemoryPartUI(currentPairID, currentPanoramaPart);

        timer += maxTimer;
        timerIsRunning = true;

        //stop environment + Adlibs
        audioManager.StopIdleSounds();
        //start music with Pair Id
        audioManager.StartMelodyID(currentPairID);
    }

    private void FoundInTime()
    {
        EnemyManager.Instance.ResetEnemy();
        UIManager.Instance.CollectMemoryPairUI(true);

        timerIsRunning = false;
        timer = 0;
        doorPairs[(int)currentPairID - 1].OpenPair();

        //stop melody
        audioManager.StopMelodySounds();
        //Start environment + Adlibs + YAAYY U DID IT SOUND (oder so)
        audioManager.PlayCorrectSound();
        audioManager.StartIdleSounds();

        currentPairID = PairID.None;
        currentPanoramaPart = PanoramaPart.None;

        if (WinCheck())
        {
            //das passiert beim gewinnen!!!!
        }
    }

    private void WrongInTime()
    {
        EnemyManager.Instance.ResetEnemy();
        UIManager.Instance.CollectMemoryPairUI(false);

        timerIsRunning = false;
        timer = 0;

        //stop melody
        audioManager.StopMelodySounds();
        //Start environment + Adlibs + DUMDUM HAST ES NICHT GESCHAFFT
        audioManager.PlayIncorrectSound();
        audioManager.StartIdleSounds();

        //make first door interactable again
        doorPairs[(int)currentPairID - 1].MakeInteractable();

        currentPairID = PairID.None;
        currentPanoramaPart = PanoramaPart.None;
    }

    private void RanOutOfTime()
    {
        EnemyManager.Instance.ResetEnemy();
        UIManager.Instance.CollectMemoryPairUI(false);


        timerIsRunning = false;
        timer = 0;

        //stop melody
        audioManager.StopMelodySounds();
        //Start environment + Adlibs + DUMDUM HAST ES NICHT GESCHAFFT
        audioManager.PlayIncorrectSound();
        audioManager.StartIdleSounds();

        //make first door interactable again
        doorPairs[(int)currentPairID - 1].MakeInteractable();

        currentPairID = PairID.None;
        currentPanoramaPart = PanoramaPart.None;
    }

    public void Ouch(float ouchAmount)
    {
        timer -= ouchAmount;
    }

    private bool WinCheck()
    {
        foreach(DoorPairStruct pair in doorPairs)
        {
            if (!pair.IsCollected)
            {
                return false;
            }
        }

        return true;
    }
}
