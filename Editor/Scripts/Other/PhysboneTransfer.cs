﻿using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Dynamics.PhysBone.Components;
using Yueby.Utils;

namespace YuebyAvatarTools.PhysBoneTransfer.Editor
{
    public class PhysboneTransfer : EditorWindow
    {
        private static PhysboneTransfer _window;

        private Transform _origin;
        private Transform _current;

        [MenuItem("Tools/YuebyTools/Avatar/Other/PhysBone Transfer", false, 21)]
        public static void OpenWindow()
        {
            _window = GetWindow<PhysboneTransfer>();

            _window.titleContent = new GUIContent("Avatar材质动骨转移");
            _window.minSize = new Vector2(500, 450);
        }

        private void OnGUI()
        {
            YuebyUtil.DrawEditorTitle("Avatar材质动骨转移");
            YuebyUtil.VerticalEGLTitled("配置", () =>
            {
                YuebyUtil.HorizontalEGL(() =>
                {
                    _origin = (Transform)YuebyUtil.ObjectFieldVertical(_origin, "原Armature", typeof(Transform));
                    YuebyUtil.Line(LineType.Vertical, 2f, 5f, 40f);
                    _current = (Transform)YuebyUtil.ObjectFieldVertical(_current, "现Armature", typeof(Transform));
                });
            });

            YuebyUtil.VerticalEGLTitled("设置", () =>
            {
                if (GUILayout.Button("转移"))
                {
                    Recursive(_origin);
                    TransferMaterials();
                }
            });
        }

        private void TransferMaterials()
        {
            var originRenderers = _origin.GetComponentsInChildren<Renderer>();

            if (originRenderers.Length <= 0) return;
            foreach (var originRenderer in originRenderers)
            {
                var targetPath = originRenderer.transform.GetHierarchyPath().Replace($"{_origin.name}", $"{_current.name}");
                var targetGo = GameObject.Find(targetPath);
                if (!targetGo) continue;
                var targetRenderer = targetGo.GetComponent<Renderer>();

                var materials = originRenderer.sharedMaterials;

                targetRenderer.sharedMaterials = materials;
                Undo.RegisterFullObjectHierarchyUndo(targetGo, "TransferMaterial");
            }
        }

        private void Recursive(Transform parent)
        {
            foreach (Transform child in parent)
            {
                TransferPhysBone(child);

                Recursive(child);
                // Debug.Log(child);
            }
        }

        private void TransferPhysBone(Transform child)
        {
            var physBone = child.GetComponent<VRCPhysBone>();
            if (physBone == null) return;
            var targetPath = child.GetHierarchyPath().Replace($"{_origin.name}", $"{_current.name}");
            var targetGo = GameObject.Find(targetPath);
            if (targetGo != null)
            {
                ComponentUtility.CopyComponent(physBone);

                targetGo.transform.localPosition = child.transform.localPosition;
                targetGo.transform.localRotation = child.transform.localRotation;


                if (targetGo.GetComponent<VRCPhysBone>() != null)
                    ComponentUtility.PasteComponentValues(targetGo.GetComponent<VRCPhysBone>());
                else
                    ComponentUtility.PasteComponentAsNew(targetGo);

                var targetPhysBone = targetGo.GetComponent<VRCPhysBone>();

                if (physBone.rootTransform != null)
                {
                    var rootTransGo = GameObject.Find(physBone.rootTransform.GetHierarchyPath().Replace($"{_origin.name}", $"{_current.name}"));
                    targetPhysBone.rootTransform = rootTransGo.transform;
                }
                else
                    targetPhysBone.rootTransform = targetGo.transform;


                // targetPhysBone.ignoreTransforms = GetIgnoreTransforms(targetPhysBone.ignoreTransforms);
                targetPhysBone.colliders = GetColliders(targetPhysBone.colliders);

                Undo.RegisterFullObjectHierarchyUndo(targetGo, "CopyComponent");
            }
        }

        // private List<Transform> GetIgnoreTransforms(List<Transform> transforms)
        // {
        //     var list = new List<Transform>();
        //
        //     foreach (var trans in transforms)
        //     {
        //         var targetPath = trans.GetHierarchyPath().Replace($"{_origin.name}", $"{_current.name}");
        //         var targetGo = GameObject.Find(targetPath);
        //     }
        //
        //     return list;
        // }

        private List<VRCPhysBoneColliderBase> GetColliders(List<VRCPhysBoneColliderBase> colliders)
        {
            var list = new List<VRCPhysBoneColliderBase>();
            foreach (var col in colliders)
            {
                var targetPath = col.transform.GetHierarchyPath().Replace($"{_origin.name}", $"{_current.name}");
                var targetGo = GameObject.Find(targetPath);

                if (targetGo != null)
                {
                    ComponentUtility.CopyComponent(col);

                    targetGo.transform.localPosition = col.transform.localPosition;
                    targetGo.transform.localRotation = col.transform.localRotation;

                    if (targetGo.GetComponent<VRCPhysBoneColliderBase>() != null)
                        ComponentUtility.PasteComponentValues(targetGo.GetComponent<VRCPhysBoneColliderBase>());
                    else
                        ComponentUtility.PasteComponentAsNew(targetGo);

                    Undo.RegisterFullObjectHierarchyUndo(targetGo, "CopyComponent");
                }
                else
                {
                    var parentPath = col.transform.parent.GetHierarchyPath().Replace($"{_origin.name}", $"{_current.name}");
                    var parent = GameObject.Find(parentPath);

                    if (parent != null)
                    {
                        targetGo = new GameObject(col.name)
                        {
                            transform =
                            {
                                parent = parent.transform,
                                localPosition = col.transform.localPosition,
                                localRotation = col.transform.localRotation
                            }
                        };
                        Undo.RegisterCreatedObjectUndo(targetGo, "CreateNewCollider");
                        ComponentUtility.CopyComponent(col);
                        ComponentUtility.PasteComponentAsNew(targetGo);
                    }
                }

                if (targetGo != null)
                    list.Add(targetGo.GetComponent<VRCPhysBoneColliderBase>());
            }

            return list;
        }
    }
}