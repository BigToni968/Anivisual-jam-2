using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MouseOverBut : MonoBehaviour
{
    Image img = null;
    Animator animator = null;

    private void Awake()
    {
        img = GetComponentInChildren<Image>();
        animator = GetComponentInChildren<Animator>();
        animator.enabled = false;
        img.enabled = false;
    }
    public void OnMouseOver()
    {
        animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        animator.enabled = true;
        img.enabled = true;
    }

    private void OnMouseExit()
    {
        animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        animator.enabled = false;
        img.enabled = false;
    }
}
