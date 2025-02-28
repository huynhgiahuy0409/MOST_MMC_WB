using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.Drawing.Control;
using Tsb.Fontos.Win.FingerScroll;
using Tsb.Fontos.Win.Forms;

namespace Tsb.Catos.Cm.Mobile.Common.Controls.ItemList
{
    public delegate void ItemButtonClickedEventHandler(object sender, ItemListViewEventArgs e);

    public partial class ItemListControl : DrawUserControl
    {
        #region CONST & FIELD AREA ********************************************
        private const String BUTTON_NAME_PREFIX = "btn";
        private String _buttonListCustomStyleName = "biz_default_btn_list";
        private String _buttonListSelectedCustomStyleName = "biz_selected_btn_list";
        private string _scrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
        private int _verticalScrollWidth = 40;

        private int _maxColumnCount = 7;
        private int _maxRowCount = 8;
        private int _gapBetweenControls = 10;
        private BaseItemsList<ItemListControlItem> _itemList;
        private TButton[] _buttons;
        private Control _buttonParent;
        private Color _mainBackColor = SystemColors.Control;
        public event ItemButtonClickedEventHandler ItemButton_Clicked;
        private string _selectedItemCode = "";
        private Dictionary<string, string> _selectedItemCodeList = new Dictionary<string, string>(); // added by jaeok (2020.07.14) Mantis 108025: [YQ] 필터 중복으로 선택 가능하도록 요청
        private bool _useMultipleSelection = false; // added by YoungOk Kim (2019.07.03) - Mantis 90787: [YQ] GC 복수 선택
        private bool _useActivatedInAllBay = false; //added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        private IList<string> _actiVatedBayList = null;//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        private string _buttonListBlockBayActivated = "biz_blockbay_Activated_btn_list";//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        private int _blockBayActivatedCount = 0;//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        private bool _useSortEquipmentGroup = false; // added by kimxyuhwan(23.11.28) [PC18_UP] 0163543: YQ 작업 리스트 정렬 로직 개선

        private DefaultFingerScrollHandler _fingerScrollHdl;
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************
        public int MaxColumnCount
        {
            get
            {
                return this._maxColumnCount;
            }
            set
            {
                this._maxColumnCount = value;
            }
        }

        public int MaxRowCount
        {
            get
            {
                return this._maxRowCount;
            }
            set
            {
                this._maxRowCount = value;
            }
        }

        public int GapBetweenControls
        {
            get
            {
                return this._gapBetweenControls;
            }
            set
            {
                this._gapBetweenControls = value;
            }
        }

        public BaseItemsList<ItemListControlItem> ItemList
        {
            get
            {
                return this._itemList;
            }
            set
            {
                this._itemList = value;
                // deleted by jaeok (2016.09.12) Mantis 54439: RMRC-009 - YTs in YT selection screen hidden
                //DrawButtons();
                //InitScrollBar();
                this.Invalidate(); // added by jaeok (2016.09.29) bug fix (refresh)
            }
        }

        public int ButtonHeight
        {
            get
            {
                int totalHeight = _buttonParent.Parent.Height;
                if (_maxRowCount == 0)
                {
                    return totalHeight;
                }

                return (totalHeight - (_gapBetweenControls * (_maxRowCount - 1))) / _maxRowCount;
            }
        }

        public int ButtonWidth
        {
            get
            {
                int totalWidth = _buttonParent.Parent.Width;

                if (_maxColumnCount == 0)
                {
                    return totalWidth;
                }

                return (totalWidth - (_gapBetweenControls * (_maxColumnCount))) / _maxColumnCount;
            }
        }

        public String ButtonListCustomStyleName
        {
            get
            {
                return this._buttonListCustomStyleName;
            }
            set
            {
                this._buttonListCustomStyleName = value;
            }
        }

        public String ButtonListSelectedCustomStyleName
        {
            get
            {
                return this._buttonListSelectedCustomStyleName;
            }
            set
            {
                this._buttonListSelectedCustomStyleName = value;
            }
        }

        public Color MainBackColor
        {
            get
            {
                return this._mainBackColor;
            }
            set
            {
                this._mainBackColor = value;
                this.pnlInput.BackColor = this._mainBackColor;
                this.pnlButtonList.BackColor = this._mainBackColor;
            }
        }

        public string ScrollCustomStyleName
        {
            get { return this._scrollCustomStyleName; }
            set { this._scrollCustomStyleName = value; }
        }

        public int VerticalScrollWidth
        {
            get { return this._verticalScrollWidth; }
            set { this._verticalScrollWidth = value; }
        }

        public bool ScrollVisible
        {
            get { return this.scrButtonList.Visible; }
            set { this.scrButtonList.Visible = value; }
        }

