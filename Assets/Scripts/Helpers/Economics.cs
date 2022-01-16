using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class Economics : MonoBehaviour
    {

        public Canvas mainCanvas;
        public Canvas upgradeCanvas;
        public Button goober, rocket, tack, magickan, mommo, upgrade1, upgrade2, sell;
        private Text _moneyText;
        public static Economics Instance; 

        public void Awake() {
            _moneyText = mainCanvas.GetComponentInChildren<Text>();
            UpdateMoney(2000);
            InitialiseButtons();
            if (Instance == null) Instance = this;
        }

        private void InitialiseButtons() {
            goober.onClick.AddListener(() => Buy(goober.GetComponent<UnitManager>().Execute()));
            rocket.onClick.AddListener(() => Buy(rocket.GetComponent<UnitManager>().Execute()));
            tack.onClick.AddListener(() => Buy(tack.GetComponent<UnitManager>().Execute()));
            magickan.onClick.AddListener(() => Buy(magickan.GetComponent<UnitManager>().Execute()));
            mommo.onClick.AddListener(() => Buy(mommo.GetComponent<UnitManager>().Execute()));
            sell.onClick.AddListener(() => Sell(sell.GetComponent<UnitManager>().DestroyUnit()));
            upgrade1.onClick.AddListener(() => Buy(upgrade1.GetComponent<UpgradeManager>().OnUpgradeClicked(Money, upgrade1)));
            upgrade2.onClick.AddListener(() => Buy(upgrade2.GetComponent<UpgradeManager>().OnUpgradeClicked(Money, upgrade2)));
        }

        private void Buy(IMoneyObject moneyObject) {
            if(moneyObject == null) return;
            UpdateMoney(-moneyObject.GetBuyValue());
        }

        private void Sell(AbstractUnit unit) {
            UpdateMoney(unit.GetSellValue());
            unit.OnCallDestroy();
            upgradeCanvas.GetComponent<UIManager>().HideMenu();
        }

        public void UpdateMoney(int addSubtract) {
            Money += addSubtract;
            _moneyText.text = Money + " €";
        }

        /// <summary>
        /// Receive income.
        /// </summary>
        /// <param name="toReceive">is uint because this should only be used with positive values - can't receive negative income.</param>
        public void ReceiveIncome(int toReceive) {
            UpdateMoney(toReceive);
        }

        public int Money { get; private set; }
    }
}
