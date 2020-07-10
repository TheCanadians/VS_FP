using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class VRController : MonoBehaviour
{

    [SerializeField] private SteamVR_Input_Sources handType;

    [SerializeField] private SteamVR_Action_Boolean moveForward;
    [SerializeField] private SteamVR_Action_Boolean moveBackward;
    [SerializeField] private SteamVR_Action_Boolean UIControlRight;
    [SerializeField] private SteamVR_Action_Boolean UIControlLeft;
    [SerializeField] private SteamVR_Action_Boolean UISubmit;
    [SerializeField] private SteamVR_Action_Boolean triggerSubmit;
    [SerializeField] private SteamVR_Action_Boolean exit;

    [SerializeField] private WaypointManager wpMan;

    [SerializeField] private Transform moveTarget;
    [SerializeField] private Transform camera;
    [SerializeField] private float verSpeed = 10f;
    [SerializeField] private float rotAmount = 15f;

    [SerializeField] private Volume volume;
    private MotionBlur mb;
    [SerializeField] private float motionBlurStep = 0.25f;
    [SerializeField] private float motionBlurMinSpeedStep = 1f;

    private float ver = 0f;

    // Start is called before the first frame update
    void Start()
    {
        moveForward.AddOnStateDownListener(Forward, handType);
        moveForward.AddOnStateUpListener(StopMoving, handType);
        moveBackward.AddOnStateDownListener(Backward, handType);
        moveBackward.AddOnStateUpListener(StopMoving, handType);
        UIControlRight.AddOnStateDownListener(Right, handType);
        UIControlLeft.AddOnStateDownListener(Left, handType);
        UISubmit.AddOnStateDownListener(UISub, handType);
        triggerSubmit.AddOnStateDownListener(UISub, handType);
        exit.AddOnStateDownListener(Exit, handType);

        if (wpMan == null)
            Debug.Log("WaypointManager is NULL");

        MotionBlur tempMB;

        if (volume != null)
        {
            if (volume.profile.TryGet<MotionBlur>(out tempMB))
            {
                mb = tempMB;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!wpMan.GetQuestionAnswered())
        {
            CharacterMovement();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mb.intensity.Override(mb.intensity.value - motionBlurStep);
            Debug.Log("Intensitiy:  " + mb.intensity.value + "  Minimum Velocity:  " + mb.minimumVelocity.value);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mb.intensity.Override(mb.intensity.value + motionBlurStep);
            Debug.Log("Intensitiy:  " + mb.intensity.value + "  Minimum Velocity:  " + mb.minimumVelocity.value);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mb.minimumVelocity.Override(mb.minimumVelocity.value + motionBlurMinSpeedStep);
            Debug.Log("Intensitiy:  " + mb.intensity.value + "  Minimum Velocity:  " + mb.minimumVelocity.value);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mb.minimumVelocity.Override(mb.minimumVelocity.value - motionBlurMinSpeedStep);
            Debug.Log("Intensitiy:  " + mb.intensity.value + "  Minimum Velocity:  " + mb.minimumVelocity.value);
        }
    }

    private void CharacterMovement()
    {
        float translation = ver * verSpeed * Time.deltaTime;

        moveTarget.position += translation * camera.transform.forward;
    }

    private void Forward(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        ver = 1f;
    }

    private void Backward(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        ver = -1f;
    }

    private void StopMoving(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSourc)
    {
        ver = 0f;
    }

    private void Right(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!wpMan.GetQuestionAnswered())
            moveTarget.Rotate(0f, rotAmount, 0f);
        else
        {
            UISelection ui = wpMan.GetCurrentWaypoint().transform.Find("CanvasRotationPoint/Canvas").GetComponent<UISelection>();

            if (ui != null)
            {
                ui.SicknessUp();
            }
        }
    }

    private void Left(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!wpMan.GetQuestionAnswered())
            moveTarget.Rotate(0f, -rotAmount, 0f);
        else
        {
            UISelection ui = wpMan.GetCurrentWaypoint().transform.Find("CanvasRotationPoint/Canvas").GetComponent<UISelection>();
            if (ui != null)
            {
                ui.SicknessDown();
            }
        }
    }

    private void UISub(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (wpMan.GetQuestionAnswered())
        {
            UISelection ui = wpMan.GetCurrentWaypoint().transform.Find("CanvasRotationPoint/Canvas").GetComponent<UISelection>();
            if (ui != null)
            {
                ui.Submit();
            }
        }
    }

    private void Exit(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        wpMan.Quit();
    }
}
