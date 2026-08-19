using UnityEngine;

[CreateAssetMenu(fileName = "TransformVariableSO", menuName = "Variables/Transform Variable")]
public class TransformVariableSO : ScriptableObject
{
    [SerializeField] private Transform _value;

    public Transform Value { 
        get { return _value; } 
        set { _value = value; }
    }
}
