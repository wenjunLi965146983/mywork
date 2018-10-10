using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;

namespace WorldGeneralLib.Table
{
    public enum Element
    {
        Element_A = 0,
        Element_B,
        Element_C,
        Element_D
    }

    [Serializable]
    public class TablePosItem
    {
        public TablePosItem()
        {
            _strName = "Pos_0";
            _dPosA = 0.0;
            _dPosB = 0.0;
            _dPosC = 0.0;
            _dPosD = 0.0;
            _dPosU = 0.0;
            _dPosX = 0.0;
            _dPosY = 0.0;
            _dPosZ = 0.0;

            _bActiveA = false;
            _bActiveB = false;
            _bActiveC = false;
            _bActiveD = false;
            _bActiveU = false;
            _bActiveX = false;
            _bActiveY = false;
            _bActiveZ = false;

            _bMoveRel = false;
            _bIsUse = true;
            _trgElement = Element.Element_A;
            _pointMoveMode = PointMoveMode.NTP;
            _strRemarks = "";
        }
        public TablePosItem(string strName) : this()
        {
            this._strName = strName;
        }

        private string _strName;
        [CategoryAttribute("Design"), DescriptionAttribute("Pos item name.")]
        public string Name
        {
            get { return _strName; }
            set { this._strName = value; }
        }

        private bool _bIsUse;
        public bool IsUse
        {
            get { return _bIsUse; }
            set { _bIsUse = value; }
        }

        private Element _trgElement;
        public Element TrgElement
        {
            get { return _trgElement; }
            set { _trgElement = value; }
        }

        private double _dPosX;
        [CategoryAttribute("Points"), DescriptionAttribute("Pos X.")]
        public double PosX
        {
            get { return _dPosX; }
            set { this._dPosX = value; }
        }

        private double _dPosY;
        [CategoryAttribute("Points"), DescriptionAttribute("Pos Y.")]
        public double PosY
        {
            get { return _dPosY; }
            set { this._dPosY = value; }
        }

        private double _dPosZ;
        [CategoryAttribute("Points"), DescriptionAttribute("Pos Z")]
        public double PosZ
        {
            get { return _dPosZ; }
            set { this._dPosZ = value; }
        }

        private double _dPosU;
        [CategoryAttribute("Points"), DescriptionAttribute("Pos U.")]
        public double PosU
        {
            get { return _dPosU; }
            set { this._dPosU = value; }
        }

        private double _dPosA;
        [CategoryAttribute("Points"), DescriptionAttribute("Pos A.")]
        public double PosA
        {
            get { return _dPosA; }
            set { this._dPosA = value; }
        }

        private double _dPosB;
        [CategoryAttribute("Points"), DescriptionAttribute("Pos B.")]
        public double PosB
        {
            get { return _dPosB; }
            set { this._dPosB = value; }
        }

        private double _dPosC;
        [CategoryAttribute("Points"), DescriptionAttribute("Pos C.")]
        public double PosC
        {
            get { return _dPosC; }
            set { this._dPosC = value; }
        }

        private double _dPosD;
        [CategoryAttribute("Points"), DescriptionAttribute("Pos D")]
        public double PosD
        {
            get { return _dPosD; }
            set { this._dPosD = value; }
        }

        private bool _bActiveX;
        [CategoryAttribute("Active"), DescriptionAttribute("Is active.")]
        public bool ActiveX
        {
            get { return _bActiveX; }
            set { this._bActiveX = value; }
        }

        private bool _bActiveY;
        [CategoryAttribute("Active"), DescriptionAttribute("Is active.")]
        public bool ActiveY
        {
            get { return _bActiveY; }
            set { this._bActiveY = value; }
        }

        private bool _bActiveZ;
        [CategoryAttribute("Active"), DescriptionAttribute("Is active.")]
        public bool ActiveZ
        {
            get { return _bActiveZ; }
            set { this._bActiveZ = value; }
        }

        private bool _bActiveU;
        [CategoryAttribute("Active"), DescriptionAttribute("Is active.")]
        public bool ActiveU
        {
            get { return _bActiveU; }
            set { this._bActiveU = value; }
        }

        private bool _bActiveA;
        [CategoryAttribute("Active"), DescriptionAttribute("Is active.")]
        public bool ActiveA
        {
            get { return _bActiveA; }
            set { this._bActiveA = value; }
        }

        private bool _bActiveB;
        [CategoryAttribute("Active"), DescriptionAttribute("Is active.")]
        public bool ActiveB
        {
            get { return _bActiveB; }
            set { this._bActiveB = value; }
        }

        private bool _bActiveC;
        [CategoryAttribute("Active"), DescriptionAttribute("Is active.")]
        public bool ActiveC
        {
            get { return _bActiveC; }
            set { this._bActiveC = value; }
        }

