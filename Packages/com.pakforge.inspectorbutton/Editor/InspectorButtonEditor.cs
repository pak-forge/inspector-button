using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace PakForge.InspectorButton.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class InspectorButtonEditor : UnityEditor.Editor
    {
        private class MethodData
        {
            public MethodInfo MethodInfo;
            public string ButtonName;
            public object[] Parameters;
            public ParameterInfo[] ParameterInfos;
        }

        private List<MethodData> _methods = new();

        private void OnEnable()
        {
            _methods = new List<MethodData>();
            var targetType = target.GetType();
            var methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<InspectorButtonAttribute>();
                if (attr != null)
                {
                    var paramInfos = method.GetParameters();
                    var methodData = new MethodData
                    {
                        MethodInfo = method,
                        ButtonName = string.IsNullOrEmpty(attr.ButtonName) ? ObjectNames.NicifyVariableName(method.Name) : attr.ButtonName,
                        ParameterInfos = paramInfos,
                        Parameters = new object[paramInfos.Length]
                    };

                    // Initialize default values for parameters
                    for (int i = 0; i < paramInfos.Length; i++)
                    {
                        var type = paramInfos[i].ParameterType;
                        if (type.IsValueType)
                        {
                            methodData.Parameters[i] = Activator.CreateInstance(type);
                        }
                    }

                    _methods.Add(methodData);
                }
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            // Default inspector
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            if (_methods.Count == 0) return root;

            root.Add(new ToolbarSpacer { style = { height = 10 } });

            foreach (var method in _methods)
            {
                var methodBox = new VisualElement();
                methodBox.style.marginTop = 2;
                methodBox.style.marginBottom = 2;
                methodBox.style.paddingTop = 5;
                methodBox.style.paddingBottom = 5;
                methodBox.style.borderTopLeftRadius = 5;
                methodBox.style.borderTopRightRadius = 5;
                methodBox.style.borderBottomLeftRadius = 5;
                methodBox.style.borderBottomRightRadius = 5;
                methodBox.style.backgroundColor = new Color(0.19f, 0.19f, 0.19f, 1);

                var button = new Button(() =>
                {
                    foreach (var t in targets)
                    {
                        method.MethodInfo.Invoke(t, method.Parameters);
                    }
                })
                {
                    text = method.ButtonName
                };
                methodBox.Add(button);

                if (method.ParameterInfos.Length > 0)
                {
                    var foldout = new Foldout
                    {
                        text = "Parameters",
                        value = false,
                        style =
                        {
                            marginLeft = 15
                        }
                    };

                    for (int i = 0; i < method.ParameterInfos.Length; i++)
                    {
                        var pIndex = i;
                        var pInfo = method.ParameterInfos[i];
                        var pType = pInfo.ParameterType;
                        var pName = ObjectNames.NicifyVariableName(pInfo.Name);

                        VisualElement field = null;

                        if (pType == typeof(int))
                        {
                            var f = new IntegerField(pName) { value = (int)method.Parameters[pIndex] };
                            f.RegisterValueChangedCallback(evt => method.Parameters[pIndex] = evt.newValue);
                            field = f;
                        }
                        else if (pType == typeof(string))
                        {
                            var f = new TextField(pName) { value = (string)method.Parameters[pIndex] };
                            f.RegisterValueChangedCallback(evt => method.Parameters[pIndex] = evt.newValue);
                            field = f;
                        }
                        else if (pType == typeof(float))
                        {
                            var f = new FloatField(pName) { value = (float)method.Parameters[pIndex] };
                            f.RegisterValueChangedCallback(evt => method.Parameters[pIndex] = evt.newValue);
                            field = f;
                        }
                        else if (pType == typeof(bool))
                        {
                            var f = new Toggle(pName) { value = (bool)method.Parameters[pIndex] };
                            f.RegisterValueChangedCallback(evt => method.Parameters[pIndex] = evt.newValue);
                            field = f;
                        }
                        else if (pType == typeof(Vector2))
                        {
                            var f = new Vector2Field(pName) { value = (Vector2)method.Parameters[pIndex] };
                            f.RegisterValueChangedCallback(evt => method.Parameters[pIndex] = evt.newValue);
                            field = f;
                        }
                        else if (pType == typeof(Vector3))
                        {
                            var f = new Vector3Field(pName) { value = (Vector3)method.Parameters[pIndex] };
                            f.RegisterValueChangedCallback(evt => method.Parameters[pIndex] = evt.newValue);
                            field = f;
                        }
                        else if (pType == typeof(Vector4))
                        {
                            var f = new Vector4Field(pName) { value = (Vector4)method.Parameters[pIndex] };
                            f.RegisterValueChangedCallback(evt => method.Parameters[pIndex] = evt.newValue);
                            field = f;
                        }
                        else if (pType.IsEnum)
                        {
                            var f = new EnumField(pName, (Enum)method.Parameters[pIndex]);
                            f.RegisterValueChangedCallback(evt => method.Parameters[pIndex] = evt.newValue);
                            field = f;
                        }
                        else
                        {
                            field = new HelpBox($"Parameter type {pType.Name} not supported for {pName}", HelpBoxMessageType.Warning);
                        }

                        field.style.marginRight = 15;
                        foldout.Add(field);
                    }

                    methodBox.Add(foldout);
                }

                root.Add(methodBox);
            }

            return root;
        }
    }
}
