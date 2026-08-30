using UnityEngine;

public class CameraViewSwitcher : MonoBehaviour
{
    [Header("Cameras")]
    public Camera firstPersonCamera;
    public Camera demoCamera;

    [Header("Input")]
    public KeyCode toggleKey = KeyCode.V;

    private bool isThirdPerson = false;

    void Start()
    {
        SetFirstPersonView();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (isThirdPerson)
            {
                SetFirstPersonView();
            }
            else
            {
                SetThirdPersonView();
            }
        }
    }

    void SetFirstPersonView()
    {
        isThirdPerson = false;

        firstPersonCamera.gameObject.SetActive(true);
        demoCamera.gameObject.SetActive(false);
    }

    void SetThirdPersonView()
    {
        isThirdPerson = true;

        firstPersonCamera.gameObject.SetActive(false);
        demoCamera.gameObject.SetActive(true);
    }
}