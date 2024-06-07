using UnityEngine;

namespace Airplane
{
    [RequireComponent(typeof(Rigidbody))]
    public class AirplaneMovement : MonoBehaviour
    {

        [SerializeField] private AirplaneConfiguration airplaneConfiguration;

        #region Private Variables

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float currentSpeed;
        
        private float _maxSpeed;
        private float _minSpeed;
        private float _speedMultiplier;
        private float _currentYawSpeed;
        private float _currentPitchSpeed;
        private float _currentRollSpeed;
        private bool _isNearGround;
        private bool _isNearGroundLeft;
        private bool _isNearGroundRight;
        private bool _turboOverheat;
        private float _turboHeat;
        
        private Rigidbody _rb;

        // Input Variables
        private float _inputH;
        private float _inputV;
        private bool _inputTurbo;
        private bool _inputYawLeft;
        private bool _inputYawRight;
        
        #endregion

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            _minSpeed = 50f;
            _maxSpeed = 100f;
            currentSpeed = airplaneConfiguration.defaultSpeed;
            _speedMultiplier = 1;
        }

        private void Update()
        {
            Controls();
            Movement();
            SidewaysForceCalculation();
            DampVelocities();
            DampRotations();
            AlignWithGround();
            

        }

        private void FixedUpdate()
        {
            Acceleration();
        }
        //Rotate inputs

        private void Controls()
        {
        
            _inputH = Input.GetAxis("Horizontal");
            _inputV = Input.GetAxis("Vertical");

            //Yaw axis inputs
            _inputYawLeft = Input.GetKey(KeyCode.Q);
            _inputYawRight = Input.GetKey(KeyCode.E);

            //Turbo
            _inputTurbo = Input.GetKey(KeyCode.LeftShift);
            
        }

        #region Fly State
        
        private void SidewaysForceCalculation()
        {
            float multiplierXRot = airplaneConfiguration.sidewaysMovement * airplaneConfiguration.sidewaysMovementXRot;
            float multiplierYRot = airplaneConfiguration.sidewaysMovement * airplaneConfiguration.sidewaysMovementYRot;
            float multiplierYPosition = airplaneConfiguration.sidewaysMovement * airplaneConfiguration.sidewaysMovementYPosition;

            // Right side 
            if (transform.localEulerAngles.z is > 270f and < 360f)
            {
                float angle = (transform.localEulerAngles.z - 270f) / (360f - 270f);
                float invert = 1f - angle;

                transform.Rotate(Vector3.up * (invert * multiplierYRot * Time.deltaTime));
                transform.Rotate(Vector3.right * (-invert * multiplierXRot * _currentPitchSpeed * Time.deltaTime));

                transform.Translate(transform.up * (invert * multiplierYPosition * Time.deltaTime));
            }

            // Left side
            if (transform.localEulerAngles.z is > 0f and < 90f)
            {
                float angle = transform.localEulerAngles.z / 90f;

                transform.Rotate(-Vector3.up * (angle * multiplierYRot * Time.deltaTime));
                transform.Rotate(Vector3.right * (-angle * multiplierXRot * _currentPitchSpeed * Time.deltaTime));

                transform.Translate(transform.up * (angle * multiplierYPosition * Time.deltaTime));
            }

            // Right side down
            if (transform.localEulerAngles.z is > 90f and < 180f)
            {
                float angle = (transform.localEulerAngles.z - 90f) / (180f - 90f);
                float invert = 1f - angle;

                transform.Translate(transform.up * (invert * multiplierYPosition * Time.deltaTime));
                transform.Rotate(Vector3.right * (-invert * multiplierXRot * _currentPitchSpeed * Time.deltaTime));
            }

            // Left side down
            if (transform.localEulerAngles.z is > 180f and < 270f)
            {
                float angle = (transform.localEulerAngles.z - 180f) / (270f - 180f);

                transform.Translate(transform.up * (angle * multiplierYPosition * Time.deltaTime));
                transform.Rotate(Vector3.right * (-angle * multiplierXRot * _currentPitchSpeed * Time.deltaTime));
            }
        }

