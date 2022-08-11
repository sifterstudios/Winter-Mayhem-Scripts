using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [Tooltip("Max impulse magnitude to play max sound")] [SerializeField]
    float _clampImpulseMagnitude = 5000.0f;

    public enum CollisionMaterial
    {
        None = 0,
        Wood = 1,
        Stone = 2,
        Player = 3,
        Yeti = 4,
        Ground = 5,
        Ice = 6,
    }

    [SerializeField] CollisionMaterial _chooseCollisionMaterial;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagConstants.OtherPlayer))
        {
            print("Collided with OtherPlayer");
        }

        if (collision.gameObject.CompareTag(TagConstants.Player))
        {
            var clampedMagnitude = collision.impulse.magnitude;
            if (clampedMagnitude > _clampImpulseMagnitude)
            {
                clampedMagnitude = _clampImpulseMagnitude;
            }

            var magnitude = clampedMagnitude / _clampImpulseMagnitude;
            var sound = "Collision_" + _chooseCollisionMaterial.ToString();
            

            if (_chooseCollisionMaterial != CollisionMaterial.None)
            {
                SoundManager.Instance.PlayCollisionSound(sound, magnitude, collision.gameObject.transform);
            }
        }
    }
}