        public bool UseMultipleSelection
        {
            get { return this._useMultipleSelection; }
            set { this._useMultipleSelection = value; }
        }
        public bool UseSortEquipmentGroup
        {
            get { return this._useSortEquipmentGroup; }
            set { this._useSortEquipmentGroup = value; }
        }

        //added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        public bool UseActivatedInAllBay
        {
            get { return this._useActivatedInAllBay; }
            set { this._useActivatedInAllBay = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************
        public ItemListControl()
        {
            InitializeComponent();
            this.InitData();
            this.InitControl();
            this.AddEventHandler();

            _fingerScrollHdl = new DefaultFingerScrollHandler(this, null, this.scrButtonList);
            _fingerScrollHdl.BindingScroll();
        }

        private void AddEventHandler()
        {
            this.scrButtonList.ValueChanged += new EventHandler(scrButtonList_ValueChanged);
            this.Disposed += new EventHandler(ItemListControl_Disposed);
            this.Paint += new PaintEventHandler(ItemListControl_Paint);
        }

        private void InitData()
        {
            _buttonParent = this.pnlButtonList;
        }

        private void InitControl()
        {
            this.InitScrollBar();
            this.ResizeScroll();
        }

        private void InitScrollBar()
        {
            if (this._itemList == null)
            {
                this.scrButtonList.Maximum = _buttonParent.Height + (_gapBetweenControls * 6) - (_buttonParent.Parent.Height / 2);
                this.scrButtonList.SmallChange = ButtonWidth + _gapBetweenControls;
                this.scrButtonList.LargeChange = (ButtonWidth + _gapBetweenControls) * 2;
            }
            else // added by YoungOk Kim (2018.11.26) - Mantis 87547: YQ can confirm YO job without YT
            {
                int rowCnt = Convert.ToInt32(Math.Ceiling((double)_itemList.Count / MaxColumnCount));
                bool isNeedScroll = rowCnt > MaxRowCount;
                if (isNeedScroll)
                {
                    this.scrButtonList.Maximum = (ButtonHeight + _gapBetweenControls) * rowCnt;
                    this.scrButtonList.SmallChange = ButtonHeight + _gapBetweenControls;

                    int overHeight = (rowCnt - MaxRowCount) * (ButtonHeight + _gapBetweenControls);
                    int largeChange = this.scrButtonList.Maximum - overHeight;
                    this.scrButtonList.LargeChange = largeChange;
                }
                else
                {
                    this.scrButtonList.Maximum = this.pnlInput.Height;
                    this.scrButtonList.SmallChange = this.scrButtonList.Maximum + 1;
                    this.scrButtonList.LargeChange = this.scrButtonList.Maximum + 1;
                }
            }
        }

        public void ResizeScroll()
        {
            this.scrButtonList.CustomStyleName = this.ScrollCustomStyleName;
            this.pnlScroll.Width = this.VerticalScrollWidth;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region EVENTHANDLER AREA *********************************************
        private void scrButtonList_ValueChanged(object sender, EventArgs args)
        {
            this._buttonParent.Location = new Point(0, (this.scrButtonList.Value) * -1);
        }

        private void ItemListControl_Disposed(object sender, EventArgs e)
        {
            if (_fingerScrollHdl != null)
            {
                _fingerScrollHdl.Dispose();
            }
        }

        private void ItemListControl_Paint(object sender, EventArgs e) // added by jaeok (2016.09.12) Mantis 54439: RMRC-009 - YTs in YT selection screen hidden
        {
            try
            {
                DrawButtons();
                //InitScrollBar();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        void ItemListView_Click(object sender, EventArgs e)
        {
            TButton clickedButton = sender as TButton;

            try
            {
                if (UseMultipleSelection == true) // added by YoungOk Kim (2019.07.03) - Mantis 90787: [YQ] GC 복수 선택
                {
                    bool isAlreadySelected = false;

                    List<TButton> list = clickedButton.Parent.Controls.OfType<TButton>().ToList();
                    var button = list.Where(p => p == clickedButton && p.CustomStyleName == ButtonListSelectedCustomStyleName).FirstOrDefault();
                    if (button != null)
                    {
                        isAlreadySelected = true;
                    }

                    if (isAlreadySelected == true)
                    {
                        clickedButton.ChangeCustomStyle(ButtonListCustomStyleName);
                    }
                    else
                    {
                        clickedButton.ChangeCustomStyle(ButtonListSelectedCustomStyleName);
                    }

                    if (ItemButton_Clicked != null)
                    {
                        string codeName = string.Empty;
                        if (clickedButton.Name.Length > BUTTON_NAME_PREFIX.Length)
                        {
                            codeName = clickedButton.Name.Substring(BUTTON_NAME_PREFIX.Length);
                        }

                        object selectedItem = null;
                        var item = _itemList.Where(p => p.Code.Equals(clickedButton.Tag)).FirstOrDefault();
                        if (item != null)
                        {
                            selectedItem = item.CodeItem;
                        }
                        // raise Event
                        ItemButton_Clicked(sender, new ItemListViewEventArgs(clickedButton.Tag) { SelectedItemCodeName = codeName, SelectedItem = selectedItem, IsSelected = (isAlreadySelected == false) });
                    }

                    _selectedItemCode = clickedButton.Tag as string;

                    // added by jaeok (2020.07.14) Mantis 108025: [YQ] 필터 중복으로 선택 가능하도록 요청
                    if (_selectedItemCodeList.ContainsKey(clickedButton.Tag as string) == false)
                    {
                        _selectedItemCodeList.Add(clickedButton.Tag as string, clickedButton.Tag as string);
                    }
                }
                else
                {
                    // set default color
                    List<TButton> list = clickedButton.Parent.Controls.OfType<TButton>().ToList();

                    if (this.UseActivatedInAllBay == false)//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
                    {
                        foreach (TButton btn in list)
                        {
                            btn.ChangeCustomStyle(ButtonListCustomStyleName);
                        }
                    }
                    else
                    {
                        int checkCount = 0;

                        if (_actiVatedBayList == null)
                        {
                            _actiVatedBayList = new List<string>();
                        }

                        foreach (TButton btn in list)
                        {
                            if (_blockBayActivatedCount == 0)
                            {
                                if (btn.CustomStyleName.Equals(_buttonListBlockBayActivated))
                                {
                                    _actiVatedBayList.Add(btn.Name);
                                }
                            }

                            for (int i = 0; i < _actiVatedBayList.Count; i++)
                            {

                                if (btn.Name.Equals(_actiVatedBayList[i]))
                                {
                                    btn.ChangeCustomStyle(_buttonListBlockBayActivated);
                                    break;
                                }

                                checkCount++;
                            }

                            if (checkCount >= _actiVatedBayList.Count)
                            {
                                btn.ChangeCustomStyle(ButtonListCustomStyleName);
                            }
                        }

                        _blockBayActivatedCount = _actiVatedBayList.Count;
                    }

                    // set color for selected Button
                    clickedButton.ChangeCustomStyle(ButtonListSelectedCustomStyleName);

                    if (ItemButton_Clicked != null)
                    {
                        string codeName = string.Empty;
                        if (clickedButton.Name.Length > BUTTON_NAME_PREFIX.Length)
                        {
                            codeName = clickedButton.Name.Substring(BUTTON_NAME_PREFIX.Length);
                        }

                        object selectedItem = null;
                        var item = _itemList.Where(p => p.Code.Equals(clickedButton.Tag)).FirstOrDefault();
                        if (item != null)
                        {
                            selectedItem = item.CodeItem;
                        }
                        // raise Event
                        ItemButton_Clicked(sender, new ItemListViewEventArgs(clickedButton.Tag) { SelectedItemCodeName = codeName, SelectedItem = selectedItem });
                    }

                    _selectedItemCode = clickedButton.Tag as string;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }
        #endregion EVENTHANDLER AREA ******************************************

        #region METHOD AREA ***************************************************
        private void DrawButtons()
        {
            int x = 0;
            int y = 0;

            try
            {
                this.SuspendLayout();

                RemoveButtons();

                if (_itemList == null) return;

                // set parent Panel width / location
                _buttonParent.Width = _buttonParent.Parent.Width;
                _buttonParent.Location = new Point(0, 0);

                // set Buttons
                _buttons = new TButton[_itemList.Count];
                for (int i = 0; i < _itemList.Count; i++)
                {
                    ItemListControlItem item = _itemList[i];

                    if (i % _maxColumnCount == 0)
                    {
                        x = 0; // first column

                        if (i > 0)
                        {
                            y += ButtonHeight + _gapBetweenControls; // new line
                        }
                    }
                    else
                    {
                        x += ButtonWidth + _gapBetweenControls; // next column
                    }

                    _buttons[i] = new TButton();
                    _buttons[i].Parent = _buttonParent;
                    _buttons[i].Name = BUTTON_NAME_PREFIX + item.CodeName;
                    _buttons[i].Tag = item.Code;
                    _buttons[i].Text = item.TextValue;
                    _buttons[i].Location = new Point(x, y);
                    _buttons[i].Size = new Size(ButtonWidth, ButtonHeight);
                    _buttons[i].Click += new EventHandler(ItemListView_Click);

                    if (this._useMultipleSelection == true)
                    {
                        // added by jaeok (2020.07.14) Mantis 108025: [YQ] 필터 중복으로 선택 가능하도록 요청
                        if (_selectedItemCodeList.ContainsKey(_buttons[i].Tag as string) == true)
                        {
                            _buttons[i].ChangeCustomStyle(ButtonListSelectedCustomStyleName);
                        }
                        else if (_buttons[i].Tag.Equals(_selectedItemCode))
                        {
                            _buttons[i].ChangeCustomStyle(ButtonListSelectedCustomStyleName);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(item.CustomStyleName) == true)
                            {
                                _buttons[i].ChangeCustomStyle(ButtonListCustomStyleName);
                            }
                            else
                            {
                                _buttons[i].ChangeCustomStyle(item.CustomStyleName);
                            }
                        }
                    }
                    else
                    {
                        if (_buttons[i].Tag.Equals(_selectedItemCode))
                        {
                            _buttons[i].ChangeCustomStyle(ButtonListSelectedCustomStyleName);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(item.CustomStyleName) == true)
                            {
                                _buttons[i].ChangeCustomStyle(ButtonListCustomStyleName);
                            }
                            else
                            {
                                _buttons[i].ChangeCustomStyle(item.CustomStyleName);
                            }
                        }
                    }

                    //_buttons[i].ChangeCustomStyle(ButtonListCustomStyleName);
                    //_buttons[i].CustomStyleName = "biz_default_btn_list";
                }

                // set parent Panel height
                _buttonParent.Height = y + ButtonHeight + _gapBetweenControls;

                InitScrollBar();

                SelectItem(_selectedItemCode);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.ResumeLayout(false);
            }
        }

        public void RemoveButtons()
        {
            try
            {
                // set button panel empty
                _buttonParent.Width = _buttonParent.Parent.Width - _gapBetweenControls;
                _buttonParent.Height = 0;

                List<Button> list = _buttonParent.Controls.OfType<Button>().ToList();

                foreach (Button btn in list)
                {
                    btn.Click -= new EventHandler(ItemListView_Click);
                    this._buttonParent.Controls.Remove(btn);
                    btn.Dispose();
                }

                if (_useActivatedInAllBay == true)//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
                {
                    _blockBayActivatedCount = 0;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public string GetSelectedItemCode()
        {
            return _selectedItemCode;
        }

        public void SelectItem(string code)
        {
            try
            {
                List<TButton> list = pnlButtonList.Controls.OfType<TButton>().ToList();

                // set default
                foreach (TButton btn in list)
                {
                    if (btn.CustomStyleName.Equals(ButtonListSelectedCustomStyleName) == true)
                    {
                        btn.ChangeCustomStyle(ButtonListCustomStyleName);
                    }
                }

                int selectedCodeIdx = 0;
                int rowIndex = 0;
                // search Buttons
                foreach (TButton btn in list)
                {
                    if (_useMultipleSelection == true)
                    {
                        if (_selectedItemCodeList.ContainsKey(btn.Tag as string) == true)
                        {
                            // set color for selected Button
                            btn.ChangeCustomStyle(ButtonListSelectedCustomStyleName);

                            selectedCodeIdx = rowIndex;
                        }
                        else if (btn.Tag.Equals(code))
                        {
                            // set color for selected Button
                            btn.ChangeCustomStyle(ButtonListSelectedCustomStyleName);

                            selectedCodeIdx = rowIndex;
                            //break;
                        }
                    }
                    else
                    {
                        if (btn.Tag.Equals(code))
                        {
                            // set color for selected Button
                            btn.ChangeCustomStyle(ButtonListSelectedCustomStyleName);

                            selectedCodeIdx = rowIndex;
                            break;
                        }
                    }

                    rowIndex++;
                }

                if (MaxColumnCount == 0) MaxColumnCount = 1;

                this.scrButtonList.Value = ((int)(selectedCodeIdx) / MaxColumnCount) * this.scrButtonList.SmallChange;
                this._buttonParent.Location = new Point(0, (this.scrButtonList.Value) * -1);

                _selectedItemCode = code;

                // added by jaeok (2020.07.14) Mantis 108025: [YQ] 필터 중복으로 선택 가능하도록 요청
                if (_useMultipleSelection == true)
                {
                    if (_selectedItemCodeList.ContainsKey(code) == false)
                    {
                        _selectedItemCodeList.Add(code, code);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }
        public void ClearSelectedCode()
        {
            this._selectedItemCode = string.Empty;
            this._selectedItemCodeList = new Dictionary<string, string>();
        }

        #endregion METHOD AREA ************************************************
    }
    public class ItemListViewEventArgs : EventArgs
    {
        public object SelectedItemCode { get; set; }
        public string SelectedItemCodeName { get; set; }
        public object SelectedItem { get; set; }
        public bool IsSelected { get; set; }

        public ItemListViewEventArgs(object selectedItemCode)
        {
            this.SelectedItemCode = selectedItemCode;
        }
    }
}
