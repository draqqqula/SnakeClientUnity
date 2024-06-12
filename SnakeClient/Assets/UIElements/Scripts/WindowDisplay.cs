using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WindowDisplay : MonoBehaviour
{
    [SerializeField]
    public bool ShowOnStart;
    public void Start()
    {
        if (!ShowOnStart)
        {
            Hide();
        }
    }
    public abstract void Show();

    public abstract void Hide();
}
