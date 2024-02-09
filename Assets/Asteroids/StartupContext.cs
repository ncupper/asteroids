using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class StartupContext : MonoBehaviour
    {
        private const int GameSceneIndex = 1;

        private void Start()
        {
            SceneManager.LoadScene(GameSceneIndex);
        }
    }
}
