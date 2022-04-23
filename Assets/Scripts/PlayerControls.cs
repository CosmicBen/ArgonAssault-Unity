using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    private const string FIRE_BUTTON = "Fire1";

    [Header("General Setup Settings")]
    
    [Tooltip("How fast ship moves up, down, left, and right based upon player input.")]
    [SerializeField] private float controlSpeed = 10.0f;
    
    [Tooltip("How far the player moves horizontally.")]
    [SerializeField] private Vector2 xRange = new Vector2(-7.0f, 7.0f);
    
    [Tooltip("How far the player moves vertically.")]
    [SerializeField] private Vector2 yRange = new Vector2(-2.0f, 8.0f);
    
    [Header("Screen Position Based Tuning.")]

    [Tooltip("How much y movement affects pitch rotation.")]
    [SerializeField] private float pitchFactor = -2.0f;    

    [Tooltip("How much x movement affects yaw rotation.")]
    [SerializeField] private float yawFactor = 2.5f;

    [Header("Player Input Based Tuning.")]

    [Tooltip("How much y movement input affects pitch rotation.")]
    [SerializeField] private float controlPitchFactor = -15.0f;

    [Tooltip("How much x movement input affects roll rotation.")]
    [SerializeField] private float controlRollFactor = -15.0f;

    [Header("Laser Gun Array.")]
    [Tooltip("Add all player lasers here.")]
    [SerializeField] private ParticleSystem[] lasers = new ParticleSystem[0];

    private float horizontalThrow;
    private float verticalThrow;

    private void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        horizontalThrow = Input.GetAxis(HORIZONTAL_AXIS);
        verticalThrow = Input.GetAxis(VERTICAL_AXIS);

        float nextX = transform.localPosition.x + horizontalThrow * controlSpeed * Time.deltaTime;
        float clampedX = Mathf.Clamp(nextX, xRange.x, xRange.y);

        float nextY = transform.localPosition.y + verticalThrow * controlSpeed * Time.deltaTime;
        float clampedY = Mathf.Clamp(nextY, yRange.x, yRange.y);

        transform.localPosition = new Vector3(clampedX, clampedY, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = pitchFactor * transform.localPosition.y;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = yawFactor * transform.localPosition.x;
        float yaw = yawDueToPosition;

        float rollDueToControlThrow = horizontalThrow * controlRollFactor;
        float roll = rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        if (Input.GetButton(FIRE_BUTTON))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (ParticleSystem laser in lasers)
        {
            ParticleSystem.EmissionModule emissionModule = laser.emission;
            emissionModule.enabled = isActive;
        }
    }
}
