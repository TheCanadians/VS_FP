using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private WaypointManager wpMan;
    private Log log;
    private Transform player;

    [SerializeField] private float radius = 3f;

    [SerializeField] private bool question = false;
    public bool questionAnswered = false;
    [SerializeField] private GameObject canvas;

    [SerializeField] private Waypoint nextWaypoint = null;

    private int sicknessScore = 100;

    // Start is called before the first frame update
    void Start()
    {
        wpMan = GameObject.Find("Manager").GetComponent<WaypointManager>();
        log = GameObject.Find("Manager").GetComponent<Log>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.Log("Player is NULL");
            GameObject.FindGameObjectWithTag("Player");
        }
        if (canvas == null)
            Debug.Log("Canvas is NULL");
        if (wpMan == null)
            Debug.Log("WaypointManager is NULL");
        if (log == null)
            Debug.Log("Log is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, this.transform.position);

        if (distance < radius)
        {
            if (question && !canvas.gameObject.activeInHierarchy)
            {
                LoadQuestion();
            }
            if (!question)
            {
                questionAnswered = true;
            }
            if (questionAnswered)
                LoadNextWaypoint();

        }
    }

    private void LoadNextWaypoint()
    {
        if (sicknessScore != 100)
            log.AddRow(sicknessScore.ToString());
        else
            log.AddRow("");

        if (nextWaypoint != null)
        {
            nextWaypoint.gameObject.SetActive(true);
            wpMan.SetCurrentWaypoint(nextWaypoint.gameObject);
            this.gameObject.SetActive(false);
        }
        else
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

    private void LoadQuestion()
    {
        canvas.SetActive(true);
        wpMan.SetQuestionActivated(true);      
    }

    public void ReturnUIValues(bool state, int sickness)
    {
        questionAnswered = state;
        sicknessScore = sickness;
    }
}
