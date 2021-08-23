using System.Linq;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class UIManager : MonoBehaviour {
        
        public Canvas canvas;
        public Dropdown dropdown;
        private ActiveObjectsTracker _activeObjects;
        private AbstractUnit _unit;
        public Button firstTree;
        public Button secondTree;
        public TextMeshProUGUI displayStats;
        private Text _firstTreeText;
        private Text _secondTreeText;
        public TextMeshProUGUI displayRound;
        private Canvas shittoCanvas;

        private void Start() {
            shittoCanvas = GameObject.FindWithTag("Panel").GetComponent<Canvas>();
            shittoCanvas.enabled = false;
            _activeObjects = ActiveObjectsTracker.Instance;
            _firstTreeText = firstTree.GetComponentInChildren<Text>();
            _secondTreeText = secondTree.GetComponentInChildren<Text>();
            dropdown.onValueChanged.AddListener(delegate {OnDropDownValueChanged(dropdown);});
            if(canvas.name != "MainCanvas") {
                canvas.enabled = false;
            }
        }
    
        public void DisplayStats(AbstractUnit unit, TextMeshProUGUI textMesh) {
            if (unit == null ) return;
            textMesh.text =
                "Damage: " + unit.currentUpgrade.damage + "\n" +
                "Pierce: " + unit.currentUpgrade.pierce + "\n" +
                "P.Speed: " + unit.currentUpgrade.projectileSpeed + "\n" +
                "Range: " + unit.currentUpgrade.range + "\n" +
                "Atk/s: " + 1/(unit.currentUpgrade.secondsPerAttackModifier*unit.baseAttackSpeed) + "\n" +
                "Value: " + unit.GetSellValue();
            _firstTreeText.text = unit.abstractUpgradeContainer.GetKey(1);
            _secondTreeText.text = unit.abstractUpgradeContainer.GetKey(2);
        }
    
        public void ShowMenu(AbstractUnit unit) {
            canvas.enabled = true;
            dropdown.SetValueWithoutNotify(unit.targetingStyle);
        }

        public void HideMenu() {
            canvas.enabled = false;
        }

        public void DisplayRound(string round) {
            displayRound.text = round;
            if (!round.Equals("Round 21")) return;
            //shittoCanvas.enabled = true;
            //foreach (Button btn in GetComponents<Button>()) {
              //  btn.enabled = false;
            //}
        }

        private AbstractUnit GetSelectedUnit() {
            AbstractUnit[] objects = _activeObjects.units;
            return objects.FirstOrDefault(u => u.isSelected);
        }

        private Button GetButton(Button button) {
            return button == firstTree ? firstTree : secondTree;
        }

        public Text GetText(Button button) {
            return GetButton(button).GetComponentInChildren<Text>();
        }

        public void ShowWin() {
            
        }

        private void OnDropDownValueChanged(Dropdown dropdown) {
            _unit = GetSelectedUnit();
            _unit.targetingStyle = dropdown.value;
        }
    
    }
}
