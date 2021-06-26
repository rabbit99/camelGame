using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using williamTool;

public class AIManager : MonoBehaviour
{
    private List<BaseAI> standbyAIs = new List<BaseAI>();

    private void Awake()
    {
        Singleton<GameManager>.Instance.onInitPlayerDataEvent.AddListener(InitAI);
        Singleton<GameManager>.Instance.onActivateAIEvent.AddListener(ActivateAI);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitAI(List<PlayerData> playersData)
    {
        for (int i = 0; i < playersData.Count; i++)
        {
            if (playersData[i].isAI)
            {
                standbyAIs.Add(CreateAI(playersData[i]));
            }
        }
    }

    private BaseAI CreateAI(PlayerData playerData)
    {
        //TODO
        //Use the factory model to generate all kinds of AI
        NormalAI normalAI = gameObject.AddComponent<NormalAI>();
        normalAI.aiName = playerData.playerName;
        normalAI.aiManager = this;
        return normalAI;
    }

    public void ActivateAI(string name)
    {
        BaseAI ai = standbyAIs.Find(x => x.aiName.Contains(name));
        ai.ActionBehaviour();
    }

    public void DoSpin()
    {
        Singleton<GameManager>.Instance.Spin();
    }
}
