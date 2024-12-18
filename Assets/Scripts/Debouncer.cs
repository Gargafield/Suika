
using UnityEngine;

public class Debouncer {
    private float _debounceTime;
    private float _lastTime;

    public Debouncer(float debounceTime) {
        _debounceTime = debounceTime;
    }

    public bool CanExecute() {
        if (Time.time - _lastTime > _debounceTime) {
            return true;
        }

        return false;
    }
    
    public void Reset() {
        _lastTime = Time.time;
    }
}