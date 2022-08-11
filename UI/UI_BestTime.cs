using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_BestTime : MonoBehaviour
{
    private TMPro.TMP_Text _text;

    void Awake()
    {
        _text = gameObject.GetComponent<TMPro.TMP_Text>();
        if (_text == null)
        {
            Debug.LogWarning("Missing text in besttime");
        }
    }

    void Update()
    {
        var timespan = GameManager.Instance.GetBestTime();
        if (timespan == TimeSpan.MaxValue)
            _text.text = "";
        else
            _text.text = timespan.ToString(@"mm\:ss\.ff");
    }
}
