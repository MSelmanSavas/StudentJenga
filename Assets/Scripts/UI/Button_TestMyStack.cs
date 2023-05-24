using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button_TestMyStack : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _buttonText;

    [SerializeField]
    ToggleLogicWithEvent _toggleLogic;

    [SerializeField]
    string _toggleOffText = "Test My Stack";

    [SerializeField]
    string _toggleOnText = "Test My Stack In Progress";

    public void UpdateText()
    {
        if (_toggleLogic == null)
            return;

        _buttonText.text = _toggleLogic.GetToggleStatus() ? _toggleOnText : _toggleOffText;
    }
}
