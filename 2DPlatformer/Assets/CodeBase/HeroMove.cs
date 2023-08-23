using System;
using System.Collections;
using UnityEngine;


namespace CodeBase
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 0.001f;
        [SerializeField] private float _attackAnimationTime = 1f;
        [SerializeField] private float _jampForce = 250f;

        [SerializeField] private GameObject _weaponGameObject;
        
        private Rigidbody2D _rigidbody2D;
        private bool _isAttaking;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_isAttaking)
                return;


            if (Input.GetButton("Fire1")) 
                StartCoroutine(Attack());


            if (Input.GetButtonDown("Jump"))
            {
                _rigidbody2D.AddForce(Vector2.up * _jampForce);
                Debug.Log("jump");
            }

            float translation = Input.GetAxis("Horizontal") * _movementSpeed;
            
            transform.Translate(translation, 0, 0 );
        }

        private IEnumerator Attack()
        {
            _isAttaking = true;
            
            yield return new WaitForEndOfFrame();

            var inputAxisHorizontal =  Input.GetAxis("Horizontal");
            var axisVertical = + Input.GetAxis("Vertical");

            if (inputAxisHorizontal > 0.5f)
            {
                if (axisVertical > 0.5f)
                {
                    Debug.Log("Attack UP Forward");
                    StartCoroutine(AttackAnimation(Vector3.forward, -45f));
                    yield break;
                }

                if (axisVertical < -0.5f)
                {
                    Debug.Log("Attack Down Forward");
                    StartCoroutine(AttackAnimation(Vector3.forward, -125f));
                    yield break;
                }
                
                Debug.Log("Attack Forward Forward");
                StartCoroutine(AttackAnimation(Vector3.forward, -90f));
                yield break;
            }
            
            if (inputAxisHorizontal < -0.5f)
            {
                if (axisVertical > 0.4f)
                {
                    Debug.Log("Attack UP Back");
                    StartCoroutine(AttackAnimation(Vector3.forward, 45f));
                    yield break;
                }
                
                if (axisVertical < -0.5f)
                {
                    Debug.Log("Attack Down Back");
                    StartCoroutine(AttackAnimation(Vector3.forward, 125f));
                    yield break;
                }
                
                Debug.Log("Attack Back Back");
                StartCoroutine(AttackAnimation(Vector3.forward, 90f));
                yield break;
            }

            if (axisVertical < -0.2f)
            {
                Debug.Log("Attack Down");
                StartCoroutine(AttackAnimation(Vector3.forward, 180f));
                yield break;
            }

            if (axisVertical > 0.2f)
            {
                Debug.Log("Up");
                _weaponGameObject.transform.localPosition += Vector3.up;
                yield return new WaitForSeconds(_attackAnimationTime);
                _weaponGameObject.transform.localPosition -= Vector3.up;
                _isAttaking = false;
                yield break;
            }
            
            
            Debug.Log("Attack Forward Forward");
            StartCoroutine(AttackAnimation(Vector3.forward, -90f));
            //_isAttaking = false;
        }

        private IEnumerator AttackAnimation(Vector3 direction, float angle )
        {
            _weaponGameObject.transform.Rotate(direction, angle);
            yield return new WaitForSeconds(_attackAnimationTime);
            _weaponGameObject.transform.Rotate(direction, - angle);
            _isAttaking = false;
        }
    }
}

