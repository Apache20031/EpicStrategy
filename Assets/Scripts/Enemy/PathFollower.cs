using UnityEngine;
using Events;

[System.Serializable]
public class PathFollower
{
    private event System.Action<object> PathCompleted;

    [SerializeField] private EnemyPath _path;
    [SerializeField] private float _speed = 2;

    private GameObject _movableObject;
    private bool _pause = false;
    private bool _stop = false;
    private float _slowdown = 0;

    private PathPoint _nextPathPoint;
    private PathPoint _currentPathPoint;
    private int _nextPathPointIndex = 0;
    private float _interpolateValue;
    private float _pathDistance;

    public void Awake() {
        Observer.Subscribe<PauseEvent>(Pause);
        _movableObject.transform.position = _path.Path[0].Position;
        NextPoint();
    }
    public void OnDestroy() {
        Observer.Unsubscribe<PauseEvent>(Pause);
    }

    public void Subscribe(System.Action<object> action) {
        PathCompleted += action;
    }

    public void SetFollower(GameObject gameObject) {
        _movableObject = gameObject;
    }

    public void Update() {
        if (_pause || _stop) {
            return;
        }
        switch (_currentPathPoint.Type) {
            case PathPointType.Move:
                MovementUpdate();
                break;
            case PathPointType.Teleport:
                Teleport();
                break;
        }
    }

    private void MovementUpdate() {
        _interpolateValue += Time.deltaTime * (_speed * (1 - _slowdown)) / _pathDistance;
        _movableObject.transform.position = Vector3.Lerp(_currentPathPoint.Position, _nextPathPoint.Position, _interpolateValue);
        if (_movableObject.transform.position == _nextPathPoint.Position) {
            if (_path.Path[_nextPathPointIndex].Type == PathPointType.End) {
                End();
            }
            else {
                NextPoint();
            }
        }
    }

    private void Teleport() {
        NextPoint();
        if (_currentPathPoint.Type == PathPointType.Teleport) {
            Teleport();
        }
    }

    private void End() {
        _stop = true;
        PathCompleted?.Invoke(this);
    }

    private void NextPoint() {
        _interpolateValue = 0;
        _nextPathPointIndex += 1;
        _nextPathPoint = _path.Path[_nextPathPointIndex];
        _currentPathPoint = _path.Path[_nextPathPointIndex - 1];
        _pathDistance = Vector3.Distance(_currentPathPoint.Position, _nextPathPoint.Position);
        _movableObject.transform.LookAt(new Vector3(_nextPathPoint.Position.x, _movableObject.transform.position.y, _nextPathPoint.Position.z));
    }

    private void Pause(object sender, PauseEvent eventData) {
        _pause = eventData.pause;
    }
}
