using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DummyKeyboardController : MonoBehaviour
{
    [SerializeField] private WaypointManager wpMan;

    [SerializeField] private float verSpeed = 10f;
    [SerializeField] private float rotSpeed = 3f;

    [SerializeField] private Volume volume;
    private MotionBlur mb;

    // Start is called before the first frame update
    void Start()
    {
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
        float ver = Input.GetAxis("Vertical");
        float rot = Input.GetAxis("Horizontal");

        if (rot != 0)
        {
            mb.intensity.Override(7f);
        }
        else
        {
            mb.intensity.Override(0f);
        }

        float translation = ver * verSpeed * Time.deltaTime;
        float rotation = rot * rotSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
