using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_ProgressBar : MonoBehaviour
{
    [SerializeField] Transform _bar;

    public static UI_ProgressBar Create(Vector3 position, Vector3 size)
    {
        // Main Progress Bar
        GameObject progressBarGameObject = new GameObject("ProgressBar");
        progressBarGameObject.transform.position = position;

        // Background
        GameObject backgroundGameObject = new GameObject("Background", typeof(Image));
        backgroundGameObject.transform.SetParent(progressBarGameObject.transform);
        backgroundGameObject.transform.localPosition = Vector3.zero;
        backgroundGameObject.transform.localScale = size;
        backgroundGameObject.GetComponent<Image>().color = Color.gray;

        // Bar
        GameObject barGameObject = new GameObject("Bar");
        barGameObject.transform.SetParent(progressBarGameObject.transform);
        barGameObject.transform.localPosition = new Vector3(-size.x / 2f, 0f);

        // Bar Sprite
        GameObject barSpriteGameObject = new GameObject("BarSprite");
        barSpriteGameObject.transform.SetParent(barGameObject.transform);
        barSpriteGameObject.transform.localPosition = new Vector3(size.x / 2f, 0f);
        barSpriteGameObject.transform.localScale = size;
        barSpriteGameObject.GetComponent<Image>().color = Color.green;

        UI_ProgressBar uiProgressBar = progressBarGameObject.AddComponent<UI_ProgressBar>();
        return uiProgressBar;
    }

    void Awake()
    {
    }

    public void SetSize(float sizeNormalized)
    {
        _bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color color)
    {
        _bar.Find("BarSprite").GetComponent<Image>().color = color;
    }

    void Update()
    {
        SetSize(GameManager.Instance.GetRaceProgress());
    }
}