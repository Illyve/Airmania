using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AircraftPhysics : MonoBehaviour
{
    const float PREDICTION_TIMESTEP_FRACTION = 0.5f;
    [SerializeField]
    float thrust = 8000;
    float boostSpeed = 4f;
    float boostCooldown = 8f;
    float boostTimer = 4f;
    float timer= 0f;
    float timerInterpolation;
    float startedBoost = 0f;
    float cooldownBoost = 0f;
    bool hasCooldown;
    float slowDown = 10f;
    bool hasSlowedDown = false;
    bool startedBoosts = true;
    bool pressedDown = false;
    [SerializeField] 
    List<AeroSurface> aerodynamicSurfaces = null;
    Rigidbody rb;
    float thrustPercent;
    [Header("Field of View")]
    BiVector3 currentForceAndTorque;

    public void SetThrustPercent(float percent)
    {
        thrustPercent = percent;
        if (Input.GetKeyDown(KeyCode.LeftShift) && !hasCooldown && !pressedDown)
        {
            if(startedBoosts == true)
            {
                startedBoost = timer;
                startedBoosts = false;
                
            }
            Debug.Log(startedBoost);
            startedBoost = timer;
            Debug.Log("Speeding up!");
            pressedDown = true;
            
            
        }

        if ((timer - startedBoost) >= boostTimer && pressedDown == true)
        {
            Debug.Log("On cooldown!");
            timer = cooldownBoost;
            hasCooldown = true;
            hasSlowedDown = false;
            pressedDown = false;
        }
        else {
            if ((timer - cooldownBoost) >= boostCooldown)
            {
                startedBoosts = true;
                hasCooldown = false;
            
            }
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        timerInterpolation =  Time.deltaTime;
        timer += Time.deltaTime;
        if (pressedDown)
        {
            Debug.Log(thrust);
            thrust = Mathf.Lerp(thrust, (thrust * boostSpeed *.3f), timerInterpolation);
            if(timerInterpolation > 1.0f)
            {
                timerInterpolation = 0.0f;
            }
        }
        else
        {
            if ((timer-startedBoost) >= 1 && hasSlowedDown && (timer-startedBoost) <= 2.5)
            {
                thrust = Mathf.Lerp(thrust, (thrust / (boostSpeed*.52f)), timerInterpolation);
                Debug.Log(thrust);
            }
            Debug.Log(thrust);
            if(timerInterpolation > 1.0f)
            {
                timerInterpolation = 0.0f;
            }
        }

        BiVector3 forceAndTorqueThisFrame = 
            CalculateAerodynamicForces(rb.velocity, rb.angularVelocity, Vector3.zero, 1.2f, rb.worldCenterOfMass);

        Vector3 velocityPrediction = PredictVelocity(forceAndTorqueThisFrame.p
            + transform.forward * thrust * thrustPercent + Physics.gravity * rb.mass);
        Vector3 angularVelocityPrediction = PredictAngularVelocity(forceAndTorqueThisFrame.q);

        BiVector3 forceAndTorquePrediction = 
            CalculateAerodynamicForces(velocityPrediction, angularVelocityPrediction, Vector3.zero, 1.2f, rb.worldCenterOfMass);

        currentForceAndTorque = (forceAndTorqueThisFrame + forceAndTorquePrediction) * 0.5f;
 
        rb.AddForce(currentForceAndTorque.p);
        rb.AddTorque(currentForceAndTorque.q);

        rb.AddForce(transform.forward * thrust * thrustPercent);
       if (slowDown > .15 && hasCooldown && !hasSlowedDown) {
            rb.AddForce(-currentForceAndTorque.p * slowDown);
            rb.AddTorque(-currentForceAndTorque.q * slowDown);
            rb.AddForce(-transform.forward * thrust * thrustPercent * slowDown);
            hasSlowedDown = true;
            Debug.Log("Slowing down!");
        }
        else
        {
            slowDown = 2f;
        }
    }

    private BiVector3 CalculateAerodynamicForces(Vector3 velocity, Vector3 angularVelocity, Vector3 wind, float airDensity, Vector3 centerOfMass)
    {
        BiVector3 forceAndTorque = new BiVector3();
        foreach (var surface in aerodynamicSurfaces)
        {
            Vector3 relativePosition = surface.transform.position - centerOfMass;
            forceAndTorque += surface.CalculateForces(-velocity + wind
                -Vector3.Cross(angularVelocity,
                relativePosition),
                airDensity, relativePosition);
        }
        return forceAndTorque;
    }

    private Vector3 PredictVelocity(Vector3 force)
    {
        return rb.velocity + Time.fixedDeltaTime * PREDICTION_TIMESTEP_FRACTION * force / rb.mass;
    }

    private Vector3 PredictAngularVelocity(Vector3 torque)
    {
        Quaternion inertiaTensorWorldRotation = rb.rotation * rb.inertiaTensorRotation;
        Vector3 torqueInDiagonalSpace = Quaternion.Inverse(inertiaTensorWorldRotation) * torque;
        Vector3 angularVelocityChangeInDiagonalSpace;
        angularVelocityChangeInDiagonalSpace.x = torqueInDiagonalSpace.x / rb.inertiaTensor.x;
        angularVelocityChangeInDiagonalSpace.y = torqueInDiagonalSpace.y / rb.inertiaTensor.y;
        angularVelocityChangeInDiagonalSpace.z = torqueInDiagonalSpace.z / rb.inertiaTensor.z;

        return rb.angularVelocity + Time.fixedDeltaTime * PREDICTION_TIMESTEP_FRACTION
            * (inertiaTensorWorldRotation * angularVelocityChangeInDiagonalSpace);
    }

#if UNITY_EDITOR
    // For gizmos drawing.
    public void CalculateCenterOfLift(out Vector3 center, out Vector3 force, Vector3 displayAirVelocity, float displayAirDensity)
    {
        Vector3 com;
        BiVector3 forceAndTorque;
        if (aerodynamicSurfaces == null)
        {
            center = Vector3.zero;
            force = Vector3.zero;
            return;
        }

        if (rb == null)
        {
            com = GetComponent<Rigidbody>().worldCenterOfMass;
            forceAndTorque = CalculateAerodynamicForces(-displayAirVelocity, Vector3.zero, Vector3.zero, displayAirDensity, com);
        }
        else
        {
            com = rb.worldCenterOfMass;
            forceAndTorque = currentForceAndTorque;
        }

        force = forceAndTorque.p;
        center = com + Vector3.Cross(forceAndTorque.p, forceAndTorque.q) / forceAndTorque.p.sqrMagnitude;
    }
#endif
}


