using UnityEngine;
using UnityEngine.UI;

namespace Sound.UI
{
    public class ToggleOnOffSpriteAudio : MonoBehaviour
    {
        [SerializeField] Sprite Toggle_ON;
        [SerializeField] Sprite Toggle_OFF;

        Image _activeImage;

        private void Start()
        {
            _activeImage = GetComponent<Image>();
        }

        public void OnMouseDown()
        {
            if (_activeImage.sprite == Toggle_OFF)
            {
                SoundManager.Instance.AudioOn();
                _activeImage.sprite = Toggle_ON;
            }
            else
            {
                _activeImage.sprite = Toggle_OFF;
                SoundManager.Instance.AudioOff();
            }
        }
    }
}