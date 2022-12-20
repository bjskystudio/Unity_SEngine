---===================== Author Qcbf 这是自动生成的代码 =====================

---@class SuperScrollView.LoopListView2 : UnityEngine.MonoBehaviour
---@field public ArrangeType SuperScrollView.ListItemArrangeType
---@field public ItemPrefabDataList System.Collections.Generic.List
---@field public ItemList System.Collections.Generic.List
---@field public IsVertList System.Boolean
---@field public ItemTotalCount int32
---@field public ContainerTrans UnityEngine.RectTransform
---@field public ScrollRect UnityEngine.UI.ScrollRect
---@field public IsDraging System.Boolean
---@field public ItemSnapEnable System.Boolean
---@field public SupportScrollBar System.Boolean
---@field public SnapMoveDefaultMaxAbsVec number
---@field public ShownItemCount int32
---@field public ViewPortSize number
---@field public ViewPortWidth number
---@field public ViewPortHeight number
---@field public CurSnapNearestItemIndex int32
---@field public mOnBeginDragAction System.Action
---@field public mOnDragingAction System.Action
---@field public mOnEndDragAction System.Action
---@field public mOnSnapItemFinished System.Action
---@field public mOnSnapNearestChanged System.Action
local LoopListView2 = {}

---@param prefabName string
---@return SuperScrollView.ItemPrefabConfData
function LoopListView2:GetItemPrefabConfData(prefabName) end

---@param prefabName string
function LoopListView2:OnItemPrefabChanged(prefabName) end

---@param itemTotalCount int32
---@param onGetItemByIndex System.Func
---@param initParam SuperScrollView.LoopListViewInitParam
function LoopListView2:InitListView(itemTotalCount,onGetItemByIndex,initParam) end

---@param resetPos System.Boolean
function LoopListView2:ResetListView(resetPos) end

---@param itemCount int32
---@param resetPos System.Boolean
function LoopListView2:SetListItemCount(itemCount,resetPos) end

---@param itemIndex int32
---@return SuperScrollView.LoopListViewItem2
function LoopListView2:GetShownItemByItemIndex(itemIndex) end

---@param itemIndex int32
---@return SuperScrollView.LoopListViewItem2
function LoopListView2:GetShownItemNearestItemIndex(itemIndex) end

---@param index int32
---@return SuperScrollView.LoopListViewItem2
function LoopListView2:GetShownItemByIndex(index) end

---@param index int32
---@return SuperScrollView.LoopListViewItem2
function LoopListView2:GetShownItemByIndexWithoutCheck(index) end

---@param item SuperScrollView.LoopListViewItem2
---@return int32
function LoopListView2:GetIndexInShownItemList(item) end

---@param action System.Action
---@param param System.Object
function LoopListView2:DoActionForEachShownItem(action,param) end

---@param itemPrefabName string
---@return SuperScrollView.LoopListViewItem2
function LoopListView2:NewListViewItem(itemPrefabName) end

---@param itemIndex int32
function LoopListView2:OnItemSizeChanged(itemIndex) end

---@param itemIndex int32
function LoopListView2:RefreshItemByItemIndex(itemIndex) end

function LoopListView2:FinishSnapImmediately() end

---@param itemIndex int32
---@param offset number
function LoopListView2:MovePanelToItemIndex(itemIndex,offset) end

function LoopListView2:RefreshAllShownItem() end

---@param firstItemIndex int32
function LoopListView2:RefreshAllShownItemWithFirstIndex(firstItemIndex) end

---@param firstItemIndex int32
---@param pos UnityEngine.Vector3
function LoopListView2:RefreshAllShownItemWithFirstIndexAndPos(firstItemIndex,pos) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopListView2:OnBeginDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopListView2:OnEndDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopListView2:OnDrag(eventData) end

---@param item SuperScrollView.LoopListViewItem2
---@param corner SuperScrollView.ItemCornerEnum
---@return UnityEngine.Vector3
function LoopListView2:GetItemCornerPosInViewPort(item,corner) end

function LoopListView2:UpdateAllShownItemSnapData() end

function LoopListView2:ClearSnapData() end

---@param itemIndex int32
---@param moveMaxAbsVec number
function LoopListView2:SetSnapTargetItemIndex(itemIndex,moveMaxAbsVec) end

function LoopListView2:ForceSnapUpdateCheck() end

---@param distanceForRecycle0 number
---@param distanceForRecycle1 number
---@param distanceForNew0 number
---@param distanceForNew1 number
function LoopListView2:UpdateListView(distanceForRecycle0,distanceForRecycle1,distanceForNew0,distanceForNew1) end

return LoopListView2
