using Game.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character Data", fileName = "Character_SO")]
public class CharacterSo : ScriptableObject
{
    [Header("Character Data")] 
    [SerializeField] [MinMaxRange(0, 1)] protected RangedFloat _runSpeed = new RangedFloat(.7f, .8f);
    
    public float RunSpeed { get; protected set; }

    public float GetRandomRunSpeed()
    {
        RunSpeed = Random.Range(_runSpeed.MinValue, _runSpeed.MaxValue);
        return RunSpeed;
    }
    
}
