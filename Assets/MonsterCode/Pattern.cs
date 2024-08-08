using UnityEngine;

[CreateAssetMenu(fileName = "NewPattern", menuName = "Boss/Pattern")]
public class Pattern : ScriptableObject
{
    public Skill[] skills;
}
