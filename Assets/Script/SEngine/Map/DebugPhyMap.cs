using UnityEngine;
using UnityEngine.Tilemaps;

namespace SEngine.Map
{
    public class DebugPhyMap : MonoBehaviour
    {
      
        public Tile RedTile;
        public Tile GreenTile;

        public static DebugPhyMap DebugPhy;
        private Tilemap tilemap;

        private void Awake()
        {
            DebugPhy = this;
            //var tilemapObj = transform.Find("HotelGrid/HotelFloorTilemap").gameObject;
            var tilemapObj = transform.Find("LoungeGrid/LoungeFloorTilemap").gameObject;
            if (tilemapObj == null)
            {
                tilemapObj = transform.Find("LoungeGrid/LoungeFloorTilemap").gameObject;
            }
            var debugMapObj = GameObject.Instantiate(tilemapObj, tilemapObj.transform.parent);
            debugMapObj.name = "debugTilemap";
            debugMapObj.SetActive(false);
            tilemap = debugMapObj.GetComponent<Tilemap>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetCellCanUse(Vector3Int pos, bool canUse)
        {
            if (canUse)
            {
                tilemap.SetTile(pos, null);
            }
            else
            {
                tilemap.SetTile(pos, GreenTile);
            }

        }

        public void SetCellCanMove(Vector3Int pos, bool canMove)
        {
            if (canMove)
            {
                tilemap.SetTile(pos, null);
            }
            else
            {
                tilemap.SetTile(pos, RedTile);
            }

        }
    }
}