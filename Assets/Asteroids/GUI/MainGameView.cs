using TMPro;

using UnityEngine;
namespace Asteroids.GUI
{
    public class MainGameView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Scores { get; private set; }
        [field: SerializeField] public TMP_Text PlayerPosition { get; private set; }
        [field: SerializeField] public TMP_Text PlayerAngle { get; private set; }
        [field: SerializeField] public TMP_Text PlayerVelocity { get; private set; }
        [field: SerializeField] public TMP_Text LaserCharges { get; private set; }
        [field: SerializeField] public TMP_Text LaserRestoreTimer { get; private set; }
        [field: SerializeField] public TMP_Text Round { get; private set; }
    }
}
