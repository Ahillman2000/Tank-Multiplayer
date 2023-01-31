using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITweening : MonoBehaviour
{
    [SerializeField] private float scaleFactor = 1.1f;
    [SerializeField] private float scaleTime   = 0.25f;

    private Vector3 startScale;
    private Vector3 endScale;

    void Start()
    {
        startScale = this.gameObject.transform.localScale;
        endScale   = startScale * scaleFactor;
    }

    public void ScaleUp()
    {
        LeanTween.scale(this.gameObject, endScale, scaleTime);
    }

    public void ScaleDown()
    {
        LeanTween.scale(this.gameObject, startScale, scaleTime);
    }

    private void OnDisable()
    {
        ScaleDown();
    }
}
