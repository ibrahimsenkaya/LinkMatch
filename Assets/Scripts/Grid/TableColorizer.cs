using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TableColorizer : MonoBehaviour
{
    [SerializeField] private BoardCreator boardCreator; 
    public bool colorizeGradient;
    public bool withCurve;
    [ShowIf("colorizeGradient")]
    public Gradient gradient;
    [ShowIf("colorizeGradient")]
    public AnimationCurve gradientCurve;
    [ShowIf("colorizeGradient")]
    public EasingFunction.Ease ease;
    
    private void OnDrawGizmos()
    {
        if (boardCreator==null) return;
      
        float farest = 0;
        foreach (var point in boardCreator.points)
        {
            float magnitude = (transform.position - point).magnitude;
            if (magnitude>farest)
                farest = magnitude;
        }
        
    
        foreach (var pos in boardCreator.points)
        {
            if (colorizeGradient)
            {
                float magnitude = (transform.position - pos).magnitude;
                if (withCurve)
                    Gizmos.color=gradient.Evaluate(gradientCurve.Evaluate(MathHelper.Remap(magnitude,0,farest,0f,1f)));
                else
                    Gizmos.color=gradient.Evaluate( EasingFunction.GetEasingFunctionDerivative(ease)(0,1,MathHelper.Remap(magnitude,0,farest,0f,1f)));
                // float sample = Mathf.PerlinNoise(pos.x, pos.y);
                // Gizmos.color=gradient.Evaluate( sample);


            }
                
            else
                Gizmos.color=Color.red;
            if (boardCreator.currentDimension==Dimansions.XY)
                Gizmos.DrawCube(pos,new Vector3(boardCreator.width,boardCreator.height,.5f));
            if (boardCreator.currentDimension==Dimansions.XZ)
                Gizmos.DrawCube(pos,new Vector3(boardCreator.width,.5f,boardCreator.height));
        }
    }
}