using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private Log log;

    [SerializeField] private GameObject currentWaypoint;
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
