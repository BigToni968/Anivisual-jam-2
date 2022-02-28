using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    Animator animator = null;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.speed = 0;
    }

    public void PanelOpen()
    {
        animator.speed = 1;
        animator.Play("Visibles");
    }
    
    public void PanelClsoe(float time)
    {
        StopAllCoroutines();
        StartCoroutine(_8(time));
    }

    public void Authors_haed_Visible(float time)
    {
        StopAllCoroutines();
        StartCoroutine(_7(time));
    }

    public void Authors_haed_Invible()
    {
        animator.Play("AHI");
    }

    public void Load_Visible(float time)
    {
        StopAllCoroutines();
        StartCoroutine(_9(time));
    }

    public void Load_Invible()
    {
        animator.Play("LoadI");
    }

    IEnumerator _7(float time)
    {
        yield return new WaitForSeconds(time);
        animator.Play("AHV");
    }

    IEnumerator _8(float time)
    {
        yield return new WaitForSeconds(time);
        animator.Play("Invisible");
    }

    IEnumerator _9(float time)
    {
        yield return new WaitForSeconds(time);
        animator.Play("LoadV");
    }
}
