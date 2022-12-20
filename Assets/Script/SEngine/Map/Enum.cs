using UnityEngine;

namespace SEngine.Map
{
    public class Enum : MonoBehaviour
    {
        public enum ProtocolConst
        {
            SCENCE_LAYER_FLOOR = 0, //地面
            SCENCE_LAYER_WALL_A = 1, //A墙
            SCENCE_LAYER_WALL_B = 2, //B墙
            SCENCE_LAYER_ROOMFLOOR = 3, //房间地面
            SCENCE_OBJ_SIDE_HORIZON = 1, //水平，道具x朝向X
            SCENCE_OBJ_SIDE_VERTICAL = 2 //竖直,道具x朝向Y
        }
        
        public enum EnPhyFlag
        {
            CANT_MOVE = 1, //不可行走
            CANT_USE = 2, //不可用
            Reserve_1,
            Reserve_2,
        }
        
        public const int PhyFlag = (int)EnPhyFlag.CANT_USE | (int)EnPhyFlag.CANT_MOVE;
        public const int WallHeight = 21;
        public enum EnTileMapType
        {
            None = -1,
            PhyMap = ProtocolConst.SCENCE_LAYER_FLOOR,
            WallLeft = ProtocolConst.SCENCE_LAYER_WALL_A,
            WallRight = ProtocolConst.SCENCE_LAYER_WALL_B,
            PhyRoomMap = ProtocolConst.SCENCE_LAYER_ROOMFLOOR,
            Count,
        }
        
        public static int GetPhyFlagByTileName(string name, Vector3Int cellPos)
        {
            // if (name.Equals(SceneConst.TileName.OnlyMove))
            // {
            //     return (int)EnPhyFlag.CANT_USE;
            // }
            return (int)EnPhyFlag.CANT_USE | (int)EnPhyFlag.CANT_MOVE;
        }
    }
}