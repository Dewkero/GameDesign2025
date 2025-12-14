using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject imageOne;
    public GameObject imageTwo;
    public float displayTime = 60f;

    private void Start()
    {
        imageOne.SetActive(false);
        imageTwo.SetActive(false);
    }

    public void PlayGame()
    {
        //SceneManager.LoadSceneAsync("Level");
        StartCoroutine(ShowImageAndStartgame());
    }

    IEnumerator ShowImageAndStartgame()
    {
        imageOne.SetActive(true);
        CanvasGroup firstImage = imageOne.GetComponent<CanvasGroup>();
        firstImage.alpha = 0f;
        float fadeDuration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration) 
        {
            firstImage.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        firstImage.alpha = 1f;
        yield return new WaitForSeconds(displayTime);

        imageTwo.SetActive(true);
        CanvasGroup secondImage = imageTwo.GetComponent<CanvasGroup>();
        secondImage.alpha = 0f;
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            secondImage.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        secondImage.alpha = 1f;
        yield return new WaitForSeconds(displayTime);

        SceneManager.LoadSceneAsync("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
