using UnityEngine;
using UnityEngine.Tilemaps;

namespace SEngine.Map
{
    public class Map : MonoBehaviour
    {
        public GameObject Floor;
        public GameObject Wall;
        public Tilemap SurfaceTilemap;
        public Tilemap FloorTilemap;
        public Tilemap RoomFloorTilemap;
        public Tilemap WallLeftTilemap;
        public Tilemap WallRightTilemap;
        public Tilemap WallDisplayTilemap;
        //public CameraBoundScript CameraBound;
        public GameObject TV;
        public GameObject ExtendBorder;
        public GameObject CafeCar_anchor;
    }
}