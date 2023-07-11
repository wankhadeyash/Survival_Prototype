// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using UnityEditor;
// ReSharper disable All
namespace Doozy.Editor.UIManager.UIMenu
{
    public static class UIMenuContextMenu
    {
        private const int MENU_ITEM_PRIORITY = 7;
        private const string MENU_PATH = "GameObject/UIMenu";

        public static class Custom
        {
            private const string TYPE_NAME = "Custom";
            private const string TYPE_MENU_PATH = MENU_PATH + "/" + TYPE_NAME + "/";

            public static class Buttons
            {
                private const string CATEGORY_NAME = "Buttons";
                private const string CATEGORY_MENU_PATH = TYPE_MENU_PATH + CATEGORY_NAME + "/";

                [MenuItem(CATEGORY_MENU_PATH + "Close Button", false, MENU_ITEM_PRIORITY)]
                public static void CreateCloseButton(MenuCommand command) => UIMenuUtils.AddToScene(TYPE_NAME, CATEGORY_NAME, "CloseButton");

                [MenuItem(CATEGORY_MENU_PATH + "UI Button", false, MENU_ITEM_PRIORITY)]
                public static void CreateUIButton(MenuCommand command) => UIMenuUtils.AddToScene(TYPE_NAME, CATEGORY_NAME, "UIButton");
            }

            public static class InputField
            {
                private const string CATEGORY_NAME = "InputField";
                private const string CATEGORY_MENU_PATH = TYPE_MENU_PATH + CATEGORY_NAME + "/";

                [MenuItem(CATEGORY_MENU_PATH + "Input Field", false, MENU_ITEM_PRIORITY)]
                public static void CreateInputField(MenuCommand command) => UIMenuUtils.AddToScene(TYPE_NAME, CATEGORY_NAME, "InputField");
            }

            public static class Slot
            {
                private const string CATEGORY_NAME = "Slot";
                private const string CATEGORY_MENU_PATH = TYPE_MENU_PATH + CATEGORY_NAME + "/";

                [MenuItem(CATEGORY_MENU_PATH + "Inventory Slot", false, MENU_ITEM_PRIORITY)]
                public static void CreateInventorySlot(MenuCommand command) => UIMenuUtils.AddToScene(TYPE_NAME, CATEGORY_NAME, "InventorySlot");
            }
        }        
    }
}

