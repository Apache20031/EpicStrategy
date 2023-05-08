using UnityEngine;
using Events;

namespace Events
{
    public class PauseEvent { public bool pause; }
}

namespace Pause
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] KeyCode _pauseButton = KeyCode.Space;
        [SerializeField] private bool _paused = false;

        private void Update() {
            if (Input.GetKeyDown(_pauseButton)) {
                _paused = !_paused;
                SetPause(_paused);
            }
        }
        private void SetPause(bool pause) {
            Observer.Post(this, new PauseEvent { pause = pause });
        }
    }

}
