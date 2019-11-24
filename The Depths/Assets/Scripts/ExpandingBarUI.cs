using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingBarUI : Singleton<ExpandingBarUI>
{
    public enum ExpandStyle { FromCenter, FromLeft };
    public ExpandStyle BarExpandStyle;
    public Transform barBackground;
    public Transform bar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateBarUI (float maxBarValue, float currentBarValue)
    {
        bar.localScale = new Vector2(currentBarValue / maxBarValue, 1);
        if (BarExpandStyle == ExpandStyle.FromLeft)
        {
            bar.localPosition = new Vector2(((RectTransform)barBackground).rect.width/2 * (currentBarValue/maxBarValue - 1), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
