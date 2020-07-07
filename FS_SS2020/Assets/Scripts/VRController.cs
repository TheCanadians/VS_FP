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

    [SerializeField] private WaypointManager wpMan;

    [SerializeField] private Transform moveTarget;
    [SerializeField] private Transform camera;
    [SerializeField] private float verSpeed = 10f;
    [SerializeField] private float rotAmount = 15f;

    [SerializeField] private Volume volume;
    private MotionBlur mb;

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
}
