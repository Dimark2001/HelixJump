using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{
    public Color BarColor;
    [Range(1f, 100f)]
    public int Alert = 20;
    public Color BarAlertColor;
    public GameObject nextLevel;
    private Image bar, barBackground;
    private float nextPlay;
    private Text txtTitle;
    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            UpdateValue(barValue);
        }
    }

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
    }

    private void Start()
    {
        bar.color = BarColor;
        barValue = 0;

        UpdateValue(barValue);
    }

    void UpdateValue(float val)
    {
        bar.fillAmount = val/100;
    }
    private void Update()
    {
        if (!Application.isPlaying)
        {           
            UpdateValue(Alert);

            bar.color = BarColor;        
        }
        if(bar.fillAmount == 1f)
            nextLevel.GetComponent<Image>().color = BarColor;
        else
            nextLevel.GetComponent<Image>().color = BarAlertColor;
    }
}