using System;
using System.Collections;
using System.Collections.Generic;
using SEngine.Map;
using SEngine;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SEngine.Map
{
    public class MapService : MonoSingleton<MapService>
    {
        //phyMap
        private int[][] phyPoints = new int[4][];
        private BoundsInt[] mapBounds = new BoundsInt[(int)Enum.EnTileMapType.Count];
        private BoundsInt[] putAreaBounds = new BoundsInt[(int)Enum.EnTileMapType.Count];

        private Tilemap[] phyTilemaps = new Tilemap[(int)Enum.EnTileMapType.Count];

        private GameObject[] phyGameObjs = new GameObject[(int)Enum.EnTileMapType.Count];

        private List<Vector3Int> doorPosList = new List<Vector3Int>();

        public List<Vector3Int> DoorPathPosList = new List<Vector3Int>();

        private List<Vector3Int> waiterBornList = new List<Vector3Int>();

        private Vector3Int extendSize = Vector3Int.zero;

        public static Vector3Int UnUseCellPos = new Vector3Int(-999999, -99999, 0);

        public static Vector3 UnUsePos = new Vector3(-999999, -99999, 0);

        PointList tempPathPoints = new PointList(1000);

        private int[] doorCount;

        public int mapId = 0;

        private Astar pathFinding;

        private MapComponent mapComponent;

        public bool Line = true;
        public MapService()
        {
            doorCount = new int[5];
        }

        /// <summary>
        /// tilemap的bounds
        /// </summary>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public BoundsInt GetMapBounds(Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            return mapBounds[(int)tileMapType];
        }
        /// <summary>
        /// 放置区域的bounds
        /// </summary>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public BoundsInt GetPutAreaBounds(Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            return putAreaBounds[(int)tileMapType];
        }
        /// <summary>
        /// 获取tilemap
        /// </summary>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public Tilemap GetPhyTilemap(Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            return phyTilemaps[(int)tileMapType];
        }

        public Vector3Int WallCellToUseCell(Vector3Int pos, Enum.EnTileMapType tileMapType)
        {
            var newPos = pos;

            if (tileMapType == Enum.EnTileMapType.WallLeft)
            {
                newPos.x -= pos.y - putAreaBounds[(int)tileMapType].yMin;

            }
            else if (tileMapType == Enum.EnTileMapType.WallRight)
            {
                newPos.y -= pos.x - putAreaBounds[(int)tileMapType].xMin;
            }

            return newPos;
        }

        public List<Vector3Int> GetTileCellListFromBounds(BoundsInt bounds, Enum.EnTileMapType tileMapType)
        {
            List<Vector3Int> retList = new List<Vector3Int>();
            foreach (var pos in bounds.allPositionsWithin)
            {
                var newPos = pos;
                if (tileMapType == Enum.EnTileMapType.WallLeft)
                {
                    newPos.x += pos.y;
                }
                else if (tileMapType == Enum.EnTileMapType.WallRight)
                {
                    newPos.x += pos.y;
                }
                retList.Add(newPos);
            }

            return retList;
        }

        public Vector3Int UseCellToWallCell(Vector3Int pos, Enum.EnTileMapType tileMapType)
        {
            var newPos = pos;

            if (tileMapType == Enum.EnTileMapType.WallLeft)
            {
                newPos.x += pos.y - putAreaBounds[(int)tileMapType].yMin;
            }
            else if (tileMapType == Enum.EnTileMapType.WallRight)
            {
                newPos.y += pos.x - putAreaBounds[(int)tileMapType].xMin;
            }

            return newPos;
        }

        /// <summary>
        /// 排队的pos
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector3Int GetDoorPos(int index)
        {
            if (doorPosList.Count == 0)
            {
                return new Vector3Int(-6, -19, 0);
            }

            if (index < 0 || index >= doorPosList.Count)
            {
                return doorPosList[doorPosList.Count - 1];
            }
            return doorPosList[index];
        }
        /// <summary>
        /// 最大排队数
        /// </summary>
        /// <returns></returns>
        public int GetDoorPosListCount()
        {
            if (doorCount != null)
            {
                return doorCount.Length;
            }
            return 0;
        }
        /// <summary>
        /// 随机服务员出生坐标
        /// </summary>
        /// <returns></returns>
        public Vector3Int GetWaiterRanomBornPos()
        {
            int index = UnityEngine.Random.Range(0, waiterBornList.Count);
            return waiterBornList[index];
        }

        /// <summary>
        /// 设置物理层隐藏显示
        /// </summary>
        /// <param name="active"></param>
        /// <param name="tileMapType"></param>
        public void SetPhyActive(bool active, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyGameObjs[(int)tileMapType] == null)
            {
                return;
            }

            phyGameObjs[(int)tileMapType].SetActive(active);
        }

        public Tile GetTile(Enum.EnTileMapType tileMapType, bool physic)
        {
            var mapComponent = this.GetComponent<MapComponent>();//mainGameService.GetMainComponent<MapComponent>();
            Tile tile = null;
            if (null != mapComponent)
            {
                if (tileMapType == Enum.EnTileMapType.PhyMap)
                {
                    if (physic)
                    {
                        tile = mapComponent.PhysicLeftWallTile;
                    }
                    else
                    {
                        tile = mapComponent.FurnitureGroundGridTile;
                    }
                }
                else if (tileMapType == Enum.EnTileMapType.PhyRoomMap)
                {
                    if (physic)
                    {
                        tile = mapComponent.PhysicRoomGroundTile;
                    }
                    else
                    {
                        tile = mapComponent.FurnitureRoomGroundGridTile;
                    }
                }
                else if (tileMapType == Enum.EnTileMapType.WallLeft)
                {
                    if (physic)
                    {
                        tile = mapComponent.PhysicLeftWallTile;
                    }
                    else
                    {
                        tile = mapComponent.FurnitureLeftWallGridTile;
                    }
                }
                else if (tileMapType == Enum.EnTileMapType.WallRight)
                {
                    if (physic)
                    {
                        tile = mapComponent.PhysicRightWallTile;
                    }
                    else
                    {
                        tile = mapComponent.FurnitureRightWallGridTile;
                    }
                }
            }
            return tile;
        }

        public Enum.EnTileMapType GetTileMap(int mapid)
        {
            if (mapid == 1000)
            {
                return Enum.EnTileMapType.PhyMap;
            }
            else if (mapid == 1001)
            {
                return Enum.EnTileMapType.PhyRoomMap;
            }
            return Enum.EnTileMapType.None;
        }

        public int GetTileMap(Enum.EnTileMapType tileMapType)
        {
            if (tileMapType == Enum.EnTileMapType.PhyMap)
            {
                return 1000;
            }
            else if (tileMapType == Enum.EnTileMapType.PhyRoomMap)
            {
                return 1001;
            }
            return 0;
        }
        
        /// <summary>
        /// 判读一个点是否在指定的放置区域内
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public bool CellPosIsInPutArea(Vector3Int pos, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            var bounds = putAreaBounds[(int)tileMapType];

            return pos.x >= bounds.xMin && pos.x <= bounds.xMax && pos.y >= bounds.yMin && pos.y <= bounds.yMax;
        }

        /// <summary>
        /// 判断格子能否移动
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool CellPosCanMove(Vector3Int pos , Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyPoints[(int)tileMapType] == null)
            {
                return false;
            }
            
            pos -= mapBounds[(int)tileMapType].min;

            if (pos.x >= mapBounds[(int)tileMapType].size.x || pos.x < 0)
            {
                return false;
            }

            if (pos.y >= mapBounds[(int)tileMapType].size.y || pos.y < 0)
            {
                return false;
            }

            int index = pos.x + pos.y * mapBounds[(int)tileMapType].size.x;

            if (index >= phyPoints[(int)tileMapType].Length)
            {
                return false;
            }

            return (phyPoints[(int)tileMapType][index] & (int)Enum.EnPhyFlag.CANT_MOVE) == 0;
        }
        /// <summary>
        /// 判断格子是否可用
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public bool CellPosCanUse(Vector3Int pos, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyPoints[(int)tileMapType] == null)
            {
                return false;
            }

            var newPos = UseCellToWallCell(pos, tileMapType);

            newPos -= mapBounds[(int)tileMapType].min;

            if (newPos.x >= mapBounds[(int)tileMapType].size.x || newPos.x < 0)
            {
                return false;
            }

            if (newPos.y >= mapBounds[(int)tileMapType].size.y || newPos.y < 0)
            {
                return false;
            }

            int index = newPos.x + newPos.y * mapBounds[(int)tileMapType].size.x;

            return (phyPoints[(int)tileMapType][index] & (int)Enum.EnPhyFlag.CANT_USE) == 0;
        }
        /// <summary>
        /// 判断坐标是否可用
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public bool WorldPosCanUse(Vector3 pos, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            return CellPosCanUse(WorldPosToCellPos(pos), tileMapType);
        }
        /// <summary>
        /// 判断坐标是否可以移动
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool WorldPosCanMove(Vector3 pos , int mapId = 0 ,Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (mapId == 0)
            {
                tileMapType = Enum.EnTileMapType.PhyMap;
            }
            else
            {
                tileMapType = GetTileMap(mapId);
            }
            return CellPosCanMove(WorldPosToCellPos(pos,mapId,tileMapType));
        }
        /// <summary>
        /// 随机在格子内移动
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="offset"></param>
        /// <param name="isOutDoor"></param>
        /// <returns></returns>
        public Vector3Int GetRanomdCanMovePos(Vector3Int pos, int offset, bool isOutDoor = false , Enum.EnTileMapType enTileMapType = Enum.EnTileMapType.PhyMap)
        {
            int count = 50;
            Vector3Int newPos = pos;
            while (count > 0)
            {
                newPos.x = pos.x + UnityEngine.Random.Range(-offset, offset + 1);
                newPos.y = pos.y + UnityEngine.Random.Range(-offset, offset + 1);

                if ((isOutDoor || newPos.x > 0) && CellPosCanMove(newPos,enTileMapType))
                {
                    break;
                }

                count--;
            }

            return newPos;
        }


        public Vector3Int GetCanMovePos(Vector3Int pos, int offset , Enum.EnTileMapType enTileMapType = Enum.EnTileMapType.PhyMap)
        {
            Vector3Int newPos = new Vector3Int();
            for (int i = -offset; i < offset; i++)
            {
                for (int j = -offset; j < offset; j++)
                {
                    newPos.x = pos.x + i;
                    newPos.y = pos.y + j;
                    if (newPos.x > 0 && CellPosCanMove(newPos,enTileMapType))
                    {
                        return newPos;
                    }
                }
            }

            if (offset > 30)
            {
                return UnUseCellPos;
            }

            offset++;
            return GetCanMovePos(pos, offset , enTileMapType);
        }

        /// <summary>
        /// 找到最近的可以移动世界位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Vector3 FindNearstCanMoveWorldPos(Vector3 pos)
        {
            return CellPosToWorldPos(FindNearstCanMovePos(WorldPosToCellPos(pos)));
        }

        /// <summary>
        /// 找对应格子最近的一个可移动坐标
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Vector3Int FindNearstCanMovePos(Vector3Int pos, Enum.EnTileMapType enTileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyTilemaps[(int)enTileMapType] == null)
            {
                return UnUseCellPos;
            }

            if (CellPosCanMove(pos,enTileMapType))
            {
                return pos;
            }

            int offset = 1;

            return GetCanMovePos(pos, offset , enTileMapType);
        }

        /// <summary>
        /// 根据格子坐标找Tile
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public Tile GetTileByCellPos(Vector3Int pos, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyTilemaps[(int)tileMapType] == null)
            {
                return null;
            }

            var newPos = UseCellToWallCell(pos, tileMapType);

            return phyTilemaps[(int)tileMapType].GetTile<Tile>(newPos);
        }

        /// <summary>
        /// 格子的物理层flag
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public int GetTilePhyFlag(Vector3Int pos, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyPoints[(int)tileMapType] == null)
            {
                return Enum.PhyFlag;
            }

            var newPos = UseCellToWallCell(pos, tileMapType);

            newPos -= mapBounds[(int)tileMapType].min;

            if (newPos.x >= mapBounds[(int)tileMapType].size.x || newPos.x < 0)
            {
                return Enum.PhyFlag;
            }

            if (newPos.y >= mapBounds[(int)tileMapType].size.y || newPos.y < 0)
            {
                return Enum.PhyFlag;
            }

            int index = newPos.x + newPos.y * mapBounds[(int)tileMapType].size.x;

            if (index >= phyPoints[(int)tileMapType].Length)
            {
                return Enum.PhyFlag;
            }

            return phyPoints[(int)tileMapType][index];
        }

        /// <summary>
        /// 设置能不能行走
        /// </summary>
        /// <param name="pos"></param>
        public void SetCellCanMove(Vector3 pos, bool canMove , Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap )
        {
            if (phyPoints[(int)tileMapType] == null)
            {
                return;
            }
            
            var poS = Vector3Int.FloorToInt(pos);
            DebugPhyMap.DebugPhy.SetCellCanMove(poS,canMove,GetTileMap(tileMapType));
            
            //DebugPhyMap.DebugPhy?.SetCellCanMove(poS, canMove);
            
            poS -= mapBounds[(int)tileMapType].min;

            if (poS.x >= mapBounds[(int)tileMapType].size.x || poS.x < 0)
            {
                return;
            }

            if (poS.y >= mapBounds[(int)tileMapType].size.y || poS.y < 0)
            {
                return;
            }

            int index = poS.x + poS.y * mapBounds[(int)tileMapType].size.x;

            if (index >= phyPoints[(int)tileMapType].Length)
            {
                return;
            }

            if (canMove)
            {
                phyPoints[(int)tileMapType][index] &= ~(int)Enum.EnPhyFlag.CANT_MOVE;
            }
            else
            {
                phyPoints[(int)tileMapType][index] |= (int)Enum.EnPhyFlag.CANT_MOVE;
            }
        }

        public void SetCellCanStand(Vector3 pos, bool canMove, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyPoints[(int)tileMapType] == null)
            {
                return;
            }
            
            var poS = Vector3Int.FloorToInt(pos);
            DebugPhyMap.DebugPhy.SetCellCanStand(poS,canMove,GetTileMap(tileMapType));
        }
        
        public void SetCellCanQueue(Vector3 pos, bool canMove, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyPoints[(int)tileMapType] == null)
            {
                return;
            }
            
            var poS = Vector3Int.FloorToInt(pos);
            DebugPhyMap.DebugPhy.SetCellCanQueue(poS,canMove,GetTileMap(tileMapType));
        }
        public void SetCellCanInteract(Vector3 pos, bool canMove, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyPoints[(int)tileMapType] == null)
            {
                return;
            }
            
            var poS = Vector3Int.FloorToInt(pos);
            DebugPhyMap.DebugPhy.SetCellCanInteract(poS,canMove,GetTileMap(tileMapType));
        }
        
        public void SetCellCanCoinPos(Vector3 pos, bool canMove, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyPoints[(int)tileMapType] == null)
            {
                return;
            }
            
            var poS = Vector3Int.FloorToInt(pos);
            DebugPhyMap.DebugPhy.SetCellCanCoinPos(poS,canMove,GetTileMap(tileMapType));
        }
        public void RestPhyPoints()
        {
            for (Enum.EnTileMapType i = Enum.EnTileMapType.PhyMap; i < Enum.EnTileMapType.Count; i++)
            {
                var putBounds = putAreaBounds[(int)i];
                var mapBounds = this.mapBounds[(int)i];
                var phyPoint = this.phyPoints[(int)i];
                var phyTilemap = phyTilemaps[(int)i];
                if (phyPoint != null && putBounds != null)
                {
                    foreach (var point in putBounds.allPositionsWithin)
                    {
                        int index = point.x - mapBounds.xMin + (point.y - mapBounds.yMin) * mapBounds.size.x;
                        phyPoint[index] = 0;

                        //if (mainGameService != null)
                        //{
                        phyTilemap.SetTile(point, GetTile(i, true));
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// 设置摆放
        /// </summary>
        /// <param name="pos"></param>
        public void SetCellCanUse(Vector3Int pos ,bool canUse, int mapId = 0, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (phyTilemaps[(int)tileMapType] == null)
            {
                return;
            }
            //TODO
            DebugPhyMap.DebugPhy?.SetCellCanUse(pos, canUse , GetTileMap(tileMapType));

            var newPos = UseCellToWallCell(pos, tileMapType);

            newPos -= mapBounds[(int)tileMapType].min;

            if (newPos.x >= mapBounds[(int)tileMapType].size.x || newPos.x < 0)
            {
                return;
            }

            if (newPos.y >= mapBounds[(int)tileMapType].size.y || newPos.y < 0)
            {
                return;
            }



            int index = newPos.x + newPos.y * mapBounds[(int)tileMapType].size.x;

            if (index >= phyPoints[(int)tileMapType].Length)
            {
                return;
            }

            if (canUse)
            {
                phyPoints[(int)tileMapType][index] &= ~(int)Enum.EnPhyFlag.CANT_USE;
            }
            else
            {
                phyPoints[(int)tileMapType][index] |= (int)Enum.EnPhyFlag.CANT_USE;
            }
        }

        /// <summary>
        /// 世界坐标转格子坐标
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public Vector3Int WorldPosToCellPos(Vector3 pos, int mapId = 0, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (mapId != 0)
            {
                tileMapType = GetTileMap(mapId);
            }
            
            if (phyTilemaps[(int)tileMapType] == null)
            {
                return UnUseCellPos;
            }
            
            Vector3Int cellPos = phyTilemaps[(int)tileMapType].WorldToCell(Vector3Int.FloorToInt(pos));

            return WallCellToUseCell(cellPos, tileMapType);
        }
        
        /// <summary>
        /// 局部坐标转格子坐标
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public Vector3Int LocalPosToCellPos(Vector3 pos, int mapId = 0 ,Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap )
        {
            if (mapId != 0)
            {
                tileMapType = GetTileMap(mapId);
            }
            
            if (phyTilemaps[(int)tileMapType] == null)
            {
                return UnUseCellPos;
            }

            Vector3Int cellPos = phyTilemaps[(int)tileMapType].LocalToCell(Vector3Int.FloorToInt(pos));

            return WallCellToUseCell(cellPos, tileMapType);
        }
        
        /// <summary>
        /// 格子坐标转世界坐标
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public Vector3 CellPosToWorldPos(Vector3 pos, int mapId = 0 ,Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (mapId != 0)
            {
                tileMapType = GetTileMap(mapId);
            }
            
            if (phyTilemaps[(int)tileMapType] == null)
            {
                return UnUsePos;
            }

            var newPos = UseCellToWallCell(Vector3Int.FloorToInt(pos), tileMapType);

            return phyTilemaps[(int)tileMapType].GetCellCenterWorld(newPos);
        }
        
        /// <summary>
        /// 格子坐标转局部坐标
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public Vector3 CellPosToLocalPos(Vector3 pos, int mapId = 0, Enum.EnTileMapType tileMapType = Enum.EnTileMapType.PhyMap)
        {
            if (mapId != 0)
            {
                tileMapType = GetTileMap(mapId);
            }
            
            if (phyTilemaps[(int)tileMapType] == null)
            {
                return UnUsePos;
            }

            var newPos = UseCellToWallCell(Vector3Int.FloorToInt(pos), tileMapType);

            return phyTilemaps[(int)tileMapType].GetCellCenterLocal(newPos);
        }
        
        /// <summary>
        /// 当前的cell坐标属于那个放置区域
        /// </summary>
        /// <param name="cellPos"></param>
        /// <returns></returns>
        public Enum.EnTileMapType GetCellPosBelongPutArea(Vector3 cellPos , int mapId = 0 )
        {
            var pos = Vector3Int.FloorToInt(cellPos);
            if (mapId == 0)
            {
                if (CellPosIsInPutArea(pos))
                {
                    return Enum.EnTileMapType.PhyMap;
                }
            }
            else
            {
                if (GetTileMap(mapId) == Enum.EnTileMapType.PhyMap)
                {
                    if (CellPosIsInPutArea(pos))
                    {
                        return Enum.EnTileMapType.PhyMap;
                    }
                }
                else
                {
                    for (Enum.EnTileMapType i = Enum.EnTileMapType.PhyMap; i < Enum.EnTileMapType.Count; i++)
                    {
                        if (CellPosIsInPutArea(pos, i))
                        {
                            return i;
                        }
                    }
                }
                
            }
            //TODO
            return Enum.EnTileMapType.None;
        }

        /// <summary>
        /// 设置一个区域内的tile
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="tileMapType"></param>
        /// <returns></returns>
        public void SetTileByBounds(BoundsInt bounds, Enum.EnTileMapType tileMapType, bool physical, Color color)
        {
            Tile tile = GetTile(tileMapType, physical);

            var tilemap = phyTilemaps[(int)tileMapType];

            foreach (var pos in bounds.allPositionsWithin)
            {
                var newPos = UseCellToWallCell(pos, tileMapType);

                tilemap.SetTile(newPos, tile);
                tilemap.SetTileFlags(newPos, TileFlags.None);
                tilemap.SetColor(newPos, color);
            }
        }

        public Enum.EnTileMapType GetWorldPosBelongPutArea(Vector3 pos , int mapId = 0)
        {
            if (mapId == 0)
            {
                if (CellPosIsInPutArea(WorldPosToCellPos(pos)))
                {
                    return Enum.EnTileMapType.PhyMap;
                }
            }
            else
            {
                if (GetTileMap(mapId) == Enum.EnTileMapType.PhyMap)
                {
                    if (CellPosIsInPutArea(WorldPosToCellPos(pos)))
                    {
                        return Enum.EnTileMapType.PhyMap;
                    }
                }
                else
                {
                    for (Enum.EnTileMapType i = Enum.EnTileMapType.PhyMap; i < Enum.EnTileMapType.Count; i++)
                    {
                        if (CellPosIsInPutArea(WorldPosToCellPos(pos,mapId, i), i))
                        {
                            return i;
                        }
                    }
                }
            }
            //TODO
            return Enum.EnTileMapType.None;
        }

        public void SetmapComponent(MapComponent mapComponent)
        {
            this.mapComponent = mapComponent;
        }

        public MapComponent GetmapComponent()
        {
            return this.mapComponent;
        }
        private Action<bool> AstarCallBack;
        public void InitTile(Enum.EnTileMapType enTileMapType)
        {
            //mapId = MapId;
            if (enTileMapType == Enum.EnTileMapType.PhyMap)
            {
                Init(this.mapComponent.FloorTilemap, Enum.EnTileMapType.PhyMap);

            }
            else if (enTileMapType == Enum.EnTileMapType.PhyRoomMap)
            {
                Init(this.mapComponent.RoomFloorTilemap, Enum.EnTileMapType.PhyRoomMap);
            }
        }

        
        private void Init(Tilemap tilemap, Enum.EnTileMapType tileMapType)
        {
            var phyGameObjs = tilemap.gameObject;
            if (phyGameObjs == null)
            {
                return;
            }

            this.phyGameObjs[(int)tileMapType] = phyGameObjs;

            var phyTilemap = phyGameObjs.GetComponent<Tilemap>();

            if (phyTilemap == null)
            {
                return;
            }
            this.phyTilemaps[(int)tileMapType] = phyTilemap;

            var mapBounds = phyTilemap.cellBounds;

            this.mapBounds[(int)tileMapType] = mapBounds;

            var phyPoint = new int[mapBounds.size.x * mapBounds.size.y];

            //putAreaBounds[(int)tileMapType] = new BoundsInt(-38, -49, 0, 81, 71, 0);

            for (int x = mapBounds.xMin; x < mapBounds.xMax; x++)
            {
                for (int y = mapBounds.yMin; y < mapBounds.yMax; y++)
                {
                    int index = x - mapBounds.xMin + (y - mapBounds.yMin) * mapBounds.size.x;

                    Vector3Int cellPos = new Vector3Int(x, y, 0);

                    
                    
                    if (CellPosIsInPutArea(WallCellToUseCell(cellPos, tileMapType), tileMapType))//putAreaBounds[(int)tileMapType].Contains())
                    {
                        phyPoint[index] = 0;

                        if (this.gameObject != null)
                        {
                            phyTilemap.SetTile(cellPos, GetTile(tileMapType, true));
                        }
                    }
                    else
                    {
                        Tile tile = phyTilemap.GetTile<Tile>(cellPos);
                        if (tile != null)
                        {
                            phyPoint[index] = Enum.GetPhyFlagByTileName(tile.name, cellPos);

                            if (phyPoint[index] == Enum.PhyFlag)
                            {
                                phyTilemap.SetTile(cellPos, null);
                            }
                        }
                        else
                        {
                            phyPoint[index] = Enum.PhyFlag;
                        }
                    }
                }
            }

            if (tileMapType == Enum.EnTileMapType.WallLeft)
            {

            }
            else if (tileMapType == Enum.EnTileMapType.WallRight)
            {

            }

            SetPhyActive(false, tileMapType);

            phyPoints[(int)tileMapType] = phyPoint;

            if (tileMapType != Enum.EnTileMapType.PhyMap)
            {
                return;
            }
            //Astar.InitAstar(phyPoint, mapBounds);
            //pathFinding = new Astar(this);
            //InitPath(pathFinding, mapBounds);
            //astarCallBack(true);
        }

        public void InitAstar(int mapId)
        {
            var tileMapType = GetTileMap(mapId);
            var phyPoint = this.phyPoints[(int) tileMapType];
            var phyTilemaps = this.phyTilemaps[(int) tileMapType];
            var phyTilemap = phyTilemaps.GetComponent<Tilemap>();
            var mapBounds = phyTilemap.cellBounds;
            new Astar(phyPoint , mapBounds);
            pathFinding = new Astar(this);
        }
        
        public void InitPath(Astar pathFinding, BoundsInt mapbounds)
        {
            //SetCanMovePoint(9, 6, 14, 6);
            //tempPathPoints.Clear();
            //var startpos = new Vector3Int(11, 5, 0);
            //var endpos = new Vector3Int(7, 11, 0);
            //pathFinding.GetPath(startpos, endpos);
            //pathFinding.GetPathByCell(tempPathPoints, startpos, endpos);
            //pathFinding.CheckIsValuable(tempPathPoints);
        }
        
        // public List<Vector3> GetPath(int mapId, Vector3 startPos, Vector3 endPos, bool isIgnoreCorner = false)
        // {
        //     var tilemap = GetTileMap(mapId);
        //     var start = WorldPosToCellPos(startPos, tilemap);
        //     var end = WorldPosToCellPos(endPos, tilemap);
        //      List<Vector3> pathPoints = new List<Vector3>();
        //     var path = pathFinding.GetPath(tilemap, start, end, isIgnoreCorner);
        //     for (int i = 0; i < path.Count; i++)
        //     {
        //         pathPoints.Add(CellPosToWorldPos(path[i], tilemap));
        //     }
        //
        //     return pathPoints;
        // }
            
        public List<Vector3> GetPath(int mapId, Vector3 startPos, Vector3 endPos, bool isIgnoreCorner = false)
        {
            InitAstar(mapId);
            var tilemap = GetTileMap(mapId);
            var path = pathFinding.GetPath(tilemap,Vector3Int.FloorToInt(startPos) , Vector3Int.FloorToInt(endPos), isIgnoreCorner);
            List<Vector3> temppath = new  List<Vector3>();
            for (int i = 0; i < path.Count; i++)
            {
                temppath.Add(new Vector3(path[i].x , path[i].y , path[i].z));
            }
            if (temppath.Count <= 0)
            {
                //Debug.LogError("InvalidStartPos" + startPos);
                //Debug.LogError("InvalidEndPos" + endPos);
            }
            return temppath;
        }
        
        /// <summary>
        /// 设置不可行走的点
        /// </summary>
        /// <param name="list"></param>
        public void SetCanMovePoint(int mapid , Vector2 pos, Vector2 size)
        {
            var TileMap = GetTileMap(mapid);
            if (TileMap == Enum.EnTileMapType.None)
            {
                return;
            }
            var bounds = new BoundsInt((int)pos.x, (int)pos.y, 1, (int)size.x , (int)size.y , 1);
            var list = GetTileCellListFromBounds(bounds, TileMap);
            for (int i = 0; i < list.Count; i++)
            {
                SetCellCanMove(list[i], false , TileMap);
            }
        }
        /// <summary>
        /// 设置站立点了信息
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        public void SetCanStand(int mapid, Vector2 pos, Vector2 size)
        {
            var TileMap = GetTileMap(mapid);
            if (TileMap == Enum.EnTileMapType.None)
            {
                return;
            }
            var bounds = new BoundsInt((int)pos.x, (int)pos.y, 1, (int)size.x , (int)size.y , 1);
            var list = GetTileCellListFromBounds(bounds, TileMap);
            for (int i = 0; i < list.Count; i++)
            {
                SetCellCanStand(list[i], false , TileMap);
            }
        }
        
        public void SetCanQueue(int mapid, Vector2 pos, Vector2 size)
        {
            var TileMap = GetTileMap(mapid);
            if (TileMap == Enum.EnTileMapType.None)
            {
                return;
            }
            var bounds = new BoundsInt((int)pos.x, (int)pos.y, 1, (int)size.x , (int)size.y , 1);
            var list = GetTileCellListFromBounds(bounds, TileMap);
            for (int i = 0; i < list.Count; i++)
            {
                SetCellCanQueue(list[i], false , TileMap);
            }
        }
        
        public void SetCanInteract(int mapid, Vector2 pos, Vector2 size)
        {
            var TileMap = GetTileMap(mapid);
            if (TileMap == Enum.EnTileMapType.None)
            {
                return;
            }
            var bounds = new BoundsInt((int)pos.x, (int)pos.y, 1, (int)size.x , (int)size.y , 1);
            var list = GetTileCellListFromBounds(bounds, TileMap);
            for (int i = 0; i < list.Count; i++)
            {
                SetCellCanInteract(list[i], false , TileMap);
            }
        }
        
        public void SetCanCoinPos(int mapid, Vector2 pos, Vector2 size)
        {
            var TileMap = GetTileMap(mapid);
            if (TileMap == Enum.EnTileMapType.None)
            {
                return;
            }
            var bounds = new BoundsInt((int)pos.x, (int)pos.y, 1, (int)size.x , (int)size.y , 1);
            var list = GetTileCellListFromBounds(bounds, TileMap);
            for (int i = 0; i < list.Count; i++)
            {
                SetCellCanCoinPos(list[i], false , TileMap);
            }
        }
        /// <summary>
        /// 设置TileMap的区域
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="pos"></param>
        /// <param name="size"></param>
         public void SetPutAreaBounds(int mapId , Vector3 pos , Vector3 size)
         {
            var tileMapType = GetTileMap(mapId);
            if (tileMapType == Enum.EnTileMapType.None)
            {
               return;
            }
            var pos1 = Vector3Int.FloorToInt(pos);
            var size1 = Vector3Int.FloorToInt(size);
            putAreaBounds[(int)tileMapType] = new BoundsInt(pos1.x , pos1.y , pos1.z , size1.x , size1.y , size1.z);
            InitTile(tileMapType);
         }
       
        public Vector3 WallObjectFindMovePos(Vector3 wallWorldPos)
        {
            var cellPos = WallWorldPosToFloorCellPos(wallWorldPos);
            var tileMapType = GetWorldPosBelongPutArea(wallWorldPos);

            int count = 0;
            while (CellPosCanMove(cellPos , tileMapType) == false && count < 50)
            {
                if (tileMapType == Enum.EnTileMapType.WallLeft)
                {
                    cellPos.y--;
                }
                else if (tileMapType == Enum.EnTileMapType.WallRight)
                {
                    cellPos.x--;
                }

                count++;
            }

            return CellPosToWorldPos(cellPos);
        }

        public Vector3Int WallWorldPosToFloorCellPos(Vector3 wallWorldPos)
        {
            var tileMapType = GetWorldPosBelongPutArea(wallWorldPos);

            var wallUseCell = WorldPosToCellPos(wallWorldPos,0, tileMapType);
            var cellPos = wallUseCell;
            if (tileMapType == Enum.EnTileMapType.WallLeft)
            {
                cellPos.y = putAreaBounds[(int)Enum.EnTileMapType.PhyMap].yMax;
            }
            else if (tileMapType == Enum.EnTileMapType.WallRight)
            {
                cellPos.x = putAreaBounds[(int)Enum.EnTileMapType.PhyMap].xMax;
            }


            return cellPos;
        }
    }
}