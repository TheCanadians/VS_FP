using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyKeyboardController : MonoBehaviour
{
    [SerializeField] private WaypointManager wpMan;

    [SerializeField] private float verSpeed = 10f;
    [SerializeField] private float rotSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (wpMan == null)
            Debug.Log("WaypointManager is NULL");
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
        float ver = Input.GetAxis("Vertical");
        float rot = Input.GetAxis("Horizontal");

        float translation = ver * verSpeed * Time.deltaTime;
        float rotation = rot * rotSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
