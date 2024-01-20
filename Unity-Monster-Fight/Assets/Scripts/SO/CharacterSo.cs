using Game.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Character Data", fileName = "Character_SO")]
public class CharacterSo : ScriptableObject
{
    [Header("Character Data")] 
    [SerializeField] [MinMaxRange(5, 15)] private RangedFloat _runSpeed = new RangedFloat(8f, 10f);
    
    public float RunSpeed { get; protected set; }

    public float GetRandomRunSpeed()
    {
        RunSpeed = Random.Range(_runSpeed.MinValue, _runSpeed.MaxValue);
        return RunSpeed;
    }
}