        public void Acceleration()
        {
            if (Input.GetMouseButton(0) && currentSpeed < _maxSpeed)
            {
                currentSpeed += airplaneConfiguration.accelerating * Time.deltaTime;
                // Asegurarse de que la velocidad no exceda _maxSpeed
                currentSpeed = Mathf.Min(currentSpeed, _maxSpeed);
            }

            if (Input.GetMouseButton(1) && currentSpeed > _minSpeed)
            {
                currentSpeed -= airplaneConfiguration.accelerating * Time.deltaTime;
                // Asegurarse de que la velocidad no caiga por debajo de _minSpeed
                currentSpeed = Mathf.Max(currentSpeed, _minSpeed);
            }
        }

private void Movement()
        {
            // Move forward
            _rb.velocity = transform.forward * currentSpeed;

            // Rotate airplane by inputs
            transform.Rotate(Vector3.forward * (-_inputH * _currentRollSpeed * Time.deltaTime));
            transform.Rotate(Vector3.right * (_inputV * _currentPitchSpeed * Time.deltaTime));
            
            // Rotate airplane by inputs
            if ((_inputH > 0 && !_isNearGroundLeft) || (_inputH < 0 && !_isNearGroundRight))
            {
                transform.Rotate(Vector3.forward * (-_inputH * _currentRollSpeed * Time.deltaTime));
            }

            if (!_isNearGround || _inputV < 0)
            {
                transform.Rotate(Vector3.right * (_inputV * _currentPitchSpeed * Time.deltaTime));
            }

            // Rotate yaw
            if (_inputYawRight)
            {
                transform.Rotate(Vector3.up * (_currentYawSpeed * Time.deltaTime));
            }
            else if (_inputYawLeft)
            {
                transform.Rotate(-Vector3.up * (_currentYawSpeed * Time.deltaTime));
            }

           

            // Turbo
            if (_inputTurbo && !_turboOverheat)
            {
                // Turbo overheating
                if(_turboHeat > 100f)
                {
                    _turboHeat = 100f;
                    _turboOverheat = true;
                }
                else
                {       
                    // Add turbo heat
                }

                // Set speed to turbo speed and rotation to turbo values
                _maxSpeed = airplaneConfiguration.turboSpeed;

                _currentYawSpeed = airplaneConfiguration.yawSpeed * airplaneConfiguration.yawTurboMultiplier;
                _currentPitchSpeed = airplaneConfiguration.pitchSpeed * airplaneConfiguration.pitchTurboMultiplier;
                _currentRollSpeed = airplaneConfiguration.rollSpeed * airplaneConfiguration.rollTurboMultiplier;
            }
            else
            {
                // Turbo cooling down
                if(_turboHeat > 0f)
                {
                    _turboHeat -= Time.deltaTime * airplaneConfiguration.turboCooldownSpeed;
                }
                else
                {
                    _turboHeat = 0f;
                }

                // Turbo cooldown
                if (_turboOverheat)
                {
                   if(_turboHeat <= airplaneConfiguration.turboOverheatOver)
                   {
                        _turboOverheat = false;
                   }
                }

                // Speed and rotation normal
                _maxSpeed = airplaneConfiguration.defaultSpeed * _speedMultiplier;

                _currentYawSpeed = airplaneConfiguration.yawSpeed;
                _currentPitchSpeed = airplaneConfiguration.pitchSpeed;
                _currentRollSpeed = airplaneConfiguration.rollSpeed;
            }
        }

