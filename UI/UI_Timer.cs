using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    private TMPro.TMP_Text _text;

    void Awake()
    {
        _text = gameObject.GetComponent<TMPro.TMP_Text>();
        if (_text == null)
        {
            Debug.LogWarning("BOOOOO, This sucks");
        }
    }

    void Update()
    {
        var timespan = GameManager.Instance.GetTimer();
        _text.text = timespan.ToString(@"mm\:ss\.ff");
    }
}
