using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WindowDisplay : MonoBehaviour
{
    public UnityEvent OnShow;
    public UnityEvent OnHide;


    [SerializeField]
    public bool ShowOnStart;
    public virtual void Start()
    {
        if (!ShowOnStart)
        {
            Hide();
        }
    }
    public virtual void Show()
    {
        OnShow?.Invoke();
    }

    public virtual void Hide()
    {
        OnHide?.Invoke();
    }
}
