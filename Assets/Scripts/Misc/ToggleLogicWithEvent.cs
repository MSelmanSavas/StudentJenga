using System.Collections;
using System.Collections.Generic;
using SimpleAtoms.Events;
using UnityEngine;

public class ToggleLogicWithEvent : MonoBehaviour
{
    [SerializeField]
    bool _isToggled = false;

    [SerializeField]
    VoidEvent _onToggleOn;

    [SerializeField]
    VoidEvent _onToggleOff;

    public void Toggle()
    {
        _isToggled = !_isToggled;

        VoidEvent selectedEvent = _isToggled ? _onToggleOn : _onToggleOff;

        selectedEvent?.Raise();
    }

    public bool GetToggleStatus() => _isToggled;
}
