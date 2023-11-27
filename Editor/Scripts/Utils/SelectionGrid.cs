using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Yueby.Utils
{
    public class SelectionGrid
    {
        private readonly bool _isShowAddButton;
        private readonly bool _isShowRemoveButton;

        private readonly SerializedProperty _serializedProperty;
        private List<SelectionGridButton> _selectionGridButtons;

        public event UnityAction OnAdd;
        public event UnityAction<int, Object> OnRemove;
        public event UnityAction<SerializedProperty> OnChangeSelected;

        private static Vector2 _scrollPos;
        public event UnityAction OnTitleDraw;

        private int _selectedIndex = -1;
        private SelectionGridButton _selectedButton;

        private readonly bool _isPPTR;

        public SerializedProperty CurrentSelection => _selectedButton?.SerializedProperty;

        public int Count => _selectionGridButtons?.Count ?? 0;

        public int Index
        {
            get => _selectedIndex;
            set
            {
                if (value < 0)
                    value = 0;

                _selectedIndex = value;
                PlayerPrefs.SetInt("SelectionGridIndex", _selectedIndex);
            }
        }

        private readonly float _gridHeight;

        public SelectionGrid(SerializedProperty serializedProperty, float gridMaxHeight, bool isShowAddButton = true, bool isShowRemoveButton = true, bool isPPTR = false)
        {
            _serializedProperty = serializedProperty;
            _isShowAddButton = isShowAddButton;
            _isShowRemoveButton = isShowRemoveButton;
            _isPPTR = isPPTR;
            _gridHeight = gridMaxHeight;

            GenerateButtons();
        }

        public void Clear()
        {
            if (PlayerPrefs.HasKey("SelectionGridIndex"))
            {
                PlayerPrefs.DeleteKey("SelectionGridIndex");
            }
        }

        private void GenerateButtons()
        {
            if (_selectionGridButtons == null)
                _selectionGridButtons = new List<SelectionGridButton>();
            else
                _selectionGridButtons.Clear();

            if (_serializedProperty.arraySize <= 0) return;

            for (var i = 0; i < _serializedProperty.arraySize; i++)
            {
                var item = _serializedProperty.GetArrayElementAtIndex(i);
                var selectionGridButton = new SelectionGridButton(item, i);
                selectionGridButton.OnClick += Select;
                _selectionGridButtons.Add(selectionGridButton);
            }

            if (PlayerPrefs.HasKey("SelectionGridIndex") && Count > 0)
            {
                Index = PlayerPrefs.GetInt("SelectionGridIndex");
                if (Index < 0)
                    Index = 0;
                else if (Index > Count - 1)
                    Index = Count - 1;
            }
            else
                Index = 0;

            Select(Index);
        }

        public void Select(int index)
        {
            Index = index;

            _selectedButton?.Deselect();

            _selectedButton = _selectionGridButtons[index];
            _selectedButton.Select();

            OnChangeSelected?.Invoke(_selectedButton.SerializedProperty);
        }

        private void Add()
        {
            _serializedProperty.arraySize++;
            OnAdd?.Invoke();
        }

        private void Remove()
        {
            var item = _isPPTR ? _serializedProperty.GetArrayElementAtIndex(Index).objectReferenceValue : null;
            if (_isPPTR)
                _serializedProperty.GetArrayElementAtIndex(Index).objectReferenceValue = null;
            _serializedProperty.DeleteArrayElementAtIndex(Index);

            OnRemove?.Invoke(Index, item);

            if (_serializedProperty.arraySize > 1)
            {
                if (Index == Count - 1)
                    Index--;
                Select(Index);
            }
        }

        public void Draw(UnityAction<Rect, int> onArrayItemDraw, float elementEdgeLength, Vector2 padding)
        {
            YuebyUtil.VerticalEGL("Badge", () =>
            {
                YuebyUtil.SpaceArea(() =>
                {
                    // 绘制标题头
                    YuebyUtil.HorizontalEGL(() =>
                    {
                        YuebyUtil.HorizontalEGL("Badge", () => { EditorGUILayout.LabelField($"{_serializedProperty.arraySize}", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(25), GUILayout.Height(18)); }, GUILayout.Width(25), GUILayout.Height(18));

                        EditorGUILayout.Space();
                        OnTitleDraw?.Invoke();
                        if (_isShowAddButton && GUILayout.Button("+", GUILayout.Width(25), GUILayout.Height(18)))
                        {
                            Add();
                        }

                        EditorGUI.BeginDisabledGroup(_serializedProperty.arraySize == 0);
                        if (_isShowRemoveButton && GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(18)))
                        {
                            Remove();
                        }

                        EditorGUI.EndDisabledGroup();
                    });

                    YuebyUtil.Line(LineType.Horizontal, 2, 0);
                    if (_selectionGridButtons.Count > 0)
                    {
                        // 绘制列表内容
                        _scrollPos = YuebyUtil.ScrollViewEGL(() =>
                        {
                            var width = Screen.width;
                            var count = Mathf.Floor((width - padding.x) / (elementEdgeLength + padding.x));
                            var maxHeight = (elementEdgeLength + padding.y) * Mathf.Ceil(_serializedProperty.arraySize / count);

                            var gridRect = GUILayoutUtility.GetRect(elementEdgeLength, width, elementEdgeLength, maxHeight);

                            GUILayout.Label("", GUILayout.Width(0), GUILayout.Height(maxHeight - elementEdgeLength));

                            for (var i = 0; i < _selectionGridButtons.Count; i++)
                            {
                                var x = gridRect.x + padding.x + (elementEdgeLength + padding.x) * (i % count);
                                var y = gridRect.y + padding.y + (elementEdgeLength + padding.y) * Mathf.Floor(i / count);

                                var btnRect = new Rect(x, y, elementEdgeLength, elementEdgeLength);

                                var gridButton = _selectionGridButtons[i];
                                gridButton.Draw(btnRect, (rect, index) =>
                                {
                                    if (index > 0 || _serializedProperty.arraySize != 0)
                                        onArrayItemDraw?.Invoke(rect, index > _serializedProperty.arraySize - 1 ? 0 : index);
                                });
                            }
                        }, _scrollPos);
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("请添加一件物品", MessageType.Info);
                    }
                });
            }, option: GUILayout.MaxHeight(_gridHeight));
        }

        public class SelectionGridButton
        {
            public bool IsSelected { get; private set; }
            public SerializedProperty SerializedProperty { get; private set; }

            public event UnityAction<int> OnClick;
            public int Index { get; }

            public SelectionGridButton(SerializedProperty serializedProperty, int index)
            {
                SerializedProperty = serializedProperty;
                Index = index;

                var texture = new Texture2D(50, 50, TextureFormat.ARGB32, false);
                for (var y = 0; y < texture.height; y++)
                {
                    for (var x = 0; x < texture.width; x++)
                    {
                        var color = new Color(1, 1, 1, 0.1f);
                        texture.SetPixel(x, y, color);
                    }
                }

                texture.alphaIsTransparency = true;
                texture.Apply();
            }

            public void Draw(Rect rect, UnityAction<Rect, int> onArrayItemDraw)
            {
                if (GUI.Button(rect, ""))
                {
                    OnClick?.Invoke(Index);
                }

                onArrayItemDraw?.Invoke(rect, Index);

                if (IsSelected)
                {
                    GUI.Box(rect, "");
                }
            }

            public void Select()
            {
                IsSelected = true;
            }

            public void Deselect()
            {
                IsSelected = false;
            }
        }
    }
}