using UnityEngine;

public enum TalentType
{
    Laser
}

[CreateAssetMenu(menuName = "Game Framework/Game Data/Talent Definition")]
public class TalentDefinition : ScriptableObject
{
    [SerializeField] protected TalentType _type;
    [SerializeField] protected string _name;
    [TextArea]
    [SerializeField] protected string _description;

    public TalentType Type => _type;
    public string Name => _name;
    public string Description => _description;
}
