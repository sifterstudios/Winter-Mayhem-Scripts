using UnityEngine;

public class UI_TogglePanelOnOff : MonoBehaviour
{
    [SerializeField] GameObject ObjectToToggle;

    public void TogglePanelVisibility()
    {
        ObjectToToggle.SetActive(!ObjectToToggle.activeSelf);
    }
}