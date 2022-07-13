using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public PlayerScriptable playerData;
    public UIContainer uiContainer;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI gameScore;
    private const int totalNeededGameScore = 100;
    public TextMeshProUGUI levelText;
    public GameObject finger;

    Color32 normal;
    Color32 clicked;
    private Vector3 initialFingerScale;
    public List<Button> buyButtons;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject successUI, failUI;
    void Awake()
    {
        buyButtons[0].onClick.AddListener(() => Buy(0));
        buyButtons[1].onClick.AddListener(() => Buy(1));
        buyButtons[2].onClick.AddListener(() => Buy(3));
    }
    void Start()
    {
        moneyText = uiContainer.moneyText.GetComponent<TextMeshProUGUI>();
        playerData.levelNumber = SceneManager.GetActiveScene().buildIndex + 1;
        levelText.text = "Level " + playerData.levelNumber.ToString();
        initialFingerScale = finger.transform.localScale;
        normal = finger.GetComponent<Image>().color;
        clicked = new Color32(205, 205, 205, 150);
    }
    void Update()
    {
        UpdateTexts();

        DisplayFingerOnCanvas();
    }

    private void UpdateTexts()
    {
        moneyText.text = playerData.playerMoney.ToString();
        gameScore.text = playerData.gameScore.ToString() + "/" + totalNeededGameScore;
    }

    private void DisplayFingerOnCanvas()
    {
        finger.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 35f, Input.mousePosition.z);

        if (Input.GetMouseButtonDown(0))
        {
            finger.GetComponent<Image>().color = clicked;
            finger.transform.DOScale(initialFingerScale * 2f, .1f).OnComplete(() =>
            finger.transform.DOScale(initialFingerScale, .1f).OnComplete(() =>
            finger.GetComponent<Image>().color = normal));
        }
    }

    private void OnEnable()
    {
        GameManager.winEvent += ShowSuccessUI;
        GameManager.loseEvent += ShowFailUI;
    }
    private void OnDisable()
    {
        GameManager.winEvent -= ShowSuccessUI;
        GameManager.loseEvent -= ShowFailUI;
    }
    private void ShowSuccessUI()
    {
        StartCoroutine(PlaySuccessUIPhase(1.5f));

    }
    private IEnumerator PlaySuccessUIPhase(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        successUI.transform.localScale = Vector3.zero;
        successUI.SetActive(true);
        successUI.transform.DOScale(Vector3.one, 1.25f);
    }
    private void ShowFailUI()
    {
        StartCoroutine(PlayFailUIPhase(1.5f));
    }
    private IEnumerator PlayFailUIPhase(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        failUI.transform.localScale = Vector3.zero;
        failUI.SetActive(true);
        failUI.transform.DOScale(Vector3.one, 1.25f);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void OnStartButtonClicked()
    {
        startButton.SetActive(false);
        GameManager.Instance.StartGame();
    }
    public void Buy(int typeIndex)
    {

    }
}
