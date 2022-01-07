using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Helpers
{
    public class ActionController : MonoBehaviour {
        public Canvas mainMenu;
        public Button exitButton;
        public Button startButton;

        public Button mainMenuButton;

        private RoundManager _roundManager;
        //TODO move to UIManager
        
        private void Start() {
            _roundManager = startButton.GetComponent<RoundManager>();
            mainMenu = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<Canvas>();
            exitButton.onClick.AddListener(ExitGame);
            mainMenuButton.onClick.AddListener(GoToMain);
            mainMenu.enabled = false;
        }
        
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) 
                ToggleCanvas();
            if (Input.GetKeyDown(KeyCode.Space)) 
                _roundManager.StartRound();
            
        }
        private void ToggleCanvas() {
            mainMenu.enabled = !mainMenu.enabled;
        }

        private static void ExitGame() {
            Application.Quit();
        }

        private static void GoToMain() {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    


    }
}
