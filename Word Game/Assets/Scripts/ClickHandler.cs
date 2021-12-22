using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ClickHandler : MonoBehaviour
{
    public UnityEvent downEvent;
    private void OnClick()
    {
        downEvent?.Invoke();
    }
}
