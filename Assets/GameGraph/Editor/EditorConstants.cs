namespace GameGraph.Editor
{
    public static class EditorConstants
    {
        // Configuration
        // E.g.: "UnityEngine.CoreModule"
        public static readonly string[] ParameterAssemblyModules = { };
        public static readonly string[] ParameterAssemblyModulesStartWith =
        {
            "UnityEngine.",
        };
        public static readonly string[] ParameterTypesExcludedStrings = {"<", "$", "`"};

        // Magic constants
        public const string ParameterPortId = "!Instance";

        // Text
        public const string OpenEditorText = "Open Game Graph Editor";
        public const string CloseEditorSaveHeadline = "Game Graph Has Been Modified";
        public const string CloseEditorSaveContent = "Do you want to save the changes you made in the Graph?\n" +
                                                     "Your changes will be lost if you don't save them.";
        public const string CloseEditorSaveOk = "Save";
        public const string CloseEditorSaveCancel = "Don't Save";
        public const string BlackboardSubHeadline = "Game Graphs";
        public const string ParameterPortName = "Instance";
        public const string NodeSearchWindowHeadline = "Create Node";
        public const string ParameterSearchWindowHeadline = "Select Type";

        // Assets
        public const string FileExtension = "gamegraph";
        public const string AssetPath = "Assets/GameGraph/Editor";
        public const string ResourcesPath = AssetPath + "/Resources";
        public const string ResourcesUxmlPath = ResourcesPath + "/UXML";
        public const string ResourcesUxmlViewPath = ResourcesUxmlPath + "/View";
    }
}
