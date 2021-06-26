using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using williamTool;
public class ConsoleManager : MonoBehaviour
{
    public GameObject CurrentControlUIGameObject;
    public Text stepText;
    public Button spinBtn;

    private void Awake()
    {
        Singleton<GameManager>.Instance.onGameManagerPlayResponseEvent.AddListener(ShowStepText);
        Singleton<GameManager>.Instance.onShowCurrentControlUIEvent.AddListener(ShowCurrentControlUI);
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
        stepText.text = index + "步";
    }

    public void ShowCurrentControlUI(bool show)
    {
        CurrentControlUIGameObject.SetActive(show);
    }
}
