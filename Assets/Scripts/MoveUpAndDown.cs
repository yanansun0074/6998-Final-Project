using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;


public class MoveUpAndDown : InputSystemGlobalHandlerListener, IMixedRealityInputHandler<Vector2>
{
    public MixedRealityInputAction moveAction;
    public float multiplier = 5f;

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

    public void Update()
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