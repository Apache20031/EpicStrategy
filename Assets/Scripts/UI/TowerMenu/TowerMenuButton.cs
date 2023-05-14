using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Events;
using Level;

public enum TowerMenuButtunState
{
    Default,
    Request,
    Lock
}

public enum TowerMenuButtonType
{
    Build,
    Cancel,
    Sell
}

public class TowerMenuButton : MonoBehaviour
{
    private event System.Action<TowerMenuButton, TowerMenuButtunState> ButtonClicked;

    [SerializeField] private TowerMenuButtonType _type;
    [SerializeField] private int _towerID = 0;
    [SerializeField] private GameObject _towerObject;
    [SerializeField] private int _price = 10;
    [SerializeField] private int _sellPrice = 10;
    [SerializeField] private float _constructionTime = 3;
    [Space(5)]
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _requestSprite;
    [SerializeField] private Sprite _lockSprite;

    public TowerMenuButtonType Type => _type;
    public GameObject TowerObject => _towerObject;
    public int Price => _price;
    public int SellPrice => _sellPrice;
    public float ConstructionTime => _constructionTime;

    private Image _image;
    private Button _button;
    private TowerMenuButtunState _state = TowerMenuButtunState.Default;

    public void Subscribe(System.Action<TowerMenuButton, TowerMenuButtunState> action) {
        ButtonClicked += action;
    }

    public void SetState(TowerMenuButtunState state) {
        _state = state;
        switch (state) {
            case TowerMenuButtunState.Default:
                _image.sprite = _defaultSprite;
                _button.interactable = true;
                break;
            case TowerMenuButtunState.Request:
                _image.sprite = _requestSprite;
                _button.interactable = true;
                break;
            case TowerMenuButtunState.Lock:
                _image.sprite = _lockSprite;
                _button.interactable = false;
                break;
        }
    }

    private void Awake() {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void Start() {
        Observer.Subscribe<MoneyChangeEvent>(UpdateCostAvailable);
        _button.onClick.AddListener(OnMouseButtonClickHandler);
        if (LevelManager.LevelData.AvailableTowers.GetTowerAvailable(_towerID)) {
            SetState(TowerMenuButtunState.Lock);
        }
        
    }

    private void OnDestroy() {
        Observer.Unsubscribe<MoneyChangeEvent>(UpdateCostAvailable);
    }

    private void UpdateCostAvailable(object sender, MoneyChangeEvent moneyData) {
        _button.interactable = moneyData.money > _price;
    }

    private void OnMouseButtonClickHandler() {
        ButtonClicked?.Invoke(this, _state);
        switch (_state) {
            case TowerMenuButtunState.Default:
                SetState(TowerMenuButtunState.Request);
                break;
            case TowerMenuButtunState.Request:
                SetState(TowerMenuButtunState.Default);
                break;
        }
    }

    #region Editor
    #if UNITY_EDITOR
    [CustomEditor(typeof(TowerMenuButton))]
    private class TowerMenuButtonEditor : Editor 
    {
        SerializedProperty type;
        SerializedProperty towerID;
        SerializedProperty towerObject;
        SerializedProperty price;
        SerializedProperty sellPrice;
        SerializedProperty constructionTime;
        SerializedProperty defaultSprite;
        SerializedProperty requestSprite;
        SerializedProperty lockSprite;

        public override void OnInspectorGUI() {
            TowerMenuButton towerMenuButton = (TowerMenuButton)target;
            if (towerMenuButton == null) return;

            serializedObject.Update();

            EditorGUILayout.PropertyField(type);
            if (towerMenuButton._type == TowerMenuButtonType.Build) {
                EditorGUILayout.PropertyField(towerID);
                EditorGUILayout.PropertyField(towerObject);
                EditorGUILayout.PropertyField(price);
                EditorGUILayout.PropertyField(sellPrice);
                EditorGUILayout.PropertyField(constructionTime);
            }
            EditorGUILayout.PropertyField(defaultSprite);
            EditorGUILayout.PropertyField(requestSprite);
            EditorGUILayout.PropertyField(lockSprite);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable() {
            type = serializedObject.FindProperty("_type");
            towerID = serializedObject.FindProperty("_towerID");
            towerObject = serializedObject.FindProperty("_towerObject");
            price = serializedObject.FindProperty("_price");
            sellPrice = serializedObject.FindProperty("_sellPrice");
            constructionTime = serializedObject.FindProperty("_constructionTime");
            defaultSprite = serializedObject.FindProperty("_defaultSprite");
            requestSprite = serializedObject.FindProperty("_requestSprite");
            lockSprite = serializedObject.FindProperty("_lockSprite");
        }
    }
    #endif
    #endregion
}
