using UnityEngine;
using UnityEngine.Tilemaps;

namespace SEngine.Map
{
    public class MapComponent : MonoBehaviour
    {
        public GameObject Map;
        public Tile FurnitureGroundGridTile;
        public Tile FurnitureRoomGroundGridTile;
        public Tile FurnitureLeftWallGridTile;
        public Tile FurnitureRightWallGridTile;
        public Tile PhysicGroundTile;
        public Tile PhysicRoomGroundTile;
        public Tile PhysicLeftWallTile;
        public Tile PhysicRightWallTile;
        
        
        public GameObject Floor;
        public GameObject Wall;
        public Tilemap FloorTilemap;
        public Tilemap RoomFloorTilemap;
        public Tilemap WallLeftTilemap;
        public Tilemap WallRightTilemap;
        public Tilemap WallDisplayTilemap;
        public bool MapOperating;
        public bool CanMoveMap = true;
    }
}