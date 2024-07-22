using UnityEngine;

namespace InventorySystemShowcase
{
    public class PlayerInput : MonoBehaviour
    {
        public Transform HeadTransform;

        [Range(1, 10)]
        public float MovementSpeed;
        [Range(1, 10)]
        public float MaxMovementSpeed;

        [Range(1, 100)]
        public float HeadSensitivity;
        [Range(50, 80)]
        public float MaxHeadRotation;

        private Rigidbody _rb;

        private float _headRotationX;
        private float _headRotationY;

        private bool _isMoveLeftRight;
        private bool _isMoveForwardBackward;
        private float _movementHorizontal;
        private float _movementVertical;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            ReadMouseInputAndAct();
            ReadKeyboardInputAndAct();
        }

        void FixedUpdate()
        {
            CheckMovementStateAndAct();
        }

        private void ReadMouseInputAndAct()
        {
            float movementX = Input.GetAxisRaw("Mouse X"); 
            float movementY = Input.GetAxis("Mouse Y"); 
            
            if (movementX != 0)
            {
                RotateHeadY(movementX);
            }
            if (movementY != 0)
            {
                RotateHeadX(movementY);
            }
        }

        private void RotateHeadY(float ratio)
        {
            float rotationY = 
                ratio *
                HeadSensitivity * 10.0f *
                Time.deltaTime;

            _headRotationY = (_headRotationY + rotationY) % 360.0f;

            transform.localRotation =
                Quaternion.Euler(
                    0.0f,
                    _headRotationY,
                    0.0f);
        }

        private void RotateHeadX(float ratio)
        {
            float rotationX = 
                -ratio *
                HeadSensitivity * 10.0f *
                Time.deltaTime;

            if (IsPotentialHeadRotationOutOfBounds(rotationX))
            {
                ClampHeadRotation();
            }
            else
            {
                _headRotationX = (_headRotationX + rotationX) % 360.0f;

                HeadTransform.transform.localRotation =
                    Quaternion.Euler(
                        _headRotationX,
                        0.0f,
                        0.0f);
            }

            //HeadTransform.transform.localRotation =
            //    Quaternion.Euler(
            //        rotationX,
            //        0.0f,
            //        0.0f);
        }

        private bool IsPotentialHeadRotationOutOfBounds(float rotation)
        {
            return
                Mathf.Abs((_headRotationX + rotation) % 360.0f) > MaxHeadRotation;
        }

        private void ClampHeadRotation()
        {
            float sign = 
                Mathf.Sign(_headRotationX);

            _headRotationX = sign * MaxHeadRotation;

            HeadTransform.transform.localRotation =
                Quaternion.Euler(
                    _headRotationX,
                    0.0f,
                    0.0f);
        }

        private void ReadKeyboardInputAndAct()
        {
            _movementHorizontal = Input.GetAxis("Horizontal"); 
            _movementVertical = Input.GetAxis("Vertical"); 

            if (_movementHorizontal != 0)
            {
                _isMoveLeftRight = true;
            }
            if (_movementVertical != 0)
            {
                _isMoveForwardBackward = true;
            }
        }

        private void MoveLeftRight(float ratio)
        {
            Vector3 force =
                ratio * 
                transform.right * 
                MovementSpeed * 100000.0f * 
                Time.fixedDeltaTime;
            _rb.AddForce(force);

            if (IsCurrentVelocityTooHigh())
            {
                ClampVelocity();
            }
        }

        private void MoveForwardBackward(float ratio)
        {
            Vector3 force =
                ratio * 
                transform.forward * 
                MovementSpeed * 100000.0f * 
                Time.fixedDeltaTime;
            _rb.AddForce(force);

            if (IsCurrentVelocityTooHigh())
            {
                ClampVelocity();
            }
        }

        private bool IsCurrentVelocityTooHigh()
        {
            return
                _rb.velocity.sqrMagnitude > 
                    MaxMovementSpeed * MaxMovementSpeed;
        }

        private void ClampVelocity()
        {
            _rb.velocity =
                MaxMovementSpeed *
                _rb.velocity.normalized;
        }

        private void CheckMovementStateAndAct()
        {
            if (_isMoveLeftRight)
            {
                MoveLeftRight(_movementHorizontal);
            }
            if (_isMoveForwardBackward)
            {
                MoveForwardBackward(_movementVertical);
            }
        }
    }
}
