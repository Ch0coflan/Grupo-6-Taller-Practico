using UnityEngine;

namespace Airplane
{
    [RequireComponent(typeof(Rigidbody))]
    public class AirplaneMovement : MonoBehaviour
    {

        #region Serialized Variables

        [Header("Rotating speeds")]
        [Range(5f, 500f)]
        [SerializeField] private float yawSpeed = 50f;

        [Range(5f, 500f)]
        [SerializeField] private float pitchSpeed = 100f;

        [Range(5f, 500f)]
        [SerializeField] private float rollSpeed = 200f;

        [Header("Rotating speeds multipliers when turbo is used")]

        [SerializeField] private float yawTurboMultiplier = 0.3f;

        [Range(0.1f, 5f)]
        [SerializeField] private float pitchTurboMultiplier = 0.5f;

        [Range(0.1f, 5f)]
        [SerializeField] private float rollTurboMultiplier = 1f;

        [Header("Moving speed")]
        [Range(5f, 1000f)]
        [SerializeField] private float defaultSpeed = 150f;

        [Range(10f, 50f)]
        [SerializeField] private float turboSpeed = 20f;

        [Range(0.1f, 50f)]
        [SerializeField] private float accelerating = 10f;

        [Header("Damping settings")]
        [Range(0f, 5f)]
        [SerializeField] private float dampingSpeed = 2f;

        [Range(0f, 5f)]
        [SerializeField] private float linearDampingSpeed = 2f;

        [Header("Turbo settings")]
        [Range(0f, 100f)]
        [SerializeField] private float turboHeatingSpeed;

        [Range(0f, 100f)]
        [SerializeField] private float turboCooldownSpeed;

        [Header("Turbo heat values")]
        [Tooltip("Real-time information about the turbo's current temperature (do not change in the editor)")]
        [Range(0f, 100f)]
        [SerializeField] private float turboHeat;

        [Tooltip("You can set this to determine when the turbo should cease overheating and become operational again")]
        [Range(0f, 100f)]
        [SerializeField] private float turboOverheatOver;

        [SerializeField] private bool turboOverheat;

        [Header("Sideways force")]
        [Range(0.1f, 15f)]
        [SerializeField] private float sidewaysMovement = 15f;

        [Range(0.001f, 0.05f)]
        [SerializeField] private float sidewaysMovementXRot = 0.012f;

        [Range(0.1f, 5f)]
        [SerializeField] private float sidewaysMovementYRot = 1.5f;

        [Range(-1, 1f)]
        [SerializeField] private float sidewaysMovementYPosition = 0.1f;

        [Header("Ground Detection")]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float groundCheckDistance = 10f;

        [SerializeField] private float sideRayOffset = 5f;

        [SerializeField] private float groundCheckDistanceSide = 8f;

        [Range(0f, 180f)]
        [SerializeField] private float maxAngleSide = 10f;

        [Range(0f, 5f)]
        [SerializeField] private float alignmentSpeed = 2f;

        #endregion

        #region Private Variables

        private float _maxSpeed;
        private float _minSpeed;
        private float _speedMultiplier;
        private float _currentYawSpeed;
        private float _currentPitchSpeed;
        private float _currentRollSpeed;
        [SerializeField] private float _currentSpeed;
        private bool _isNearGround;
        private bool _isNearGroundLeft;
        private bool _isNearGroundRight;

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
            _currentSpeed = defaultSpeed;
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
            aceleration();
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
            float multiplierXRot = sidewaysMovement * sidewaysMovementXRot;
            float multiplierYRot = sidewaysMovement * sidewaysMovementYRot;
            float multiplierYPosition = sidewaysMovement * sidewaysMovementYPosition;

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

        public void aceleration()
        {
            if (Input.GetMouseButton(0) && _currentSpeed < _maxSpeed)
            {
                _currentSpeed += accelerating * Time.deltaTime;
                // Asegurarse de que la velocidad no exceda _maxSpeed
                _currentSpeed = Mathf.Min(_currentSpeed, _maxSpeed);
            }

            if (Input.GetMouseButton(1) && _currentSpeed > _minSpeed)
            {
                _currentSpeed -= accelerating * Time.deltaTime;
                // Asegurarse de que la velocidad no caiga por debajo de _minSpeed
                _currentSpeed = Mathf.Max(_currentSpeed, _minSpeed);
            }
        }

