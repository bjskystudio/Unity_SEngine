using SEngine.Map;
using SEngine;
using UnityEngine;

namespace Script.SEngine.Furniture
{
    public class FurnitureUtility : MonoBehaviour
    {
        /*
        public static (int, int, int, int) GetBoundInfo(int rotateType, FurnitureData furnitureData)
        {
            int xLength = 0, yLength = 0, xOffset = 0, yOffset = 0;
            if (rotateType == ProtocolConst.SCENCE_OBJ_SIDE_VERTICAL)
            {
                xLength = furnitureData.X;
                yLength = furnitureData.Y;
                xOffset = furnitureData.OffestX;
                yOffset = furnitureData.OffestY;
            }
            else if (rotateType == ProtocolConst.SCENCE_OBJ_SIDE_HORIZON)
            {
                xLength = furnitureData.Y;
                yLength = furnitureData.X;
                xOffset = furnitureData.OffestY;
                yOffset = furnitureData.OffestX;
            }
            return (xLength, yLength, xOffset, yOffset);
        }
        
        public static BoundsInt GetBounds(Vector3Int cellPos, int rotateType, FurnitureData furnitureData)
        {
            (int xLength, int yLength, int xOffset, int yOffset) = GetBoundInfo(rotateType, furnitureData);
            return new BoundsInt(cellPos.x + xOffset, cellPos.y + yOffset, cellPos.z, xLength, yLength, 1);
        }
        
        public static BoundsInt GetBounds(GameObject entity, int rotateType, Vector3Int cellPos, Enum.EnPhyFlag tilemapFlag)
        {
            if (tilemapFlag == Enum.EnPhyFlag.CANT_MOVE)
            {
                var furniturePhysical = entity.GetComponent<FurnitureComponent>().FurnitureData.FurniturePhysical;
                return GetBounds(cellPos, rotateType, furniturePhysical);
            }
            else if (tilemapFlag == Enum.EnPhyFlag.CANT_USE)
            {
                var furnitureRenderer = entity.GetComponent<FurnitureComponent>().FurnitureData.FurnitureRenderer;
                return GetBounds(cellPos, rotateType, furnitureRenderer);
            }
            return new BoundsInt();
        }
        */
    }
}