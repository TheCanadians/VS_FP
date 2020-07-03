using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{

    private bool questionActivated = false;

    public void SetQuestionActivated (bool state)
    {
        questionActivated = state;
    }

    public bool GetQuestionAnswered()
    {
        return questionActivated;
    }
}
