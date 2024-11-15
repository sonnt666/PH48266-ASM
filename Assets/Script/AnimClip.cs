using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimClip", menuName = "ScriptableObject/AnimClip")]
public class AnimClip : ScriptableObject
{
    public AnimationClip Idle;
    public AnimationClip Run;
    public AnimationClip Atk;
}
