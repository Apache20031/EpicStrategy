using TMPro;
using UnityEngine;

namespace Money
{
    public class MoneyManager : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField] public int money = 0;
        [SerializeField] public int _startMoney = 10;
        [SerializeField] private TextMeshProUGUI _moneyText;

        public int Money => _instance.money;

        private static MoneyManager _instance;

        public static void AddMoney(int money) {
            _instance.money += money;
            _instance.UpdateText();
        }
        public static void SpendMoney(int money) {
            _instance.money -= money;
            _instance.UpdateText();
        }

        public void UpdateText() {
            if (_moneyText != null) {
                _moneyText.text = money.ToString();
            }
        }

        private void Awake() {
            _instance = FindObjectOfType<MoneyManager>();
            _instance.money = _instance._startMoney;
        }
    }
}

