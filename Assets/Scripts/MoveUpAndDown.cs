using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
namespace Sigtrap.VrTunnellingPro{

public class MoveUpAndDown : InputSystemGlobalHandlerListener, IMixedRealityInputHandler<Vector2>
{
    public MixedRealityInputAction moveAction;
    public float multiplier = 5f;
    // UIRaycastCamera
    private Camera mainCamera;
    private GameObject player;

    private Vector3 delta = Vector3.zero;

    // VR Camera
    protected GameObject cam;

    public void OnInputChanged(InputEventData<Vector2> eventData)
    {
        float horiz = eventData.InputData.x;
        float vert = eventData.InputData.y;
        if (eventData.MixedRealityInputAction == moveAction)
        {
            // delta = player.transform.TransformDirection(new Vector3(horiz, 0, vert) * multiplier);
            delta = CameraCache.Main.transform.TransformDirection(new Vector3(0, horiz, 0) * multiplier);
        }
    }

    void Start()
    {
        // Set camera for tunneling effect
        mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();
        mainCamera.gameObject.SetActive(true);
        mainCamera.gameObject.transform.position = new Vector3(0,0,0);
        // Attach script
        Tunnelling cam_tun = mainCamera.gameObject.AddComponent<Tunnelling>();
        // Set camera as child of Player
        GameObject player = GameObject.Find("Player");
        
        // Try to turn off "Main Camera" for tunneling effecti
        // cam = GameObject.Find("Main Camera");
        // cam.GetComponent<Camera>().enabled = true;
        // cam.tag = "Untagged";
        // cam.GetComponent<Camera>().enabled = false;
        // mainCamera.GetComponent<Camera>().enabled = true;
        // mainCamera.tag = "MainCamera";

        // player.transform.position = mainCamera.transform.position;
        mainCamera.transform.SetParent(player.transform);
        player.transform.position = new Vector3(0,1.5f,0);
        mainCamera.GetComponent<Camera>().enabled = true;
        mainCamera.gameObject.GetComponent<Tunnelling>().motionTarget = player.gameObject.transform;

    }

    void Update()
    {
        if (delta.sqrMagnitude > 0.01f)
        {
            MixedRealityPlayspace.Transform.Translate(delta);
        }
    }

    protected override void RegisterHandlers()
    {
        CoreServices.InputSystem.RegisterHandler<MoveUpAndDown>(this);
    }

    protected override void UnregisterHandlers()
    {
        CoreServices.InputSystem.UnregisterHandler<MoveUpAndDown>(this);
    }
}
}