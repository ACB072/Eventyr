using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPercentage : MonoBehaviour
{
    public Text percentage;

    // Update is called once per frame
    void Update()
    {
        percentage.text = Mathf.RoundToInt(GetComponent<Slider>().value * 100) + "%";
    }
}
