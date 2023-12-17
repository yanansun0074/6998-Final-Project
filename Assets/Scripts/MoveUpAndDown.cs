using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
namespace Sigtrap.VrTunnellingPro{

public class MoveUpAndDown : InputSystemGlobalHandlerListener, IMixedRealityInputHandler<Vector2>
{
    public MixedRealityInputAction moveAction;
    public float multiplier = 5f;
    private Camera mainCamera;
    private GameObject player;

    private Vector3 delta = Vector3.zero;
    public void OnInputChanged(InputEventData<Vector2> eventData)
    {
        float horiz = eventData.InputData.x;
        float vert = eventData.InputData.y;
        if (eventData.MixedRealityInputAction == moveAction)
        {
            // delta = CameraCache.Main.transform.TransformDirection(new Vector3(horiz, 0, vert) * multiplier);
            delta = CameraCache.Main.transform.TransformDirection(new Vector3(0, horiz, 0) * multiplier);
        }
    }

    void Start()
    {
        // Set camera for tunneling effect
        mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();
        // Attach script
        Tunnelling cam_tun = mainCamera.gameObject.AddComponent<Tunnelling>();
        // Set camera as child of Player
        GameObject player = GameObject.Find("Player");
        player.transform.position = mainCamera.transform.position;
        mainCamera.transform.SetParent(player.transform);
        mainCamera.gameObject.GetComponent<Tunnelling>().motionTarget = mainCamera.gameObject.transform;

    }

    void Update()
    {
        if (delta.sqrMagnitude > 0.01f)
        {
            MixedRealityPlayspace.Transform.Translate(delta);
        }
        // player.transform.position = mainCamera.gameObject.transform.position;
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