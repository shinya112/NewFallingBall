using UnityEngine;

public enum ModifierType { Add, Multiply }

public class BounceObjectData : MonoBehaviour
{
    public ModifierType type = ModifierType.Add;
    public float value = 2f;

    public string GetDisplayText()
    {
        switch (type)
        {
            case ModifierType.Add: return "+" + value.ToString();
            case ModifierType.Multiply: return "Å~" + value.ToString("F1");
            default: return "";
        }
    }

    public float ApplyModifier(float input)
    {
        switch (type)
        {
            case ModifierType.Add: return input + value;
            case ModifierType.Multiply: return input * value;
            default: return input;
        }
    }
}