        private bool _bActiveD;
        [CategoryAttribute("Active"), DescriptionAttribute("Is active.")]
        public bool ActiveD
        {
            get { return _bActiveD; }
            set { this._bActiveD = value; }
        }

        private bool _bMoveRel;
        [CategoryAttribute("MoveMode"), DescriptionAttribute("Move mode.")]
        public bool MoveRel
        {
            get { return _bMoveRel; }
            set { this._bMoveRel = value; }
        }

        private PointMoveMode _pointMoveMode;
        public PointMoveMode MoveMode
        {
            get { return _pointMoveMode; }
            set { _pointMoveMode = value; }
        }

        private string _strRemarks;
        public string Remarks
        {
            get { return _strRemarks; }
            set { _strRemarks = value; }
        }
    }
    [Serializable]
    public class TableAxisData
    {
        public TableAxisData()
        {
            this._strMotionCardName = "";
            this._strName = "";
            this._sIndex = 0;
            this._bActive = false;
            this._limitLogic = SenserLogic.NC;
            this._orgLogic = SenserLogic.NC;
            this._alarmLogic = SenserLogic.NC;
            this._axisHomeMode = HomeMode.CCWL;
            this._axisPulseMode = PulseMode.CWCCW;
            this._dPulseToMM = 0.001;
            this._dAcc = 1.0;
            this._dDec = 1.0;
            this._dRunSpeed = 1.0;
            this._dJogSpeed = 1.0;
            this._dSearchLimitSpeed = 1.0;
            this._dSearchHomeSpeed = 1.0;
            this._dSoftLimitPos = 1.0;
            this._dSoftLimitNeg = -1.0;
            this._iCorNo = 1;
            this._dMaxSpeed = 1;
        }
        public TableAxisData(string strName) : this()
        {
            this._strName = strName;
        }

        private string _strMotionCardName;
        [CategoryAttribute("Design"), DescriptionAttribute("轴资源所属的运动控制卡名称。")]
        public string MotionCardName
        {
            get { return _strMotionCardName; }
            set { this._strMotionCardName = value; }
        }

        private string _strName;
        [ReadOnly(true)]
        [CategoryAttribute("Design"), DescriptionAttribute("轴名称。")]
        public string Name
        {
            get { return _strName; }
            set { this._strName = value; }
        }

        private short _sIndex;
        [CategoryAttribute("Design"), DescriptionAttribute("轴号。")]
        public short Index
        {
            get { return _sIndex; }
            set { _sIndex = value; }
        }

        private bool _bActive;
        [CategoryAttribute("Design"), DescriptionAttribute("指示在当前平台中是否启用该轴。")]
        public bool Active
        {
            get { return _bActive; }
            set { _bActive = value; }
        }

        private bool _bUseCfgFile;
        [CategoryAttribute("Design"), DescriptionAttribute("是否使用配置文件中的配置来初始化该轴。")]
        public bool UseConfigFile
        {
            get { return _bUseCfgFile; }
            set { _bUseCfgFile = value; }
        }

        private SenserLogic _limitLogic;
        [CategoryAttribute("Logic"), DescriptionAttribute("极限逻辑。")]
        public SenserLogic LimitLogic
        {
            get { return _limitLogic; }
            set { this._limitLogic = value; }
        }

        private SenserLogic _orgLogic;
        [CategoryAttribute("Logic"), DescriptionAttribute("原点逻辑。")]
        public SenserLogic OrgLogic
        {
            get { return _orgLogic; }
            set { this._orgLogic = value; }
        }

        private SenserLogic _alarmLogic;
        [CategoryAttribute("Logic"), DescriptionAttribute("报警逻辑。")]
        public SenserLogic AlarmLogic
        {
            get { return _alarmLogic; }
            set { this._alarmLogic = value; }
        }

        private HomeMode _axisHomeMode;
        [CategoryAttribute("Logic"), DescriptionAttribute("回原方式。")]
        public HomeMode AxisHomeMode
        {
            get { return _axisHomeMode; }
            set { this._axisHomeMode = value; }
        }

        private PulseMode _axisPulseMode;
        [CategoryAttribute("Logic"), DescriptionAttribute("脉冲方式。")]
        public PulseMode AxisPulseMode
        {
            get { return _axisPulseMode; }
            set { this._axisPulseMode = value; }
        }

        private int _iCorNo;
        [CategoryAttribute("Logic"), DescriptionAttribute("坐标系编号。")]
        public int CorNo
        {
            get { return _iCorNo; }
            set { this._iCorNo = value; }
        }

        private double _dPulseToMM;
        [CategoryAttribute("Data1"), DescriptionAttribute("脉冲当量(mm)。")]
        public double PulseToMM
        {
            get { return _dPulseToMM; }
            set { this._dPulseToMM = value; }
        }

