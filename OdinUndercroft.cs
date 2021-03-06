using System.IO;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using PieceManager;
using ServerSync;

namespace OdinUndercroft
{
    [BepInPlugin(HGUIDLower, ModName, version)]
    public class OdinUndercroftPlugin : BaseUnityPlugin
    {
        public const string version = "1.1.8";
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
            BuildPiece OdinsUndercroft = new("odins_undercroft", "OdinsUndercroft");
            OdinsUndercroft.Name.English("Odins Undercroft");
            OdinsUndercroft.Description.English("A large stone basement");
            OdinsUndercroft.RequiredItems.Add("Stone", 200, true);
            OdinsUndercroft.Prefab.gameObject.AddComponent<Basement>();

            BuildPiece OU_MetalGrate = new("odins_undercroft", "OU_MetalGrate");
            OU_MetalGrate.Name.English("Odins MetalGrate");
            OU_MetalGrate.Description.English("A large metal grate");
            OU_MetalGrate.RequiredItems.Add("Iron", 1, true);

            BuildPiece OU_Urn = new("odins_undercroft", "OU_Urn");
            OU_Urn.Name.English("Odins Urn");
            OU_Urn.Description.English("A place to keep your remains");
            OU_Urn.RequiredItems.Add("Stone", 6, true);

            BuildPiece OU_Sarcophagus = new("odins_undercroft", "OU_Sarcophagus");
            OU_Sarcophagus.Name.English("Odins Sarcophagus");
            OU_Sarcophagus.Description.English("A large stone Sarcophagus");
            OU_Sarcophagus.RequiredItems.Add("Stone", 10, true);

            BuildPiece OU_Sarcophagus_Lid = new("odins_undercroft", "OU_Sarcophagus_Lid");
            OU_Sarcophagus_Lid.Name.English("Odins Sarcophagus Lid");
            OU_Sarcophagus_Lid.Description.English("A large stone Sarcophagus Lid");
            OU_Sarcophagus_Lid.RequiredItems.Add("Stone", 3, true);

            BuildPiece OU_Skeleton_Full = new("odins_undercroft", "OU_Skeleton_Full");
            OU_Skeleton_Full.Name.English("Odins Skeleton Full");
            OU_Skeleton_Full.Description.English("A Skeleton Full");
            OU_Skeleton_Full.RequiredItems.Add("BoneFragments", 4, true);

            BuildPiece OU_Skeleton_Ribs = new("odins_undercroft", "OU_Skeleton_Ribs");
            OU_Skeleton_Ribs.Name.English("Odins Skeleton Ribs");
            OU_Skeleton_Ribs.Description.English("A ribcage from a skeleton");
            OU_Skeleton_Ribs.RequiredItems.Add("BoneFragments", 2, true);

            BuildPiece OU_Skeleton_Hanging = new("odins_undercroft", "OU_Skeleton_Hanging");
            OU_Skeleton_Hanging.Name.English("Odins Skeleton Hanging");
            OU_Skeleton_Hanging.Description.English("A Skeleton Hung");
            OU_Skeleton_Hanging.RequiredItems.Add("BoneFragments", 2, true);

            BuildPiece OU_Skeleton_Pile = new("odins_undercroft", "OU_Skeleton_Pile");
            OU_Skeleton_Pile.Name.English("Odins Skeleton Pile");
            OU_Skeleton_Pile.Description.English("A Skeleton Pile");
            OU_Skeleton_Pile.RequiredItems.Add("BoneFragments", 2, true);

            BuildPiece OU_StoneArchway = new("odins_undercroft", "OU_StoneArchway");
            OU_StoneArchway.Name.English("Odins Stone Archway");
            OU_StoneArchway.Description.English("A stone Stone Archway");
            OU_StoneArchway.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_StoneWall = new("odins_undercroft", "OU_StoneWall");
            OU_StoneWall.Name.English("Odins StoneWall");
            OU_StoneWall.Description.English("A stone wall");
            OU_StoneWall.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_StoneHalfWall = new("odins_undercroft", "OU_StoneHalfWall");
            OU_StoneHalfWall.Name.English("Odins StoneHalfWall");
            OU_StoneHalfWall.Description.English("A stone half wall");
            OU_StoneHalfWall.RequiredItems.Add("Stone", 1, true);

