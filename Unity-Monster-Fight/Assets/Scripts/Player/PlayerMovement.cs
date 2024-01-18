using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterSo _playerInfo;
        private Rigidbody2D _rigidbody2D;
        private float _runSpeed;
        private Vector2 _direction = Vector2.left;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _runSpeed = _playerInfo.GetRandomRunSpeed();
        }

        private void Update()
        {
            _rigidbody2D.velocity = _direction * _runSpeed;
        }
    }
}
