using System;
using UnityEngine;


namespace CodeBase
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 0.001f;
        [SerializeField] private InputService _inputService;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _jampForce = 250f;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float translation = Input.GetAxis("Horizontal") * _movementSpeed;
            
            transform.Translate(translation, 0, 0 );

            if (Input.GetButtonDown("Jump"))
            {
                _rigidbody2D.AddForce(Vector2.up * _jampForce);
                Debug.Log("jump");
            }

            
        }
    }

    internal class InputService
    {
        
    }
}

