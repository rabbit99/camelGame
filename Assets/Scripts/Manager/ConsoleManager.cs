using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using williamTool;
public class ConsoleManager : MonoBehaviour
{
    public GameObject currentControlUIGameObject;
    public Text nowPlayerText;
    public Text stepText;
    public Button spinBtn;

    private string nowPlayerName;
    private void Awake()
    {
        Singleton<GameManager>.Instance.onGameManagerPlayResponseEvent.AddListener(ShowStepText);
        Singleton<GameManager>.Instance.onShowCurrentControlUIEvent.AddListener(ShowCurrentControlUI);
        Singleton<GameManager>.Instance.onUpdateNowPlayerInfoEvent.AddListener(ShowNowPlayerInfo);
        Singleton<GameManager>.Instance.onStopRoundkEvent.AddListener(StopRound);
        spinBtn.onClick.AddListener(Singleton<GameManager>.Instance.Spin);
        spinBtn.onClick.AddListener(delegate { ShowCurrentControlUI(false); });
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowStepText(int index)
    {
        stepText.text = "擲出" + index + "步";
    }

    public void ShowCurrentControlUI(bool show)
    {
        currentControlUIGameObject.SetActive(show);
    }

    public void ShowNowPlayerInfo(PlayerData playerData)
    {
        nowPlayerName = playerData.playerName;
        nowPlayerText.text = playerData.playerName + "的回合 等待行動中..";
    }

    public void StopRound()
    {
        stepText.text = "";
        nowPlayerText.text = "結束" + nowPlayerName + "的回合";
    }
}
