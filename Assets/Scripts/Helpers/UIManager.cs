using System.Linq;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;

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
        private char space;
        private char newLine;

        private void Start() {
            newLine = char.Parse("\n");
            space = char.Parse(" ");
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
            AbstractUpgradeContainer container = unit.abstractUpgradeContainer;
            textMesh.text =
                "Damage: " + unit.currentUpgrade.damage + "\n" +
                "Pierce: " + unit.currentUpgrade.pierce + "\n" +
                "P.Speed: " + unit.currentUpgrade.projectileSpeed + "\n" +
                "Range: " + unit.currentUpgrade.range + "\n" +
                "Atk/s: " + 1/(unit.currentUpgrade.secondsPerAttackModifier*unit.baseAttackSpeed) + "\n" +
                "Value: " + unit.GetSellValue();
            //usch helvete
            _firstTreeText.text = container.GetUpgrade(1) == null
                ? "Max Upgrades"
                : container.GetUpgrade(1).upgradeName + "\n" + container.GetUpgrade(1).price;

            _secondTreeText.text = container.GetUpgrade(2) == null
                ? "Max Upgrades"
                : container.GetUpgrade(2).upgradeName + "\n" + container.GetUpgrade(2).price;
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
            /*if (!round.Equals("Round 21")) return;
            /*shittoCanvas.enabled = true;
            foreach (Button btn in GetComponents<Button>()) { 
                btn.enabled = false;
            }*/
        }

        private AbstractUnit GetSelectedUnit() {
            AbstractUnit[] objects = _activeObjects.Units;
            return objects.FirstOrDefault(u => u.isSelected);
        }

        private Button GetButton(Button button) {
            return button == firstTree ? firstTree : secondTree;
        }

        public Text GetTextComponent(Button button) {
            return GetButton(button).GetComponentInChildren<Text>();
        }

        public string GetText(Button button) {
            string toReturn = GetButton(button).GetComponentInChildren<Text>().text;
            string[] splitted = toReturn.Split(newLine);
            for (int i = 0; i < splitted.Length; i++) 
                if (int.TryParse(splitted[i], out int toRemove))
                    splitted[i] = "";
            
            toReturn = splitted.Aggregate("", (current, s) => current + (s + " "));
            toReturn = toReturn.Trim();
            return toReturn;
        }

        public void ShowWin() {
            
        }

        private void OnDropDownValueChanged(Dropdown dropdown) {
            _unit = GetSelectedUnit();
            _unit.targetingStyle = dropdown.value;
        }
    
    }
}
