using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        // Debug.Log("Object that entered the trigger : " + col);
        RubyController controller = col.GetComponent<RubyController>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}