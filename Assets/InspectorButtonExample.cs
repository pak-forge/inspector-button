using UnityEngine;
using YoWorld.Core.Utilities.InspectorButton;

public class InspectorButtonExample : MonoBehaviour
{
    [InspectorButton]
    private void EmptyMethod()
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This is an empty method");
    }

    [InspectorButton("This is Custom Name")]
    private void EmptyMethodWithCustomName()
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This is an empty method with a custom name set");
    }
    
    [InspectorButton]
    private void MethodWithString(string value)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes a string value [{value}]");
    }
    
    [InspectorButton]
    private void MethodWithInt(int value)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes an int value [{value}]");
    }

    [InspectorButton]
    private void MethodWithFloat(float value)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes a float value [{value}]");
    }
    
    [InspectorButton]
    private void MethodWithBool(bool value)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes a bool value [{value}]");
    }
    
    [InspectorButton]
    private void MethodWithVector2(Vector2 value)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes a Vector2 value [{value}]");
    }
    
    [InspectorButton]
    private void MethodWithVector3(Vector3 value)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes a Vector3 value [{value}]");
    }
    
    [InspectorButton]
    private void MethodWithVector4(Vector4 value)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes a Vector4 value [{value}]");
    }
    
    [InspectorButton]
    private void MethodWithEnum(ExampleEnum value)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes an ExampleEnum value [{value}]");
    }

    [InspectorButton]
    private void MethodWithAllParamTypes(string stringValue, int intValue, float floatValue, bool boolValue, Vector2 vector2Value, Vector3 vector3Value, Vector4 vector4Value, ExampleEnum enumValue)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes all parameter types: string [{stringValue}], int [{intValue}], float [{floatValue}], bool [{boolValue}], Vector2 [{vector2Value}], Vector3 [{vector3Value}], Vector4 [{vector4Value}], ExampleEnum [{enumValue}]");
    }

    [InspectorButton]
    private void MethodWithUnsupportedType(AnimationClip animationClip)
    {
        Debug.Log($"[{nameof(InspectorButtonExample)}] : This method passes an unsupported type");
    }
    
    public enum ExampleEnum
    {
        Value1,
        Value2,
        Value3
    }
}