            BuildPiece OU_DrainPipe = new("odins_undercroft", "OU_DrainPipe");
            OU_DrainPipe.Name.English("Odins Drinpipe");
            OU_DrainPipe.Description.English("A stone drain deco");
            OU_DrainPipe.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_CornerCap = new("odins_undercroft", "OU_CornerCap");
            OU_CornerCap.Name.English("Odins CornerCap");
            OU_CornerCap.Description.English("A stone corner cap for OU walls");
            OU_CornerCap.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_CornerCap_Small = new("odins_undercroft", "OU_CornerCap_Small");
            OU_CornerCap_Small.Name.English("Odins CornerCap Small");
            OU_CornerCap_Small.Description.English("A small stone corner cap for OU walls");
            OU_CornerCap_Small.RequiredItems.Add("Stone", 1, true);

            BuildPiece OU_StoneBeam = new("odins_undercroft", "OU_StoneBeam");
            OU_StoneBeam.Name.English("Odins Stone Beam");
            OU_StoneBeam.Description.English("A stone beam cap for OU walls");
            OU_StoneBeam.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_StoneBeam_Small = new("odins_undercroft", "OU_StoneBeam_Small");
            OU_StoneBeam_Small.Name.English("Odins Stone Beam Small");
            OU_StoneBeam_Small.Description.English("A small stone beam cap for OU walls");
            OU_StoneBeam_Small.RequiredItems.Add("Stone", 1, true);

            BuildPiece OU_Iron_Cage = new("odins_undercroft", "OU_Iron_Cage");
            OU_Iron_Cage.Name.English("Odins Iron Cage");
            OU_Iron_Cage.Description.English("An iron cage");
            OU_Iron_Cage.RequiredItems.Add("Iron", 4, true);

            BuildPiece OU_Swords_Crossed = new("odins_undercroft", "OU_Swords_Crossed");
            OU_Swords_Crossed.Name.English("Odins Crossed Swords");
            OU_Swords_Crossed.Description.English("A stone pare of swords");
            OU_Swords_Crossed.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_Wall_Shield = new("odins_undercroft", "OU_Wall_Shield");
            OU_Wall_Shield.Name.English("Odins Wall Shield");
            OU_Wall_Shield.Description.English("A shield deco for walls");
            OU_Wall_Shield.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_StoneRoof_Tile = new("odins_undercroft", "OU_StoneRoof_Tile");
            OU_StoneRoof_Tile.Name.English("Odins StoneRoof Tile");
            OU_StoneRoof_Tile.Description.English("A stone rooftile piece");
            OU_StoneRoof_Tile.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_StoneFloor = new("odins_undercroft", "OU_StoneFloor");
            OU_StoneFloor.Name.English("Odins Stone Floor");
            OU_StoneFloor.Description.English("A stone floor piece");
            OU_StoneFloor.RequiredItems.Add("Stone", 2, true);

            BuildPiece OU_StoneStair = new("odins_undercroft", "OU_StoneStair");
            OU_StoneStair.Name.English("Odins StoneStairs");
            OU_StoneStair.Description.English("A stone stair piece");
            OU_StoneStair.RequiredItems.Add("Stone", 6, true);

            BuildPiece OU_Large_Stone_Pillar = new("odins_undercroft", "OU_Large_Stone_Pillar");
            OU_Large_Stone_Pillar.Name.English("Odins Large Stone Pillar");
            OU_Large_Stone_Pillar.Description.English("A large stone pillar");
            OU_Large_Stone_Pillar.RequiredItems.Add("Stone", 10, true);

            BuildPiece OU_Medium_Stone_Pillar = new("odins_undercroft", "OU_Medium_Stone_Pillar");
            OU_Medium_Stone_Pillar.Name.English("Odins Medium Stone Pillar");
            OU_Medium_Stone_Pillar.Description.English("A medium stone pillar");
            OU_Medium_Stone_Pillar.RequiredItems.Add("Stone", 6, true);

            BuildPiece OH_Undercroft_BuildSkull = new("odins_undercroft", "OH_Undercroft_BuildSkull");
            OH_Undercroft_BuildSkull.Name.English("Odins Crafting Skull");
            OH_Undercroft_BuildSkull.Description.English("Sets Build Area for undercroft pieces.");
            OH_Undercroft_BuildSkull.RequiredItems.Add("BoneFragments", 1, true);


            harmony = new Harmony(HarmonyGUID);

            harmony.PatchAll();
            MaxNestedLimit = Config.Bind("General", "Max nested basements", 1,
                "The maximum number of basements you can incept into each other");
        }


    }
}