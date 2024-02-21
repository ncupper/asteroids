using TMPro;

using UnityEngine;
using UnityEngine.UI;
namespace Asteroids.GUI
{
    public class GameOverView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Result { get; private set; }
        [field: SerializeField] public Button Continue { get; private set; }
    }
}
