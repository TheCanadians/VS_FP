using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private Log log;

    [SerializeField] private GameObject currentWaypoint;
    private bool questionActivated = false;

    [SerializeField] private bool motionBlur = false;

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

    public bool GetMotionBlur()
    {
        return motionBlur;
    }

    public void Quit()
    {
        log.Save();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}
