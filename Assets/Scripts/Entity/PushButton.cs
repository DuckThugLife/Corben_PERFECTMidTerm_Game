using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private bool shootable;

    public UnityEvent onPush;

    public UnityEvent onHoverEnter, onHoverExit;

    public void OnSelect()
    {
        onPush?.Invoke();
    }
    public void OnHoverEnter()
    {
       onHoverEnter?.Invoke();
    }

    public void OnHoverExit()
    {
      onHoverExit?.Invoke();
    }




    private void OnTriggerEnter(Collider other) // Be able to shoot bullets to interact with the button
    {
        if (other.gameObject.GetComponent<Bullet>() && shootable)
        {
            onPush?.Invoke();
        }
    }

}
