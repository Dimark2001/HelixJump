using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneFader : MonoBehaviour
{
    private const string FADER_PATH = "SceneFader";
    public Image _image;
    public AnimationCurve _curve;
    private static SceneFader _instance;
    public static SceneFader instance
    {
        get
        {
            if(_instance == null)
            {
                var prefab = Resources.Load<SceneFader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            
            return _instance;
        }
    }
    void Start()
    {
        StartCoroutine(FadeIn());
        this.gameObject.SetActive(true);
    }

    public void FadeToIn()
    {
        StartCoroutine(FadeIn());
    }
    public void FadeToOut(int currentScene)
    {
        Debug.Log($"FadeToOut = {SceneManager.sceneCountInBuildSettings}");
        if(currentScene < SceneManager.sceneCountInBuildSettings)
            StartCoroutine(FadeOut(currentScene));
        else
            StartCoroutine(FadeOut(0));
    }
    
    
    IEnumerator FadeIn()
    {
        float t = 1f;
        while(t > 0f)
        {
            t -= Time.deltaTime; 
            float a = _curve.Evaluate(t);
            _image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }
    IEnumerator FadeOut(int currentScene)
    {
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime; 
            float a = _curve.Evaluate(t);
            _image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(currentScene);
    }

    internal void FadeToOut(object levelName)
    {
        throw new NotImplementedException();
    }
}