        private void DampVelocities()
        {
            _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, Time.deltaTime * airplaneConfiguration.dampingSpeed);
            _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, Time.deltaTime * airplaneConfiguration.linearDampingSpeed);
        }

        private void DampRotations()
        {
            _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, Time.deltaTime * airplaneConfiguration.dampingSpeed);
            _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, Time.deltaTime * airplaneConfiguration.linearDampingSpeed);

            // Dampen the rotation when no input is given
            if (_inputH == 0 && _inputV == 0)
            {
                // Calculate the amount to dampen by
                float dampFactor = 1 - (airplaneConfiguration.dampingSpeed * Time.deltaTime);

                // Dampen the roll
                if (Mathf.Abs(transform.localEulerAngles.z) > 0.1f)
                {
                    float newZ = transform.localEulerAngles.z * dampFactor;
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, newZ);
                }

                // Dampen the pitch
                if (Mathf.Abs(transform.localEulerAngles.x) > 0.1f)
                {
                    float newX = transform.localEulerAngles.x * dampFactor;
                    transform.localEulerAngles = new Vector3(newX, transform.localEulerAngles.y, transform.localEulerAngles.z);
                }
            }
        }

        private void AlignWithGround()
        {
            //Ray-Cast Ground
            _isNearGround = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit,
                airplaneConfiguration.groundCheckDistance, groundMask);

            if (_isNearGround)
            {
                Vector3 desiredUp = hit.normal;
                Quaternion targetRotaton = Quaternion.FromToRotation(transform.up, desiredUp) * transform.rotation;
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotaton, Time.deltaTime * airplaneConfiguration.alignmentSpeed);
            }
            
            // Ray-cast Side Way
            Vector3 leftRayDirection = Quaternion.Euler(0, transform.eulerAngles.y, airplaneConfiguration.maxAngleSide) * Vector3.down;
            Vector3 rightRayDirection = Quaternion.Euler(0, transform.eulerAngles.y, -airplaneConfiguration.maxAngleSide) * Vector3.down;

            Vector3 leftRayOrigin = transform.position + transform.right * airplaneConfiguration.sideRayOffset;
            Vector3 rightRayOrigin = transform.position - transform.right * airplaneConfiguration.sideRayOffset;

            _isNearGroundLeft = Physics.Raycast(leftRayOrigin, leftRayDirection, out hit, airplaneConfiguration.groundCheckDistanceSide, groundMask);
            _isNearGroundRight = Physics.Raycast(rightRayOrigin, rightRayDirection, out hit, airplaneConfiguration.groundCheckDistanceSide, groundMask);
        }

        #endregion
        
#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            // Draw ground Gizmo
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * airplaneConfiguration.groundCheckDistance);

            // Draw Sphere if check collision.
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, airplaneConfiguration.groundCheckDistance, groundMask))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
            
            // Draw Side-way Gizmo
            Gizmos.color = Color.blue;

            Vector3 leftRayDirection = Quaternion.Euler(0, transform.eulerAngles.y, airplaneConfiguration.maxAngleSide) * Vector3.down;
            Vector3 rightRayDirection = Quaternion.Euler(0, transform.eulerAngles.y, -airplaneConfiguration.maxAngleSide) * Vector3.down;

            Vector3 leftRayOrigin = transform.position + transform.right * airplaneConfiguration.sideRayOffset;
            Vector3 rightRayOrigin = transform.position - transform.right * airplaneConfiguration.sideRayOffset;

            Gizmos.DrawLine(leftRayOrigin, leftRayOrigin + leftRayDirection * airplaneConfiguration.groundCheckDistanceSide);
            Gizmos.DrawLine(rightRayOrigin, rightRayOrigin + rightRayDirection * airplaneConfiguration.groundCheckDistanceSide);

            // Draw Sphere if check collision.
            if (Physics.Raycast(leftRayOrigin, leftRayDirection, out hit, airplaneConfiguration.groundCheckDistanceSide, groundMask))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
    
            if (Physics.Raycast(rightRayOrigin, rightRayDirection, out hit, airplaneConfiguration.groundCheckDistanceSide, groundMask))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
        }
        
#endif
        
    }
}