        private double _dAcc;
        [CategoryAttribute("Data2"), DescriptionAttribute("加速度(mm/s2)。")]
        public double Acc
        {
            get { return _dAcc; }
            set { this._dAcc = value; }
        }

        private double _dDec;
        [CategoryAttribute("Data2"), DescriptionAttribute("减速度(mm/s2)。")]
        public double Dec
        {
            get { return _dDec; }
            set { this._dDec = value; }
        }

        private double _dRunSpeed;
        [CategoryAttribute("Data3"), DescriptionAttribute("运行速度(mm/s)。")]
        public double RunSpeed
        {
            get { return _dRunSpeed; }
            set { this._dRunSpeed = value; }
        }

        private double _dJogSpeed;
        [CategoryAttribute("Data3"), DescriptionAttribute("点动速度(mm/s)。")]
        public double JogSpeed
        {
            get { return _dJogSpeed; }
            set { this._dJogSpeed = value; }
        }

        private double _dSearchHomeSpeed;
        [CategoryAttribute("Data4"), DescriptionAttribute("搜索原点速度(mm/s)。")]
        public double SearchHomeSpeed
        {
            get { return _dSearchHomeSpeed; }
            set { this._dSearchHomeSpeed = value; }
        }

        private double _dSearchLimitSpeed;
        [CategoryAttribute("Data4"), DescriptionAttribute("搜索极限速度(mm/s)。")]
        public double SearchLimitSpeed
        {
            get { return _dSearchLimitSpeed; }
            set { this._dSearchLimitSpeed = value; }
        }

        private double _dMaxSpeed;
        [CategoryAttribute("Data4"), DescriptionAttribute("轴运动最大速度(mm/s)。")]
        public double MaxSpeed
        {
            get { return _dMaxSpeed; }
            set { this._dMaxSpeed = value; }
        }

        private double _dSoftLimitPos;
        [CategoryAttribute("Safe"), DescriptionAttribute("轴正方向软限位。[mm]")]
        public double SoftLimitPos
        {
            get { return _dSoftLimitPos; }
            set { this._dSoftLimitPos = value; }
        }

        private double _dSoftLimitNeg;
        [CategoryAttribute("Safe"), DescriptionAttribute("轴正负向软限位。[mm]")]
        public double SoftLimitNeg
        {
            get { return _dSoftLimitNeg; }
            set { this._dSoftLimitNeg = value; }
        }
    }

    [XmlInclude(typeof(TableData)), XmlInclude(typeof(TableAxisData)), XmlInclude(typeof(TablePosItem))]
    public class TableData
    {
        private List<TablePosItem> _listTablePosItem;
        [XmlIgnore]
        public Dictionary<string, TablePosItem> dicTablePosItem;

        private List<TableAxisData> _listTableAxesItem;
        [XmlIgnore]
        public Dictionary<string, TableAxisData> dicTableAxisItem;

        public TableData()
        {
            _listTableAxesItem = new List<TableAxisData>();
            dicTableAxisItem = new Dictionary<string, TableAxisData>();

            _listTablePosItem = new List<TablePosItem>();
            dicTablePosItem = new Dictionary<string, TablePosItem>();
            _strMotionCardName = "";
        }
        public TableData(bool bNew) : this()
        {
            foreach (string axis in Enum.GetNames(typeof(DefaultAxis)))
            {
                TableAxisData axisData = new TableAxisData(axis);
                axisData.Active = false;
                _listTableAxesItem.Add(axisData);
                dicTableAxisItem.Add(axis, axisData);
            }
        }

        private string _strName;
        [CategoryAttribute("Design"), DescriptionAttribute("指示代码中用来标识该对象的名称。")]
        public string Name
        {
            get { return _strName; }
            set { _strName = value; }
        }

        private string _strMotionCardName;
        public string MotionCardName
        {
            get { return _strMotionCardName; }
            set { _strMotionCardName = value; }
        }

        private string _strText;
        [CategoryAttribute("Design"), DescriptionAttribute("与Table关联的文本。")]
        public string Text
        {
            get { return _strText; }
            set { _strText = value; }
        }

        private bool _bShowOnManualPage = true;
        public bool ShowOnManualPage
        {
            get { return _bShowOnManualPage; }
            set { _bShowOnManualPage = value; }
        }

        private bool _bAllowUserAddPoints = false;
        public bool AllowUserAddPoints
        {
            get { return _bAllowUserAddPoints; }
            set { _bAllowUserAddPoints = value; }
        }

        private bool _bAllowUserRemovePoints = false;
        public bool AllowUserRemovePoints
        {
            get { return _bAllowUserRemovePoints; }
            set { _bAllowUserRemovePoints = value; }
        }

