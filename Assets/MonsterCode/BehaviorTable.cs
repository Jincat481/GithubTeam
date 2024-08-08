using UnityEngine;

[CreateAssetMenu(fileName = "NewBehaviorTable", menuName = "Boss/BehaviorTable")]
public class BehaviorTable : ScriptableObject
{
    public Pattern[] patterns;
}
