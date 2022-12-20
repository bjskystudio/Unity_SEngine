using System;
using UnityEngine;

namespace SEngine.Map
{
    public class InitMap : MonoBehaviour
    {

        public void Awake()
        {
        }
        /// <summary>
        /// ????????
        /// </summary>
        /// <returns>?????</returns>
        public void Init(Action<bool> intCallBack)
        {
            var map = this.gameObject;
            var tilePalette = map.GetComponent<TilePalette>();
            var mapMonoBehaviour = map.GetComponent<Map>();
            if (tilePalette && mapMonoBehaviour)
            {
                MapComponent mapComponent;
                var mainGame = this.gameObject; //mainGameService.GetMainGame();
                if (null == mainGame.GetComponent<MapComponent>())
                {
                    mapComponent = new MapComponent();
                }
                else
                {
                    mapComponent = mainGame.GetComponent<MapComponent>();
                }

                mapComponent.Map = map;
                mapComponent.FurnitureGroundGridTile = tilePalette.FurnitureGroundGridTile;
                mapComponent.FurnitureLeftWallGridTile = tilePalette.FurnitureLeftWallGridTile;
                mapComponent.FurnitureRightWallGridTile = tilePalette.FurnitureRightWallGridTile;
                mapComponent.FurnitureRoomGroundGridTile = tilePalette.FurnitureRoomGroundGridTile;
                mapComponent.PhysicGroundTile = tilePalette.PhysicGroundTile;
                mapComponent.PhysicLeftWallTile = tilePalette.PhysicLeftWallTile;
                mapComponent.PhysicRightWallTile = tilePalette.PhysicRightWallTile;
                mapComponent.PhysicRoomGroundTile = tilePalette.PhysicRoomGroundTile;
                
                mapComponent.Floor = mapMonoBehaviour.Floor;
                mapComponent.Wall = mapMonoBehaviour.Wall;
                mapComponent.FloorTilemap = mapMonoBehaviour.FloorTilemap;
                mapComponent.RoomFloorTilemap = mapMonoBehaviour.RoomFloorTilemap;
                mapComponent.WallLeftTilemap = mapMonoBehaviour.WallLeftTilemap;
                mapComponent.WallRightTilemap = mapMonoBehaviour.WallRightTilemap;
                mapComponent.WallDisplayTilemap = mapMonoBehaviour.WallDisplayTilemap;

                if (null == mainGame.GetComponent<MapComponent>())
                {
                    mainGame.AddComponent<MapComponent>();
                }

                //MapService.Instance.InitAll(MapId,mapComponent);
                MapService.Instance.SetmapComponent(mapComponent);
                Debug.Log("MapInit");
                intCallBack?.Invoke(true);
            }else
            {
                intCallBack?.Invoke(false);
            }
        }
    }
}