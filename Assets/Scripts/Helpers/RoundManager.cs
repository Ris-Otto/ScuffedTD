using Units;
using UnityEngine;

namespace Helpers
{
    public class RoundManager : MonoBehaviour
    {
        private UIManager ui;
        private int CurrentRound = 1;
        private RoundInformation _roundInformation;
        private bool canStartRound;


        private void Awake() {
            ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        }

        private void Start() {
            _roundInformation = RoundInformation.Instance;
            canStartRound = true;
        }

        public void SetCanStartRound(bool can) {
            canStartRound = can;
        }

        public void StartRound() {
            if (!canStartRound) {
                Debug.Log("Round still going on.");
                return;
            }
            ui.DisplayRound("Round " + CurrentRound);
            CurrentRound++;
            _roundInformation.RoundStart(CurrentRound);
            Debug.Log("Called StartRound() from RoundManager");
            SetCanStartRound(false);
        }

    }
}
