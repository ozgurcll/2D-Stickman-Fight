using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private string sceneName = "MenuScene";
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;

    [SerializeField] private GameObject pauseGameUI;
    [SerializeField] private GameObject inGameUI;

    private void Start()
    {
        SwitchTo(inGameUI);
        fadeScreen.gameObject.SetActive(true);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SwitchWithKeyTo(pauseGameUI);
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            AudioManager.instance.PlaySFX(4, null);
            _menu.SetActive(true);
        }

        if (_menu == inGameUI)
            GameManager.instance.PauseGame(false);
        else
            GameManager.instance.PauseGame(true);
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            SwitchTo(inGameUI);
            return;
        }
        SwitchTo(_menu);
    }

    public void SwitchOnEndScreen()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        StartCoroutine(DieScreenCorutione());
    }

    public void SwitchOnTimeOutScreen()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        StartCoroutine(TimeOutScreenCorutione());
    }
    IEnumerator DieScreenCorutione()
    {
        yield return new WaitForSeconds(1);
        endText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
    }

    IEnumerator TimeOutScreenCorutione()
    {
        yield return new WaitForSeconds(1);
        endText.GetComponent<TextMeshProUGUI>().text = "Score: " + GameManager.instance.score;
        endText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.PauseGame(true);
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySFX(0, null);
        StartCoroutine(LoadSceneWithFadeEffect(.2f));
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();
        GameManager.instance.PauseGame(false);
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();


}
