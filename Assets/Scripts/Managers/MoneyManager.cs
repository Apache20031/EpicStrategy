using TMPro;
using UnityEngine;
using Events;

namespace Events
{
    public class MoneyChangeEvent { public int money; }
}

namespace Money
{
    public class MoneyManager : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField] public int money = 0;
        [SerializeField] public int _startMoney = 10;
        [SerializeField] private TextMeshProUGUI _moneyText;

        public static int Money => _instance.money;

        private static MoneyManager _instance;

        public static void AddMoney(int money) {
            _instance.money += money;
            Observer.Post(_instance, new MoneyChangeEvent { money = money });
            _instance.UpdateText();
        }
        public static void SpendMoney(int money) {
            _instance.money -= money;
            Observer.Post(_instance, new MoneyChangeEvent { money = money });
            _instance.UpdateText();
        }

        public void UpdateText() {
            if (_moneyText == null) {
                return;
            }
            _moneyText.text = money.ToString();
        }

        private void Awake() {
            if (_instance == null) {
                _instance = this;
                _instance.money = _instance._startMoney;
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}

