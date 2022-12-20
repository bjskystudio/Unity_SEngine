---===================== Author Qcbf 这是自动生成的代码 =====================

---@class SEngine.Map.MapService : SEngine.MonoSingleton
---@field public DoorPathPosList System.Collections.Generic.List
---@field public mapId int32
---@field static UnUseCellPos UnityEngine.Vector3Int
---@field static UnUsePos UnityEngine.Vector3
local MapService = {}

---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.BoundsInt
function MapService:GetMapBounds(tileMapType) end

---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.BoundsInt
function MapService:GetPutAreaBounds(tileMapType) end

---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Tilemaps.Tilemap
function MapService:GetPhyTilemap(tileMapType) end

---@param pos UnityEngine.Vector3Int
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3Int
function MapService:WallCellToUseCell(pos,tileMapType) end

---@param bounds UnityEngine.BoundsInt
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return System.Collections.Generic.List
function MapService:GetTileCellListFromBounds(bounds,tileMapType) end

---@param pos UnityEngine.Vector3Int
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3Int
function MapService:UseCellToWallCell(pos,tileMapType) end

---@param index int32
---@return UnityEngine.Vector3Int
function MapService:GetDoorPos(index) end

---@return int32
function MapService:GetDoorPosListCount() end

---@return UnityEngine.Vector3Int
function MapService:GetWaiterRanomBornPos() end

---@param active System.Boolean
---@param tileMapType SEngine.Map.Enum.EnTileMapType
function MapService:SetPhyActive(active,tileMapType) end

---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@param physic System.Boolean
---@return UnityEngine.Tilemaps.Tile
function MapService:GetTile(tileMapType,physic) end

---@param mapid int32
---@return SEngine.Map.Enum.EnTileMapType
function MapService:GetTileMap(mapid) end

---@param pos UnityEngine.Vector3Int
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return System.Boolean
function MapService:CellPosIsInPutArea(pos,tileMapType) end

---@param pos UnityEngine.Vector3Int
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return System.Boolean
function MapService:CellPosCanMove(pos,tileMapType) end

---@param pos UnityEngine.Vector3Int
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return System.Boolean
function MapService:CellPosCanUse(pos,tileMapType) end

---@param pos UnityEngine.Vector3
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return System.Boolean
function MapService:WorldPosCanUse(pos,tileMapType) end

---@param pos UnityEngine.Vector3
---@param mapId int32
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return System.Boolean
function MapService:WorldPosCanMove(pos,mapId,tileMapType) end

---@param pos UnityEngine.Vector3Int
---@param offset int32
---@param isOutDoor System.Boolean
---@param enTileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3Int
function MapService:GetRanomdCanMovePos(pos,offset,isOutDoor,enTileMapType) end

---@param pos UnityEngine.Vector3Int
---@param offset int32
---@param enTileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3Int
function MapService:GetCanMovePos(pos,offset,enTileMapType) end

---@param pos UnityEngine.Vector3
---@return UnityEngine.Vector3
function MapService:FindNearstCanMoveWorldPos(pos) end

---@param pos UnityEngine.Vector3Int
---@param enTileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3Int
function MapService:FindNearstCanMovePos(pos,enTileMapType) end

---@param pos UnityEngine.Vector3Int
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Tilemaps.Tile
function MapService:GetTileByCellPos(pos,tileMapType) end

---@param pos UnityEngine.Vector3Int
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return int32
function MapService:GetTilePhyFlag(pos,tileMapType) end

---@param pos UnityEngine.Vector3
---@param canMove System.Boolean
---@param tileMapType SEngine.Map.Enum.EnTileMapType
function MapService:SetCellCanMove(pos,canMove,tileMapType) end

function MapService:RestPhyPoints() end

---@param pos UnityEngine.Vector3Int
---@param canUse System.Boolean
---@param mapId int32
---@param tileMapType SEngine.Map.Enum.EnTileMapType
function MapService:SetCellCanUse(pos,canUse,mapId,tileMapType) end

---@param pos UnityEngine.Vector3
---@param mapId int32
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3Int
function MapService:WorldPosToCellPos(pos,mapId,tileMapType) end

---@param pos UnityEngine.Vector3
---@param mapId int32
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3Int
function MapService:LocalPosToCellPos(pos,mapId,tileMapType) end

---@param pos UnityEngine.Vector3
---@param mapId int32
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3
function MapService:CellPosToWorldPos(pos,mapId,tileMapType) end

---@param pos UnityEngine.Vector3
---@param mapId int32
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@return UnityEngine.Vector3
function MapService:CellPosToLocalPos(pos,mapId,tileMapType) end

---@param cellPos UnityEngine.Vector3
---@param mapId int32
---@return SEngine.Map.Enum.EnTileMapType
function MapService:GetCellPosBelongPutArea(cellPos,mapId) end

---@param bounds UnityEngine.BoundsInt
---@param tileMapType SEngine.Map.Enum.EnTileMapType
---@param physical System.Boolean
---@param color UnityEngine.Color
function MapService:SetTileByBounds(bounds,tileMapType,physical,color) end

---@param pos UnityEngine.Vector3
---@param mapId int32
---@return SEngine.Map.Enum.EnTileMapType
function MapService:GetWorldPosBelongPutArea(pos,mapId) end

---@param mapComponent SEngine.Map.MapComponent
function MapService:SetmapComponent(mapComponent) end

---@return SEngine.Map.MapComponent
function MapService:GetmapComponent() end

---@param enTileMapType SEngine.Map.Enum.EnTileMapType
function MapService:InitTile(enTileMapType) end

---@param mapId int32
function MapService:InitAstar(mapId) end

---@param pathFinding SEngine.Map.Astar
---@param mapbounds UnityEngine.BoundsInt
function MapService:InitPath(pathFinding,mapbounds) end

---@param mapId int32
---@param startPos UnityEngine.Vector3
---@param endPos UnityEngine.Vector3
---@param isIgnoreCorner System.Boolean
---@return System.Collections.Generic.List
function MapService:GetPath(mapId,startPos,endPos,isIgnoreCorner) end

---@param mapid int32
---@param pos UnityEngine.Vector2
---@param size UnityEngine.Vector2
function MapService:SetCanMovePoint(mapid,pos,size) end

---@param mapId int32
---@param pos UnityEngine.Vector3
---@param size UnityEngine.Vector3
function MapService:SetPutAreaBounds(mapId,pos,size) end

---@param wallWorldPos UnityEngine.Vector3
---@return UnityEngine.Vector3
function MapService:WallObjectFindMovePos(wallWorldPos) end

---@param wallWorldPos UnityEngine.Vector3
---@return UnityEngine.Vector3Int
function MapService:WallWorldPosToFloorCellPos(wallWorldPos) end

return MapService
