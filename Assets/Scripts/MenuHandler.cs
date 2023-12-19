using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;


public class MenuHandler : InputSystemGlobalHandlerListener, IMixedRealityInputHandler
{

    // MRTK Input Action
    public MixedRealityInputAction menuAction_button;


    // Text objects
    public TextMeshPro Title;
    public TextMeshPro ShowBookmarks;
    public TextMeshPro Bookmarkcurrent;

    //  Toggle Buttons
    public Interactable ShowBookmarksToggle;

    // menu object
    public GameObject Menu;
    // The minimap plate.
    public GameObject Miniature;

    // The data needed for smoothing the menu movement.
    private Vector3 targetPosition;

    // bool to turn on/off menu, radar, and surface
    private bool menuOn = false;
    private bool radarOn = true;
    private bool surfaceOn = true;
    private bool compassOn = false;
    public GameObject radar;
    public GameObject surface;
    public GameObject compass;

    private Camera mainCamera;



    // if button is pressed -- menuAction_button
    public void OnInputDown(InputEventData eventData)
    {
        if (eventData.MixedRealityInputAction == menuAction_button)
        {
            
            menuOn = true;
            updatePosition();
        }    
        
    }

    public void OnInputUp(InputEventData eventData) {}


    // Start is called before the first frame update
    void Start()
    {
        // mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();
        Menu.SetActive(false);
        mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();
        mainCamera.gameObject.AddComponent<MixedRealityInputModule>();

        // Menu.transform.position = Vector3.Lerp(Menu.transform.position, Camera.main.transform.position, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (menuOn)
        {
            Menu.SetActive(true);

        }
        else
        {
            Menu.SetActive(false);
}
    }

    void updatePosition()
    {
        
        Menu.transform.position = new Vector3(mainCamera.transform.position[0]+0.2f, mainCamera.transform.position[1], mainCamera.transform.position[2]+0.2f);
        // Menu.transform.rotation = Quaternion.Lerp(Menu.transform.rotation, mainCamera.transform.rotation, 0.02f);
        Menu.transform.rotation = mainCamera.transform.rotation;

    }

        protected override void RegisterHandlers()
    {
        CoreServices.InputSystem.RegisterHandler<MenuHandler>(this);
    }

    protected override void UnregisterHandlers()
    {
        CoreServices.InputSystem.UnregisterHandler<MenuHandler>(this);
    }

    // Close button: close menu
    public void CloseButton(bool shutDown)
    {
        if (shutDown)
        {
            Menu.SetActive(false);
            menuOn = false;
            Title.text = "Miniature Map";
        }
    }

    // Reset Button: reset scene
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
    }

    // Find the dem according to name.
    public void ToggleRadar()
    {
        radarOn = !radarOn;
        radar.SetActive(radarOn);
    }

    public void ToggleSurface()
    {
        surfaceOn = !surfaceOn;
        surface.SetActive(surfaceOn);
    }

    public void ToggleCompass()
    {
        compassOn = !compassOn;
        compass.SetActive(compassOn);
    }

}
