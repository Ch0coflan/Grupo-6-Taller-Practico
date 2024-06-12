using UnityEngine;

namespace Airplane
{
    [CreateAssetMenu(fileName = "AirplaneConfiguration", menuName = "AirplaneConfiguration", order = 0)]
    public class AirplaneConfiguration : ScriptableObject
    {
        
        [Header("Rotating speeds")]
        [Range(5f, 500f)]
        [SerializeField] public float yawSpeed = 50f;

        [Range(5f, 500f)]
        [SerializeField] public float pitchSpeed = 100f;

        [Range(5f, 500f)]
        [SerializeField] public float rollSpeed = 200f;

        [Header("Rotating speeds multipliers when turbo is used")]
         
        [SerializeField] public float yawTurboMultiplier = 0.3f;

        [Range(0.1f, 5f)]
        [SerializeField] public float pitchTurboMultiplier = 0.5f;

        [Range(0.1f, 5f)]
        [SerializeField] public float rollTurboMultiplier = 1f;

        [Header("Moving speed")]
        [Range(5f, 1000f)]
        [SerializeField] public float defaultSpeed = 150f;

        [Range(10f, 50f)]
        [SerializeField] public float turboSpeed = 20f;

        [Range(0.1f, 50f)]
        [SerializeField] public float accelerating = 10f;

        [Header("Damping settings")]
        [Range(0f, 5f)]
        [SerializeField] public float dampingSpeed = 2f;
        
        [Range(0f, 5f)]
        [SerializeField] public float linearDampingSpeed = 2f;

        [Range(0f, 100f)]
        [SerializeField] public float turboCooldownSpeed;

        [Tooltip("You can set this to determine when the turbo should cease overheating and become operational again")]
        [Range(0f, 100f)]
        [SerializeField] public float turboOverheatOver;

        [Header("Sideways force")]
        [Range(0.1f, 15f)]
        [SerializeField] public float sidewaysMovement = 15f;

        [Range(0.001f, 0.05f)]
        [SerializeField] public float sidewaysMovementXRot = 0.012f;

        [Range(0.1f, 5f)]
        [SerializeField] public float sidewaysMovementYRot = 1.5f;

        [Range(-1, 1f)]
        [SerializeField] public float sidewaysMovementYPosition = 0.1f;

        [SerializeField] public float groundCheckDistance = 10f;
        
        [SerializeField] public float sideRayOffset = 5f;
        
        [SerializeField] public float groundCheckDistanceSide = 8f;
        
        [Range(0f, 180f)]
        [SerializeField] public float maxAngleSide = 10f;
        
        [Range(0f, 5f)]
        [SerializeField] public float alignmentSpeed = 2f;
        
    }
}