using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystemShowcase
{
    public class Inventory : MonoBehaviour
    {
        public Image[] ItemsIcons;
        public Image[] ItemsFrames;
        public TextMeshProUGUI LookAtTextHint;

        public Transform HeadTransform;

        private Color _hihlightedFrameColor = Color.black;
        private Color _defaultFrameColor = Color.white;

        private Item[] Items;

        private int _currentCellIndex;

        private RaycastHit _hit;
        private Item _itemByHit;
        private bool _isItemFound;
        private bool _lastFoundStatus;

        private bool _isWillToTakeItem;
        private bool _lastTakeWillStatus;

        private float _wheelInput;

        private bool _isWillToDropItem;
        private bool _lastDropWillStatus;

        void Awake()
        {
            Items = new Item[ItemsIcons.Length];
        }

        void Start()
        {
            HighlighChosenItemFrameInHUD();
        }

        void Update()
        {
            CheckInputToTakeItem();
            ShootRayToFindItem();
            GrabItemProcess();

            CheckInputToChooseItem();

            CheckInputToDropItem();
            ThrowItemProcess();
        }

        private void CheckInputToTakeItem()
        {
            _isWillToTakeItem = Input.GetKey(KeyCode.E);

            CheckHoldOfTakeWill();
        }

        private void CheckHoldOfTakeWill()
        {
            if (_isWillToTakeItem)
            {
                if (_lastTakeWillStatus)  // holding will, not first
                {
                    _isWillToTakeItem = false;
                }
                else  // first will
                {
                    _lastTakeWillStatus = true;
                }
            }
            else  // no will at all
            {
                _lastTakeWillStatus = false;
            }
        }

        private void ShootRayToFindItem()
        {
            if (IsRayHitSomething())
            {
                CheckForItemHit();
                OutputItemNameInHUDByLokingAt();
            }
        }

        private bool IsRayHitSomething()
        {
            return
                Physics.SphereCast(
                    HeadTransform.position,
                    0.1f,
                    HeadTransform.forward,
                    out _hit,
                    3.0f);
        }

        private void CheckForItemHit()
        {
            _isItemFound = 
                _hit.transform.TryGetComponent(out _itemByHit);
        }

        private void OutputItemNameInHUDByLokingAt()
        {
            if (_isItemFound != _lastFoundStatus)
            {
                if (_isItemFound)
                {
                    LookAtTextHint.text = _itemByHit.ItemData.Name;
                }
                else
                {
                    LookAtTextHint.text = string.Empty;
                }
            }

            _lastFoundStatus = _isItemFound;
        }

        private void GrabItemProcess()
        {
            if (IsPossibleToGrabItem())
            {
                DisableItem();
                PutItemToInventory();
                ChangeItemStatusInHUDOnTake();
            }
        }

        private bool IsPossibleToGrabItem()
        {
            return
                _isItemFound &&
                _isWillToTakeItem &&
                Items[_currentCellIndex] == null;
        }

        private void DisableItem()
        {
            _itemByHit.gameObject.SetActive(false);
        }

        private void PutItemToInventory()
        {
            Items[_currentCellIndex] = _itemByHit;
        }

        private void ChangeItemStatusInHUDOnTake()
        {
            ItemsIcons[_currentCellIndex].sprite = 
                _itemByHit.ItemData.Icon;
            ItemsIcons[_currentCellIndex].gameObject.SetActive(true);
        }

        private void CheckInputToChooseItem()
        {
            _wheelInput = Input.GetAxisRaw("Mouse ScrollWheel");
            if (_wheelInput != 0)
            {
                RestoreDefaultColorForNotAleradyHighlightedItemFrame();
                CalculateCurrentItemIndex();
                HighlighChosenItemFrameInHUD();
            }
        }

        private void RestoreDefaultColorForNotAleradyHighlightedItemFrame()
        {
            ItemsFrames[_currentCellIndex].color = _defaultFrameColor;
        }

        private void CalculateCurrentItemIndex()
        {
            int wheelInputInt = _wheelInput > 0 ? 1 : -1;
            _currentCellIndex += wheelInputInt;
            if (_currentCellIndex < 0)
            {
                _currentCellIndex = ItemsFrames.Length - 1;
            }
            else if (_currentCellIndex >= ItemsFrames.Length)
            {
                _currentCellIndex = 0;
            }
        }

        private void HighlighChosenItemFrameInHUD()
        {
            ItemsFrames[_currentCellIndex].color = _hihlightedFrameColor;
        }

        private void CheckInputToDropItem()
        {
            _isWillToDropItem = Input.GetKey(KeyCode.G);

            CheckHoldOfDropWill();
        }

        private void CheckHoldOfDropWill()
        {
            if (_isWillToDropItem)
            {
                if (_lastDropWillStatus)  // holding will, not first
                {
                    _lastDropWillStatus = false;
                }
                else  // first will
                {
                    _lastDropWillStatus = true;
                }
            }
            else  // no will at all
            {
                _lastDropWillStatus = false;
            }
        }

        private void ThrowItemProcess()
        {
            if (IsPossibleToThrowItem())
            {
                EnableItem();
                ThrowItemFromInventory();
                ChangeItemStatusInHUDOnDrop();
            }
        }

        private bool IsPossibleToThrowItem()
        {
            return
                _isWillToDropItem &&
                Items[_currentCellIndex] != null;
        }

        private void EnableItem()
        {
            Items[_currentCellIndex].gameObject.SetActive(true);
        }

        private void ThrowItemFromInventory()
        {
            Items[_currentCellIndex].ResetItemAndSetItsPosition(
                transform.position + 
                transform.forward * 2 + 
                transform.up * 2);

            Items[_currentCellIndex] = null;
        }

        private void ChangeItemStatusInHUDOnDrop()
        {
            ItemsIcons[_currentCellIndex].sprite = null;
            ItemsIcons[_currentCellIndex].gameObject.SetActive(false);
        }
    }
}
