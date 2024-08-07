﻿using System;
using UnityEngine;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;

namespace Yueby.AvatarTools.ClothesManager
{
#if VRC_SDK_VRCSDK3

    public class CMAvatarDataReference : MonoBehaviour, IEditorOnly
#else
   public class CMAvatarDataReference : MonoBehaviour
#endif
    {
        public string ID;
        public CMCDataSo Data;
        public VRCExpressionsMenu ParentMenu;
        public string SavePath = "Assets/ClothesManager";
    }

    [Serializable]
    public class Parameter : VRC_AvatarParameterDriver.Parameter
    {
        public AnimatorControllerParameterType customType;
    }
}