        private void Movement()
        {
            // Move forward
            _rb.velocity = transform.forward * _currentSpeed;

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
            if (_inputTurbo && !turboOverheat)
            {
                // Turbo overheating
                if (turboHeat > 100f)
                {
                    turboHeat = 100f;
                    turboOverheat = true;
                }
                else
                {
                    // Add turbo heat
                    turboHeat += Time.deltaTime * turboHeatingSpeed;
                }

                // Set speed to turbo speed and rotation to turbo values
                _maxSpeed = turboSpeed;

                _currentYawSpeed = yawSpeed * yawTurboMultiplier;
                _currentPitchSpeed = pitchSpeed * pitchTurboMultiplier;
                _currentRollSpeed = rollSpeed * rollTurboMultiplier;
            }
            else
            {
                // Turbo cooling down
                if (turboHeat > 0f)
                {
                    turboHeat -= Time.deltaTime * turboCooldownSpeed;
                }
                else
                {
                    turboHeat = 0f;
                }

                // Turbo cooldown
                if (turboOverheat)
                {
                    if (turboHeat <= turboOverheatOver)
                    {
                        turboOverheat = false;
                    }
                }

                // Speed and rotation normal
                _maxSpeed = defaultSpeed * _speedMultiplier;

                _currentYawSpeed = yawSpeed;
                _currentPitchSpeed = pitchSpeed;
                _currentRollSpeed = rollSpeed;
            }
        }

        private void DampVelocities()
        {
            _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, Time.deltaTime * dampingSpeed);
            _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, Time.deltaTime * linearDampingSpeed);
        }

        private void DampRotations()
        {
            _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, Time.deltaTime * dampingSpeed);
            _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, Time.deltaTime * linearDampingSpeed);

            // Dampen the rotation when no input is given
            if (_inputH == 0 && _inputV == 0)
            {
                // Calculate the amount to dampen by
                float dampFactor = 1 - (dampingSpeed * Time.deltaTime);

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
                groundCheckDistance, groundMask);

            if (_isNearGround)
            {
                Vector3 desiredUp = hit.normal;
                Quaternion targetRotaton = Quaternion.FromToRotation(transform.up, desiredUp) * transform.rotation;
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotaton, Time.deltaTime * alignmentSpeed);
            }

            // Ray-cast Side Way
            Vector3 leftRayDirection = Quaternion.Euler(0, transform.eulerAngles.y, maxAngleSide) * Vector3.down;
            Vector3 rightRayDirection = Quaternion.Euler(0, transform.eulerAngles.y, -maxAngleSide) * Vector3.down;

            Vector3 leftRayOrigin = transform.position + transform.right * sideRayOffset;
            Vector3 rightRayOrigin = transform.position - transform.right * sideRayOffset;

            _isNearGroundLeft = Physics.Raycast(leftRayOrigin, leftRayDirection, out hit, groundCheckDistanceSide, groundMask);
            _isNearGroundRight = Physics.Raycast(rightRayOrigin, rightRayDirection, out hit, groundCheckDistanceSide, groundMask);
        }

        #endregion

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            // Draw ground Gizmo
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

            // Draw Sphere if check collision.
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance, groundMask))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.5f);
            }

            // Draw Side-way Gizmo
            Gizmos.color = Color.blue;

            Vector3 leftRayDirection = Quaternion.Euler(0, transform.eulerAngles.y, maxAngleSide) * Vector3.down;
            Vector3 rightRayDirection = Quaternion.Euler(0, transform.eulerAngles.y, -maxAngleSide) * Vector3.down;

            Vector3 leftRayOrigin = transform.position + transform.right * sideRayOffset;
            Vector3 rightRayOrigin = transform.position - transform.right * sideRayOffset;

            Gizmos.DrawLine(leftRayOrigin, leftRayOrigin + leftRayDirection * groundCheckDistanceSide);
            Gizmos.DrawLine(rightRayOrigin, rightRayOrigin + rightRayDirection * groundCheckDistanceSide);

            // Draw Sphere if check collision.
            if (Physics.Raycast(leftRayOrigin, leftRayDirection, out hit, groundCheckDistanceSide, groundMask))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.5f);
            }

            if (Physics.Raycast(rightRayOrigin, rightRayDirection, out hit, groundCheckDistanceSide, groundMask))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
        }

#endif

    }
}