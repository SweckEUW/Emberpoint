using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public CanvasGroup startMenu;
    public CanvasGroup ui;
    Image logo;
    Image button;

    public AnimationCurve animationCurve;
    public float fadingSpeed = 5f;

    public enum Direction { FadeIn, FadeOut };

    void Start()
    {
        startMenu = GetComponent<CanvasGroup>();

        if (animationCurve.length == 0)
        {
            animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }

        ui.alpha = 0f;
         
        logo = transform.Find("Logo").GetComponent<Image>();
        button = transform.Find("Button").GetComponent<Image>();

        logo.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        button.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().fontMaterial.SetColor("_FaceColor", Color.clear); ;

        StartCoroutine(StartupAnimation());
    }

    public void StartGame()
    {
        button.GetComponent<Button>().interactable = false;
        StartCoroutine(FadeCanvas(startMenu, Direction.FadeOut, fadingSpeed));
        StartCoroutine(FadeCanvas(ui, Direction.FadeIn, fadingSpeed));

        GameObject.Find("Intro Cutscene").GetComponent<IntroCutscene>().PlayIntro();
    }

    IEnumerator StartupAnimation()
    {
        yield return new WaitForSeconds(3f);
        logo.CrossFadeAlpha(1f, 2f, false);

        yield return new WaitForSeconds(5f);
        button.CrossFadeAlpha(1f, 0.5f, false);
        StartCoroutine(FadeText());
        button.GetComponent<Button>().interactable = true;
    }

    IEnumerator FadeText()
    {
        float waitTime = 0;
        while (waitTime < 1)
        {
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.clear, Color.white, waitTime)); ;
            yield return null;
            waitTime += Time.deltaTime / 0.5f;
        }
    }

    IEnumerator FadeCanvas(CanvasGroup canvasGroup, Direction direction, float duration)
    {
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var elapsedTime = 0f;

        if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(0f);
        else canvasGroup.alpha = animationCurve.Evaluate(1f);

        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime;
            var percentage = 1 / (duration / elapsedTime);
            if ((direction == Direction.FadeOut))
            {
                canvasGroup.alpha = animationCurve.Evaluate(1f - percentage);
            }
            else
            {
                canvasGroup.alpha = animationCurve.Evaluate(percentage);
            }

            yield return new WaitForEndOfFrame();
        }

        if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(1f);
        else canvasGroup.alpha = animationCurve.Evaluate(0f);
    }
}