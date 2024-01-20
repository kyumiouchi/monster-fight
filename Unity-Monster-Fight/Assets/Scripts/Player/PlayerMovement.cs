using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private float _runSpeed = 0;
        private Vector2 _direction = Vector2.left;
        private bool _canStartRun = false;
        public bool IsRunning => _canStartRun;
        

        public void SetRunSpeed(float speed)
        {
            _runSpeed = speed;
        }

        public void StartRun()
        {
            _canStartRun = true;
        }
        
        public void StopRun()
        {
            _canStartRun = false;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!_canStartRun) return;
            _rigidbody2D.velocity = _runSpeed * _direction;
        }
    }
}
