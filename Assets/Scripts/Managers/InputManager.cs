using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance;
    
    // Debounce
    public float Debounce = 0.5f;
    private Debouncer _debouncer;
        
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public void Start() {
        _debouncer = new Debouncer(Debounce);
    }
    
    public void Update() {
        if (Input.GetMouseButtonDown(0) && _debouncer.CanExecute() && GameManager.Instance.State == GameState.Playing) {
            _debouncer.Reset();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2f);
            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
            pos.x = Mathf.Clamp(pos.x, -0.875f, 0.875f);
            pos.y = 1.5f;
            pos.z = Mathf.Clamp(pos.z, -0.875f, 0.875f);
            
            FruitManager.Instance.SpawnFirst(pos);
        }
    }
}