        [CategoryAttribute("Axes setting"), DescriptionAttribute("Table axes setting。")]
        [Editor(typeof(CollectionEditorEx), typeof(UITypeEditor))]
        public List<TableAxisData> ListTableAxesItems
        {
            get { return _listTableAxesItem; }
            set { _listTableAxesItem = value; }
        }

        [CategoryAttribute("Pos data"), DescriptionAttribute("Table pos data。")]
        [Editor(typeof(CollectionEditorEx), typeof(UITypeEditor))]
        public List<TablePosItem> ListTablePosItems
        {
            get { return _listTablePosItem; }
            set { this._listTablePosItem = value; }
        }
    }

    public class CollectionEditorEx : CollectionEditor
    {
        //public delegate void ValueChangeHandler();
        //public static event ValueChangeHandler OnValueChange;

        public CollectionEditorEx(Type type) : base(type)
        {

        }
        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }
        protected override object CreateInstance(Type itemType)
        {
            System.Collections.IList list = GetObjectsFromInstance(this.Context.Instance);
            if (list.Count <= 0)
            {
                return null;
            }
            if (list[0].GetType() == typeof(TableData) && itemType == typeof(TableAxisData))
            {
                TableData table = (TableData)list[0];
                foreach (DefaultAxis item in Enum.GetValues(typeof(DefaultAxis)))
                {
                    if (!table.dicTableAxisItem.ContainsKey(item.ToString()))
                    {
                        TableAxisData newAxis = new TableAxisData(item.ToString());
                        table.dicTableAxisItem.Add(item.ToString(), newAxis);
                        return newAxis;
                    }
                }
            }

            if (list[0].GetType() == typeof(TableData) && itemType == typeof(TablePosItem))
            {
                TableData table = (TableData)list[0];
                if (null == table)
                {
                    return null;
                }

                int index = 0;
                string strTemp = "NewItem_";
                while (true)
                {
                    if (!table.dicTablePosItem.ContainsKey(strTemp + index.ToString()))
                    {
                        TablePosItem newPoint = new TablePosItem();
                        newPoint.Name = strTemp + index.ToString();
                        table.dicTablePosItem.Add(newPoint.Name, newPoint);
                        return newPoint;
                    }
                    index++;
                }
            }

            return null;

            //return base.CreateInstance(itemType);
        }
        protected override void DestroyInstance(object instance)
        {
            if (instance is TableAxisData)
            {
                try
                {
                    TableData table = (TableData)GetObjectsFromInstance(this.Context.Instance)[0];
                    if (null != table)
                    {
                        table.dicTableAxisItem.Remove(((TableAxisData)instance).Name);
                    }
                }
                catch //(Exception)
                {
                }
            }

            base.DestroyInstance(instance);
        }
        protected override CollectionForm CreateCollectionForm()
        {
            CollectionForm frm = base.CreateCollectionForm();
            FieldInfo fieldInfo = frm.GetType().GetField("propertyBrowser", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
            {
                (fieldInfo.GetValue(frm) as System.Windows.Forms.PropertyGrid).HelpVisible = true;
            }
            frm.Width = 700;
            frm.Height = 600;

            frm.ControlBox = false;

            #region 注册按钮事件
            FieldInfo removeButton = frm.GetType().GetField("removeButton", BindingFlags.NonPublic | BindingFlags.Instance);
            if (removeButton != null)
            {
                (removeButton.GetValue(frm) as Button).Click += ButtonRemove_Click;
            }

            FieldInfo cancelButton = frm.GetType().GetField("cancelButton", BindingFlags.NonPublic | BindingFlags.Instance);
            if (cancelButton != null)
            {
                (cancelButton.GetValue(frm) as Button).Click += ButtonCancel_Click;
            }

            FieldInfo okButton = frm.GetType().GetField("okButton", BindingFlags.NonPublic | BindingFlags.Instance);
            if (okButton != null)
            {
                (okButton.GetValue(frm) as Button).Click += ButtonOk_Click;
            }

            #endregion

            return frm;
        }
        #region 事件处理
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            ValueChanged();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            ValueChanged();
        }
        protected void ButtonRemove_Click(object sender, EventArgs e)
        {
            ValueChanged();
        }
        #endregion

        #region 方法
        protected void ValueChanged()
        {
            try
            {
                System.Collections.IList list = GetObjectsFromInstance(this.Context.Instance);
                if (list.Count <= 0)
                {
                    return;
                }

                if (list[0].GetType() == typeof(TableData))
                {
                    Type type = base.CollectionItemType;
                    if (type == typeof(TablePosItem))
                    {
                        TableData table = (TableData)list[0];
                        table.dicTablePosItem.Clear();
                        table.dicTablePosItem = table.ListTablePosItems.ToDictionary(p => p.Name);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }

}
