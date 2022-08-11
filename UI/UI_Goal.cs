using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_Goal : MonoBehaviour
{
    private CanvasRenderer _canvas;
    private bool uiEnabled = true;

    void Awake()
    {
        _canvas = GetComponent<CanvasRenderer>();
        if (_canvas == null)
        {
            Debug.LogWarning("Missing canvas renderer on goal ui");
        }
    }

    void Update()
    {
        var progress = GameManager.Instance.GetRaceProgress();

        if (progress == 1.0f)
        {
            if (uiEnabled == false)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                uiEnabled = true;
            }
        }
        else
        {
            if (uiEnabled == true)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }

                uiEnabled = false;
            }
        }
    }
}