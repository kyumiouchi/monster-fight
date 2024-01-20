using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Variable/Float", fileName = "Float_T_SO")]
public class FloatVariableSo : ScriptableObject
{
    [SerializeField] private float _value;
    public float Value => _value;
}
