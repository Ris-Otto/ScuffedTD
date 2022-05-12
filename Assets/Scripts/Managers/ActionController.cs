using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class ActionController : MonoBehaviour {
        public Canvas mainMenu;
        public Button exitButton;
        public Button startButton;

        public Button mainMenuButton;
        private float previousTimeScale;

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
            if (Input.GetKeyDown(KeyCode.Escape)) {
                previousTimeScale = Time.timeScale == 0 ? previousTimeScale : Time.timeScale;
                ToggleCanvas();
            }
                
            if (Input.GetKeyDown(KeyCode.Space)) 
                _roundManager.StartRound();
            
        }
        private void ToggleCanvas() {
            bool enabled1 = mainMenu.enabled;
            enabled1 = !enabled1;
            mainMenu.enabled = enabled1;
            Time.timeScale = enabled1 ? 0 : previousTimeScale;
        }

        private static void ExitGame() {
            Application.Quit();
        }

        private static void GoToMain() {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    


    }
}
