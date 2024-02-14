using TMPro;

using UnityEngine;
namespace Asteroids.GUI
{
    public class MainGameView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text PlayerVelocity { get; private set; }
    }
}
