using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntVariableSO", menuName = "Variables/Int Variable")]
public class IntVariableSO : ScriptableObject
{
    [SerializeField] private int _value;
    public event Action<int> OnValueChanged;

    public int Value { 
        get => _value; 
        set { 
            _value = value;
            OnValueChanged?.Invoke(value);
        } 
    }
}
