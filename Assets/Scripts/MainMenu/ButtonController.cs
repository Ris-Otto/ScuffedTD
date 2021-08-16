using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class ButtonController : MonoBehaviour
    {
        public Button quitButton;
        public Button MapOne;
        
        private void Start() {
            quitButton.onClick.AddListener(Exit);
            MapOne.onClick.AddListener(LoadMap);
        }

        private void Exit() {
            Application.Quit();
        }

        private void LoadMap() {
            SceneManager.LoadScene("MapOne", LoadSceneMode.Single);
        }
    }
}
