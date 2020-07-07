using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{
    private WaypointManager wpMan;
    [SerializeField] private Waypoint wp;

    private int sickness = 5;

    [SerializeField] private Text sicknessText;

    private bool pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        wpMan = GameObject.Find("Manager").GetComponent<WaypointManager>();

        if (sicknessText == null)
            Debug.Log("Sickness Text is NULL");
        if (wpMan == null)
            Debug.Log("WaypointManager is NULL");
        if (wp == null)
            Debug.Log("Waypoint is NULL");
    }

    private void Update()
    {
        UIControl();
    }

    public void SicknessUp()
    {
        if (sickness + 1 > 10)
            sickness = -1;
        sickness++;
        sicknessText.text = sickness.ToString();
    }

    public void SicknessDown()
    {
        if (sickness - 1 < 0)
            sickness = 11;
        sickness--;
        sicknessText.text = sickness.ToString();
    }

    public void Submit()
    {
        wpMan.SetQuestionActivated(false);
        wp.ReturnUIValues(true, sickness);
    }

    private void UIControl()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && !pressed)
        {
            SicknessDown();
            pressed = true;
        }
        else if(Input.GetKeyDown(KeyCode.D) && !pressed)
        {
            SicknessUp();
            pressed = true;
        }
        else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            pressed = false;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Submit();
        }
    }
}
