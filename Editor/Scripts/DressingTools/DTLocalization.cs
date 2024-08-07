﻿using System.Collections.Generic;
using Yueby.Utils;

namespace Yueby.AvatarTools.DressingTools
{
    public class DTLocalization : Localization
    {
        public DTLocalization()
        {
            Languages = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "中文", new Dictionary<string, string>
                    {
                        { "title_main_label", "穿戴工具" },
                        { "configure_avatar_label", "Avatar模型" },
                        { "configure_clothes_label", "目标物品" },
                        { "configure_avatar_tip", "↑ 将Avatar模型对象拖到这里来。" },
                        { "configure_clothes_tip", "将目标物品对象拖到这里来\t\t      ↑" },
                        { "configure_same_name_tip", "Avatar模型里有个目标物品和当前要穿上的物品重名，确定要穿上吗？" },
                        { "configure_sure_radio", "我确定！" },
                        { "configure_same_game_object_tip", "Avatar模型和目标物品是同一个对象！" },
                        { "configure_bone_name_issue_tip", "发现目标物品骨骼命名有问题！" },
                        { "configure_try_fix_button", "尝试修复" },
                        { "configure_armature_issue_tip", "Avatar模型或者目标物品上的骨架未找到，请检查骨架名字是否正确！" },
                        { "configure_armature_none_bone_tip", "Avatar模型或者目标物品骨架的底下没有其他骨骼！请检查！" },
                        { "configure_check_over_tip", "目标物品检查良好，可以穿上！" },
                        { "option_create_copy_radio", "创建Avatar模型副本并穿到副本上" },
                        { "option_delete_undo_tip", "这将删除选中的Avatar模型对象，删除后可以按\"CTRL + Z\"来撤回。" },
                        { "option_suffix_radio", "使用目标物品名字作为骨骼名字后缀" },
                        { "option_bone_will_named_tip", "你的骨骼将会被这样命名" },
                        { "option_dress_button", "穿上" },
                        { "option_test_button", "测试" },
                        { "option_test_t_pose_button", "T 姿势" },
                        { "option_test_idle_pose_button", "站立" },
                        { "option_test_walk_1_pose_button", "行走 1" },
                        { "option_test_walk_2_pose_button", "行走 2" },
                        { "option_test_run_pose_button", "跑步" },
                        { "option_test_prone_pose_button", "趴下" },
                        { "option_test_custom1_pose_button", "自定义1" },
                        { "option_test_custom2_pose_button", "自定义2" },
                        { "option_test_custom3_pose_button", "自定义3" },
                        { "option_test_custom4_pose_button", "自定义4" },
                        { "option_test_custom5_pose_button", "自定义5" },
                        { "option_test_custom6_pose_button", "自定义6" },
                        { "option_test_testing_tip", "测试中，选择动作来测试模型。" },
                        { "option_test_stop_button", "停止测试" },
                        { "title_configure_label", "配置" },
                        { "title_setup_label", "设置" },
                        { "option_auto_parent_keyword_radio", "自动设置关键字对应对象为根对象" },
                        { "option_none_keyword_object_tip", "如果没有 {0} 对象，则会自动创建一个。" },
                        { "option_combine_physbone_tip", "启用该选项将会提取出PhysBone为单独的对象，它将会放在目标物品对象下面。" },
                        { "option_combine_physbone_radio", "提取PhysBone" },
                        { "option_split_physbone_radio", "拆分为子对象" },
                        { "option_split_physbone_tip", "启用后会把每个PhysBone组件放到一个对应的新的对象上，然后统一管理。" },
                        { "option_keyword_label", "关键字" },
                        { "dialog_animator_check_title", "提示" },
                        { "dialogue_animator_check_content", "目标物品上有Animator组件，是否需要删除？" },
                        { "dialogue_yes_button", "是" },
                        { "dialogue_no_button", "否" },
                        { "option_test_change_path", "更改路径" },
                        { "option_test_jump_path", "跳转路径" },
                    }
                },
                {
                    "English", new Dictionary<string, string>
                    {
                        { "title_main_label", "Dressing Tool" },
                        { "configure_avatar_label", "VRC Avatar Descriptor" },
                        { "configure_clothes_label", "Target" },
                        { "configure_avatar_tip", "↑ Drag your avatar gameObject to here." },
                        { "configure_clothes_tip", "Drag your Target gameObject to here ↑" },
                        { "configure_same_name_tip", "There is a same name gameObject to your avatar. Are you sure you want dressing?" },
                        { "configure_sure_radio", "Sure!" },
                        { "configure_same_game_object_tip", "Avatar and Target are same gameObject!" },
                        { "configure_bone_name_issue_tip", "There is a problem with the bone name of the Target!" },
                        { "configure_try_fix_button", "Try Fix" },
                        { "configure_armature_issue_tip", "Avatar or Target armature is null. Check them name!" },
                        { "configure_armature_none_bone_tip", "Avatar or Target armature child count is 0." },
                        { "configure_check_over_tip", "Target are all right! Dressing now!" },
                        { "option_create_copy_radio", "Dressing by cloned avatar" },
                        { "option_delete_undo_tip", "It will delete avatar gameObject, when gameObject is deleted, you can press key \"CTRL + Z\" to undo." },
                        { "option_suffix_radio", "Use suffix by Target name" },
                        { "option_bone_will_named_tip", "Your bones will be named like" },
                        { "option_dress_button", "Dress" },
                        { "option_test_button", "Test" },
                        { "option_test_t_pose_button", "T Pose" },
                        { "option_test_idle_pose_button", "Idle" },
                        { "option_test_walk_1_pose_button", "Walk 1" },
                        { "option_test_walk_2_pose_button", "Walk 2" },
                        { "option_test_run_pose_button", "Run" },
                        { "option_test_prone_pose_button", "Prone" },
                        { "option_test_custom1_pose_button", "Custom1" },
                        { "option_test_custom2_pose_button", "Custom2" },
                        { "option_test_custom3_pose_button", "Custom3" },
                        { "option_test_custom4_pose_button", "Custom4" },
                        { "option_test_custom5_pose_button", "Custom5" },
                        { "option_test_custom6_pose_button", "Custom6" },
                        { "option_test_testing_tip", "Testing, choose pose to check avatar." },
                        { "option_test_stop_button", "Stop Testing" },
                        { "title_configure_label", "Configure" },
                        { "title_setup_label", "Setup" },
                        { "option_auto_parent_keyword_radio", "Auto parent to keyword gameObject" },
                        { "option_none_keyword_object_tip", "if {0} gameObject is null, it will be auto created." },
                        { "option_combine_physbone_tip", "Extract all physBone in a gameObject if enabled, it will put in cloth gameObject as a child." },
                        { "option_combine_physbone_radio", "Extract PhysBone" },
                        { "option_split_physbone_radio", "Split as child" },
                        { "option_split_physbone_tip", "Auto create a new GameObject and put each PhysBone component on it." },
                        { "option_keyword_label", "Keyword" },
                        { "dialog_animator_check_title", "Tips" },
                        { "dialogue_animator_check_content", "This gameObject contains Animator component, do you want to remove it?" },
                        { "dialogue_yes_button", "Yes" },
                        { "dialogue_no_button", "No" },
                        { "option_test_change_path", "Change Path" },
                        { "option_test_jump_path", "Jump To Path" },
                    }
                },
                {
                    "日本語 (Coming soon...)", new Dictionary<string, string>
                    {
                        { "title_main_label", "Dressing Tool" },
                        { "configure_avatar_label", "VRC Avatar Descriptor" },
                        { "configure_clothes_label", "Target" },
                        { "configure_avatar_tip", "↑ Drag your avatar gameObject to here." },
                        { "configure_clothes_tip", "Drag your Target gameObject to here ↑" },
                        { "configure_same_name_tip", "There is a same name gameObject to your avatar. Are you sure you want dressing?" },
                        { "configure_sure_radio", "Sure!" },
                        { "configure_same_game_object_tip", "Avatar and Target are same gameObject!" },
                        { "configure_bone_name_issue_tip", "There is a problem with the bone name of the Target!" },
                        { "configure_try_fix_button", "Try Fix" },
                        { "configure_armature_issue_tip", "Avatar or Target armature is null. Check them name!" },
                        { "configure_armature_none_bone_tip", "Avatar or Target armature child count is 0." },
                        { "configure_check_over_tip", "Target are all right! Dressing now!" },
                        { "option_create_copy_radio", "Dressing by cloned avatar" },
                        { "option_delete_undo_tip", "It will delete avatar gameObject, when gameObject is deleted, you can press key \"CTRL + Z\" to undo." },
                        { "option_suffix_radio", "Use suffix by Target name" },
                        { "option_bone_will_named_tip", "Your bones will be named like" },
                        { "option_dress_button", "Dress" },
                        { "option_test_button", "Test" },
                        { "option_test_t_pose_button", "T Pose" },
                        { "option_test_idle_pose_button", "Idle" },
                        { "option_test_walk_1_pose_button", "Walk 1" },
                        { "option_test_walk_2_pose_button", "Walk 2" },
                        { "option_test_run_pose_button", "Run" },
                        { "option_test_prone_pose_button", "Prone" },
                        { "option_test_custom1_pose_button", "Custom1" },
                        { "option_test_custom2_pose_button", "Custom2" },
                        { "option_test_custom3_pose_button", "Custom3" },
                        { "option_test_custom4_pose_button", "Custom4" },
                        { "option_test_custom5_pose_button", "Custom5" },
                        { "option_test_custom6_pose_button", "Custom6" },
                        { "option_test_testing_tip", "Testing, choose pose to check avatar." },
                        { "option_test_stop_button", "Stop Testing" },
                        { "title_configure_label", "Configure" },
                        { "title_setup_label", "Setup" },
                        { "option_auto_parent_keyword_radio", "Auto parent to keyword gameObject" },
                        { "option_none_keyword_object_tip", "if {0} gameObject is null, it will be auto created." },
                        { "option_combine_physbone_tip", "Combine all physBone in a gameObject if enabled, it will put in cloth gameObject as a child." },
                        { "option_combine_physbone_radio", "Combine PhysBone" },
                        { "option_split_physbone_radio", "Split as child" },
                        { "option_split_physbone_tip", "Auto create a new GameObject and put each PhysBone component on it." },
                        { "option_keyword_label", "Keyword" },
                        { "dialog_animator_check_title", "Tips" },
                        { "dialogue_animator_check_content", "This gameObject contains Animator component, do you want to remove it?" },
                        { "dialogue_yes_button", "Yes" },
                        { "dialogue_no_button", "No" },
                        { "option_test_change_path", "Change Path" },
                        { "option_test_jump_path", "Jump To Path" },
                    }
                }
            };
        }
    }
}