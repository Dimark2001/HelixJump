using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFPS : MonoBehaviour
{
    public string FpsText;
    private float pollingTime = 0.3f;
    private float time;
    private int frameCount;
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        time += Time.deltaTime;
        frameCount++;
        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            FpsText = frameRate.ToString() + " FPS";
            time -= pollingTime;
            frameCount = 0;
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), FpsText);
    }
}
