using Units;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;


namespace Managers
{
    public class Economics : MonoBehaviour
    {

        [SerializeField]
        private Canvas mainCanvas;
        [SerializeField]
        private Canvas upgradeCanvas;
        [SerializeField]
        private Button goober, rocket, tack, magickan, mommo, upgrade1, upgrade2, sell;
        private Text _moneyText;
        public static Economics Instance;
        private int _cumulativeMoney;
        private Log _log;


        public void Awake() {
            _moneyText = mainCanvas.GetComponentInChildren<Text>();
            UpdateMoney(650);
            InitialiseButtons();
            _log = Log.Instance;
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
            if (!(moneyObject is IUpgrade)) 
                _log.Logger.Log(LogType.Log, $"3: Bought {moneyObject.name} for {moneyObject.GetBuyValue()}");
            
            UpdateMoney(-moneyObject.GetBuyValue());
        }

        private void Sell(AbstractUnit unit) {
            UpdateMoney(unit.GetSellValue());
            _log.Logger.Log(LogType.Log, $"3: Sold {unit.name} for {unit.GetSellValue()}");
            unit.OnCallDestroy();
            upgradeCanvas.GetComponent<UIManager>().HideMenu();
        }

        
        public void UpdateMoney(int addSubtract) {
            Money += addSubtract;
            _moneyText.text = Money + " €";
            _cumulativeMoney += addSubtract < 0 ? 0 : addSubtract;
        }

        /// <summary>
        /// Receive income.
        /// </summary>
        /// <param name="toReceive">is uint because this should only be used with positive values - can't receive negative income.</param>
        
        public void ReceiveIncome(int toReceive) {
            UpdateMoney(toReceive);
        }

        
        public int Money { get; private set; }

        public int CumulativeMoney => _cumulativeMoney;
        
    }
    
}
