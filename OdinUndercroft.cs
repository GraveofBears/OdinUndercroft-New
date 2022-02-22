using System.IO;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using PieceManager;

//using ServerSync;

namespace OdinUndercroft
{
    [BepInPlugin(HGUIDLower, ModName, version)]
    public class OdinUndercroftPlugin : BaseUnityPlugin
    {
        public const string version = "1.0.3";
        public const string ModName = "OdinsUndercroft";
        internal const string Author = "Gravebear";
        internal const string HGUID = Author + "." + "OdinsUndercroft";
        internal const string HGUIDLower = "gravebear.odinsundercroft";
        private const string HarmonyGUID = "Harmony." + Author + "." + ModName;
        private static string ConfigFileName = HGUIDLower + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;
        public static string ConnectionError = "";

        internal static ConfigEntry<int>? MaxNestedLimit;


        //harmony
        private static Harmony? harmony;

        private void Awake()
        {
            BuildPiece buildPiece = new("odinsundercroft", "OdinsUndercroft");
            buildPiece.Name.English("Odins Undercroft");
            buildPiece.Description.English("A large stone basement");
            buildPiece.RequiredItems.Add("Stone", 200, true);
            buildPiece.Prefab.gameObject.AddComponent<Basement>();

            harmony = new Harmony(HarmonyGUID);

            harmony.PatchAll();
            MaxNestedLimit = Config.Bind("General", "Max nested basements", 4,
                "The maximum number of basements you can incept into each other");
        }
        
        
    }
}