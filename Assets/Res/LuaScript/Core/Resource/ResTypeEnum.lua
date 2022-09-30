---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by asd.
--- DateTime: 2022/9/30 17:25
---
---@class ResTypeEnum @资源类型枚举
---@field eNone string @无
---@field ePrefab string @游戏对象
---@field eTexture string @纹理
---@field eAudioClip string @音频
---@field eAnimationClip string @动画
---@field eText string @文本文件
---@field eAtlasSprite string @图集中的精灵
---@field eSprite string @零散的精灵
---@field eMaterial string @材质球
---@field eTMPFont string @TMP字体文件
---@field eFont string @字体文件
---@field eScriptableObject string @ScriptableObject（如ResourceLoadConfig文件）
local ResTypeEnum = {
    eNone = "eNone",
    ePrefab = "ePrefab",
    eTexture = "eTexture",
    eAudioClip = "eAudioClip",
    eAnimationClip = "eAnimationClip",
    eText = "eText",
    eAtlasSprite = "eAtlasSprite",
    eSprite = "eSprite",
    eMaterial = "eMaterial",
    eTMPFont = "eTMPFont",
    eFont = "eFont",
    eScriptableObject = "eScriptableObject",
}
---@type ResTypeEnum @资源类型枚举
_G.ResTypeEnum = ResTypeEnum

---@return ResTypeEnum @资源类型枚举
return ResTypeEnum