using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SEngine.Map
{
    public class DebugPhyMap : MonoBehaviour
    {
      
        public Tile RedTile;
        public Tile GreenTile;
        public Tile YellowTile;
        public Tile BlueTile;
        public Tile PurPleTile;
        public Tile PinkTile;
        public static DebugPhyMap DebugPhy;
        private Tilemap tilemap1;
        private Tilemap tilemap2;
       
        private Dictionary<int , Tilemap> tilemapDic = new Dictionary<int, Tilemap>();
        private void Awake()
        {
            DebugPhy = this;
            var tilemapObj1 = transform.Find("$_obj_HotelGrid/HotelFloorTilemap").gameObject;
            var tilemapObj2 = transform.Find("$_obj_LoungeGrid/LoungeFloorTilemap").gameObject;
            
            var debugMapObj2 = GameObject.Instantiate(tilemapObj2, tilemapObj2.transform.parent);
            var debugMapObj1 = GameObject.Instantiate(tilemapObj1, tilemapObj1.transform.parent);
            debugMapObj1.name = "debugHotelTilemap";
            debugMapObj2.name = "debugLoungeTilemap";
            debugMapObj2.SetActive(false);
            debugMapObj1.SetActive(false);
            debugMapObj2.GetComponent<TilemapRenderer>().sortingOrder = 10000;
            debugMapObj1.GetComponent<TilemapRenderer>().sortingOrder = 10000;
            tilemap1 = debugMapObj1.GetComponent<Tilemap>();
            tilemap2 = debugMapObj2.GetComponent<Tilemap>();
            tilemapDic.Add(1000, tilemap1);
            tilemapDic.Add(1001 , tilemap2);
        }
        
        // Update is called once per frame
        void Update()
        {

        }

        public void SetCellCanUse(Vector3Int pos, bool canUse , int mapId)
        {
            var tilemap = tilemapDic[mapId];
            if (canUse)
            {
                tilemap.SetTile(pos, null);
            }
            else
            {
                tilemap.SetTile(pos, GreenTile);
            }

        }

        public void SetCellCanMove(Vector3Int pos, bool canMove,int mapId)
        {
           
            var tilemap = tilemapDic[mapId];
            if (canMove)
            {
                tilemap.SetTile(pos, null);
            }
            else
            {
                tilemap.SetTile(pos, RedTile);
            }
            
        }

        public void SetCellCanStand(Vector3Int pos, bool canMove, int mapId)
        {
            var tilemap = tilemapDic[mapId];
            if (canMove)
            {
                tilemap.SetTile(pos, null);
            }
            else
            {
                tilemap.SetTile(pos, YellowTile);
            }
        }
        
        public void SetCellCanQueue(Vector3Int pos, bool canMove, int mapId)
        {
            var tilemap = tilemapDic[mapId];
            if (canMove)
            {
                tilemap.SetTile(pos, null);
            }
            else
            {
                tilemap.SetTile(pos, BlueTile);
            }
        }
        
        public void SetCellCanInteract(Vector3Int pos, bool canMove, int mapId)
        {
            var tilemap = tilemapDic[mapId];
            if (canMove)
            {
                tilemap.SetTile(pos, null);
            }
            else
            {
                tilemap.SetTile(pos, PurPleTile);
            }
        }
        
        public void SetCellCanCoinPos(Vector3Int pos, bool canMove, int mapId)
        {
            var tilemap = tilemapDic[mapId];
            if (canMove)
            {
                tilemap.SetTile(pos, null);
            }
            else
            {
                tilemap.SetTile(pos, PinkTile);
            }
        }
    }
}