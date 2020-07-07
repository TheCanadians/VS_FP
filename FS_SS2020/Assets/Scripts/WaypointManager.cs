using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private GameObject currentWaypoint;
    private bool questionActivated = false;

    public void SetQuestionActivated (bool state)
    {
        questionActivated = state;
    }

    public bool GetQuestionAnswered()
    {
        return questionActivated;
    }

    public void SetCurrentWaypoint(GameObject waypoint)
    {
        currentWaypoint = waypoint;
    }

    public GameObject GetCurrentWaypoint()
    {
        return currentWaypoint;
    }
}
