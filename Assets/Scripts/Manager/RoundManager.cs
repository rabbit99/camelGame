using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using williamTool;

public class RoundManager : MonoBehaviour
{
    private List<PlayerData> playersData = new List<PlayerData>();
    private int sequenceIndex = 0;
    public int delaySeconds = 1;
    private void Awake()
    {
        Singleton<GameManager>.Instance.onInitPlayerDataEvent.AddListener(InitPlayerData);
        Singleton<GameManager>.Instance.onStartRoundkEvent.AddListener(StartRound);
        Singleton<GameManager>.Instance.onStopRoundkEvent.AddListener(NextPlayer);
        Singleton<GameManager>.Instance.onGameManagerPlayResponseEvent.AddListener(onGameManagerPlayResponse);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitPlayerData(List<PlayerData> _playersData)
    {
        playersData = _playersData;
    }

    public void StartRound()
    {
        processRound();
    }

    public void NextPlayer()
    {
        StartCoroutine(DelayRequest(() => {
            sequenceIndex++;
            //Debug.Log("NextPlayer sequenceIndex  = " + sequenceIndex + "  playersData.Count = "+ playersData.Count);
            if (sequenceIndex > playersData.Count - 1)
            {
                sequenceIndex = 0;
            }
            processRound();
        }));

    }

    private void processRound()
    {
        if (playersData[sequenceIndex].isMine)
        {
            //Activate Current Control UI
            Singleton<GameManager>.Instance.ShowCurrentControlUI(true);
        }
        else
        {
            //Activate the AI in that order and closed Current Control UI
            Singleton<GameManager>.Instance.ShowCurrentControlUI(false);
            Singleton<GameManager>.Instance.ActivateAI(playersData[sequenceIndex].playerName);
        }
        Singleton<GameManager>.Instance.UpdateNowPlayerInfo(playersData[sequenceIndex]);
    }

    private void onGameManagerPlayResponse(int index)
    {
        StartCoroutine(DelayRequest(() => { Singleton<GameManager>.Instance.StopRound(); }));
    }

    private IEnumerator DelayRequest(Action callback)
    {
        yield return new WaitForSeconds(delaySeconds);
        callback();
    }
}
