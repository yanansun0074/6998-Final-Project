using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ShowKeyboard : MonoBehaviour
{
    private MRTKTMPInputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<MRTKTMPInputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }


    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
    }
}
