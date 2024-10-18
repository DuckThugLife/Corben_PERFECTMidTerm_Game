using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StickyWall : MonoBehaviour, ISticky
{
    [SerializeField] private bool useCounter;
    [SerializeField] public int stickyCounter;
    [SerializeField] private Material stickyMaterial;

    
    [SerializeField] private GameObject stickyObjectHolder;
    
    [SerializeField] private TMP_Text counterTxt;
    [SerializeField] private int maxStickCount = 4;

    private void Start()
    {
        stickyCounter = maxStickCount;

        UpdateCounterText();
    }

    public virtual void OnCollison(GameObject collisionGameObject)
    {
        collisionGameObject.transform.parent = stickyObjectHolder.transform;
        collisionGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
        StickyObject stickyObject = collisionGameObject.GetComponent<StickyObject>();
        

        stickyObject.wall = this;

        if (useCounter)
        {
            stickyCounter--;
            UpdateCounterText();
        }
        
        
    }

    private void StuckObject(GameObject objectStuck)
    {
        // objectStuck.GetComponent<MeshRenderer>().material = stickyMaterial;
    }

    public virtual void UnstickObject(GameObject collisionGameObject)
    {
        StickyObject stickyObject = collisionGameObject.GetComponent<StickyObject>();


        if (useCounter)
        {
            stickyCounter++;
            UpdateCounterText();
        }
        
        
        stickyObject.wall = null;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<StickyObject>())
            return; // making sure the object can actually be stuck to it
        if (collision.gameObject.GetComponent<PickCube>() && collision.gameObject.GetComponent<PickCube>().currentlyHeld) // if the object is being held by the player return and dont stick it;
            return;
        if (stickyCounter == 0 && useCounter)
            return;

        OnCollison(collision.gameObject);
    }


    private void UpdateCounterText()
    {
        if (counterTxt != null || useCounter)
        {
            counterTxt.SetText(stickyCounter.ToString());
        }
    }


}