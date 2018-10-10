using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace BSE.Windows.Forms
{
    /// <summary>
    /// Provide Panel colors
    /// </summary>
    /// <remarks>
    /// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
    /// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
    /// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
    /// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
    /// REMAINS UNCHANGED.
    /// </remarks>
    /// <copyright>Copyright © 2006-2008 Uwe Eichkorn</copyright>
    public class PanelColors
    {
		#region Enums
		/// <summary>
		/// Gets or sets the KnownColors appearance of the ProfessionalColorTable.
		/// </summary>
		public enum KnownColors
		{
			/// <summary>
			/// The border color of the panel.
			/// </summary>
			BorderColor,
			/// <summary>
			/// The forecolor of a close icon in a Panel.
			/// </summary>
            PanelCaptionCloseIcon,
            /// <summary>
			/// The forecolor of a expand icon in a Panel.
            /// </summary>
			PanelCaptionExpandIcon,
			/// <summary>
			/// The starting color of the gradient of the Panel.
			/// </summary>
            PanelCaptionGradientBegin,
			/// <summary>
			/// The end color of the gradient of the Panel.
			/// </summary>
			PanelCaptionGradientEnd,
			/// <summary>
			/// The middle color of the gradient of the Panel.
			/// </summary>
			PanelCaptionGradientMiddle,
			/// <summary>
			/// The starting color of the gradient used in the Panel.
			/// </summary>
			PanelContentGradientBegin,
			/// <summary>
			/// The end color of the gradient used in the Panel.
			/// </summary>
			PanelContentGradientEnd,
			/// <summary>
			/// The text color of a Panel.
			/// </summary>
			PanelCaptionText,
			/// <summary>
			/// The text color of a Panel when it's collapsed.
			/// </summary>
            PanelCollapsedCaptionText,
			/// <summary>
			/// The inner border color of a Panel.
			/// </summary>
			InnerBorderColor,
			/// <summary>
			/// The backcolor of a XPanderPanel.
			/// </summary>
            XPanderPanelBackColor,
			/// <summary>
			/// The forecolor of a close icon in a XPanderPanel.
			/// </summary>
			XPanderPanelCaptionCloseIcon,
			/// <summary>
			/// The forecolor of a expand icon in a XPanderPanel.
			/// </summary>
			XPanderPanelCaptionExpandIcon,
			/// <summary>
			/// The text color of a XPanderPanel.
			/// </summary>
			XPanderPanelCaptionText,
			/// <summary>
			/// The starting color of the gradient of the XPanderPanel.
			/// </summary>
			XPanderPanelCaptionGradientBegin,
			/// <summary>
			/// The end color of the gradient of the XPanderPanel.
			/// </summary>
			XPanderPanelCaptionGradientEnd,
			/// <summary>
			/// The middle color of the gradient of the XPanderPanel.
			/// </summary>
			XPanderPanelCaptionGradientMiddle,
            /// <summary>
            /// The starting color of the gradient of a flat XPanderPanel.
            /// </summary>
            XPanderPanelFlatCaptionGradientBegin,
            /// <summary>
            /// The end color of the gradient of a flat XPanderPanel.
            /// </summary>
            XPanderPanelFlatCaptionGradientEnd,
            /// <summary>
            /// The starting color of the gradient used when the XPanderPanel is pressed down.
            /// </summary>
            XPanderPanelPressedCaptionBegin,
            /// <summary>
            /// The end color of the gradient used when the XPanderPanel is pressed down.
            /// </summary>
            XPanderPanelPressedCaptionEnd,
            /// <summary>
            /// The middle color of the gradient used when the XPanderPanel is pressed down.
            /// </summary>
            XPanderPanelPressedCaptionMiddle,
            /// <summary>
			/// The starting color of the gradient used when the XPanderPanel is selected.
			/// </summary>
			XPanderPanelSelectedCaptionBegin,
			/// <summary>
			/// The end color of the gradient used when the XPanderPanel is selected.
			/// </summary>
			XPanderPanelSelectedCaptionEnd,
			/// <summary>
			/// The middle color of the gradient used when the XPanderPanel is selected.
			/// </summary>
			XPanderPanelSelectedCaptionMiddle,
			/// <summary>
			/// The text color used when the XPanderPanel is selected.
			/// </summary>
			XPanderPanelSelectedCaptionText
		}
		#endregion

        #region FieldsPrivate

        private BasePanel m_basePanel;
		private System.Windows.Forms.ProfessionalColorTable m_professionalColorTable;
		private Dictionary<KnownColors, Color> m_dictionaryRGBTable;
		private bool m_bUseSystemColors;
        private object m_colorFreshnessKey;
        [ThreadStatic]
        private static object m_objColorFreshnessKey;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the border color of a Panel or XPanderPanel.
        /// </summary>
        public virtual Color BorderColor
        {
			get { return this.FromKnownColor(KnownColors.BorderColor); }
        }
        /// <summary>
        /// Gets the forecolor of a close icon in a Panel.
        /// </summary>
        public virtual Color PanelCaptionCloseIcon
        {
            get { return this.FromKnownColor(KnownColors.PanelCaptionCloseIcon); }
        }
        /// <summary>
        /// Gets the forecolor of a expand icon in a Panel.
        /// </summary>
        public virtual Color PanelCaptionExpandIcon
        {
            get { return this.FromKnownColor(KnownColors.PanelCaptionExpandIcon); }
        }
        /// <summary>
        /// Gets the starting color of the gradient of the Panel.
        /// </summary>
        public virtual Color PanelCaptionGradientBegin
        {
			get { return this.FromKnownColor(KnownColors.PanelCaptionGradientBegin); }
        }
		/// <summary>
        /// Gets the end color of the gradient of the Panel.
        /// </summary>
        public virtual Color PanelCaptionGradientEnd
        {
            get { return this.FromKnownColor(KnownColors.PanelCaptionGradientEnd); }
        }
		/// <summary>
        /// Gets the middle color of the gradient of the Panel.
        /// </summary>
        public virtual Color PanelCaptionGradientMiddle
        {
			get { return this.FromKnownColor(KnownColors.PanelCaptionGradientMiddle); }
        }
        /// <summary>
        /// Gets the text color of a Panel.
        /// </summary>
		public virtual Color PanelCaptionText
		{
			get { return this.FromKnownColor(KnownColors.PanelCaptionText); }
		}
        /// <summary>
        /// Gets the text color of a Panel when it's collapsed.
        /// </summary>
        public virtual Color PanelCollapsedCaptionText
        {
            get { return this.FromKnownColor(KnownColors.PanelCollapsedCaptionText); }
        }
        /// <summary>
        /// Gets the starting color of the gradient used in the Panel.
        /// </summary>
        public virtual Color PanelContentGradientBegin
        {
			get { return this.FromKnownColor(KnownColors.PanelContentGradientBegin); }
        }
		/// <summary>
        /// Gets the end color of the gradient used in the Panel.
        /// </summary>
        public virtual Color PanelContentGradientEnd
        {
			get { return this.FromKnownColor(KnownColors.PanelContentGradientEnd); }
        }
        /// <summary>
        /// Gets the inner border color of a Panel.
        /// </summary>
        public virtual Color InnerBorderColor
        {
            get { return this.FromKnownColor(KnownColors.InnerBorderColor); }
        }
		/// <summary>
		/// Gets the backcolor of a XPanderPanel.
		/// </summary>
		public virtual Color XPanderPanelBackColor
		{
			get { return this.FromKnownColor(KnownColors.XPanderPanelBackColor); }
		}
        /// <summary>
		/// Gets the forecolor of a close icon in a XPanderPanel.
		/// </summary>
		public virtual Color XPanderPanelCaptionCloseIcon
		{
			get { return this.FromKnownColor(KnownColors.XPanderPanelCaptionCloseIcon); }
		}
        /// <summary>
		/// Gets the forecolor of a expand icon in a XPanderPanel.
		/// </summary>
		public virtual Color XPanderPanelCaptionExpandIcon
		{
			get { return this.FromKnownColor(KnownColors.XPanderPanelCaptionExpandIcon); }
		}
        /// <summary>
        /// Gets the starting color of the gradient of the XPanderPanel.
        /// </summary>
        public virtual Color XPanderPanelCaptionGradientBegin
        {
            get { return this.FromKnownColor(KnownColors.XPanderPanelCaptionGradientBegin); }
        }
        /// <summary>
        /// Gets the end color of the gradient of the XPanderPanel.
        /// </summary>
        public virtual Color XPanderPanelCaptionGradientEnd
        {
            get { return this.FromKnownColor(KnownColors.XPanderPanelCaptionGradientEnd); }
        }
        /// <summary>
        /// Gets the middle color of the gradient of the XPanderPanel.
        /// </summary>
        public virtual Color XPanderPanelCaptionGradientMiddle
        {
            get { return this.FromKnownColor(KnownColors.XPanderPanelCaptionGradientMiddle); }
        }
        /// <summary>
        /// Gets the text color of a XPanderPanel.
        /// </summary>
        public virtual Color XPanderPanelCaptionText
        {
			get { return this.FromKnownColor(KnownColors.XPanderPanelCaptionText); }
        }
        /// <summary>
        /// Gets the starting color of the gradient of a flat XPanderPanel.
        /// </summary>
        public virtual Color XPanderPanelFlatCaptionGradientBegin
        {
            get { return this.FromKnownColor(KnownColors.XPanderPanelFlatCaptionGradientBegin); }
        }
        /// <summary>
        /// Gets the end color of the gradient of a flat XPanderPanel.
        /// </summary>
        public virtual Color XPanderPanelFlatCaptionGradientEnd
        {
            get { return this.FromKnownColor(KnownColors.XPanderPanelFlatCaptionGradientEnd); }
        }
        /// <summary>
        /// Gets the starting color of the gradient used when the XPanderPanel is pressed down.
        /// </summary>
        public virtual Color XPanderPanelPressedCaptionBegin
        {
            get { return this.FromKnownColor(KnownColors.XPanderPanelPressedCaptionBegin); }
        }
        /// <summary>
        /// Gets the end color of the gradient used when the XPanderPanel is pressed down.
        /// </summary>
        public virtual Color XPanderPanelPressedCaptionEnd
        {
            get { return this.FromKnownColor(KnownColors.XPanderPanelPressedCaptionEnd); }
        }
        /// <summary>
        /// Gets the middle color of the gradient used when the XPanderPanel is pressed down.
        /// </summary>
        public virtual Color XPanderPanelPressedCaptionMiddle
        {
            get { return this.FromKnownColor(KnownColors.XPanderPanelPressedCaptionMiddle); }
        }
        /// <summary>
        /// Gets the starting color of the gradient used when the XPanderPanel is selected.
        /// </summary>
        public virtual Color XPanderPanelSelectedCaptionBegin
        {
			get { return this.FromKnownColor(KnownColors.XPanderPanelSelectedCaptionBegin); }
        }
        /// <summary>
        /// Gets the end color of the gradient used when the XPanderPanel is selected.
        /// </summary>
        public virtual Color XPanderPanelSelectedCaptionEnd
        {
			get { return this.FromKnownColor(KnownColors.XPanderPanelSelectedCaptionEnd); }
        }
        /// <summary>
        /// Gets the middle color of the gradient used when the XPanderPanel is selected.
        /// </summary>
        public virtual Color XPanderPanelSelectedCaptionMiddle
        {
			get { return this.FromKnownColor(KnownColors.XPanderPanelSelectedCaptionMiddle); }
        }
        /// <summary>
        /// Gets the text color used when the XPanderPanel is selected.
        /// </summary>
        public virtual Color XPanderPanelSelectedCaptionText
        {
			get { return this.FromKnownColor(KnownColors.XPanderPanelSelectedCaptionText); }
        }
		/// <summary>
		/// Gets or sets a value indicating whether to use System.Drawing.SystemColors rather than colors that match the current visual style.
		/// </summary>
		public bool UseSystemColors
		{
			get { return this.m_bUseSystemColors; }
			set
			{
				if (value.Equals(this.m_bUseSystemColors) == false)
				{
					this.m_bUseSystemColors = value;
					this.m_professionalColorTable.UseSystemColors = this.m_bUseSystemColors;
                    Clear();
				}
			}
		}
        /// <summary>
        /// Gets or sets the panel or xpanderpanel
        /// </summary>
		public BasePanel Panel
		{
			get { return this.m_basePanel; }
			set { this.m_basePanel = value; }
		}
		internal Color FromKnownColor(KnownColors color)
		{
			return (Color)this.ColorTable[color];
		}
        internal static object ColorFreshnessKey
        {
            get
            {
                return m_objColorFreshnessKey;
            }
        }
        private Dictionary<KnownColors, Color> ColorTable
        {
            get
            {
                if (PanelColors.ColorFreshnessKey != this.m_colorFreshnessKey)
                {
                    this.Clear();
                }
                this.m_colorFreshnessKey = PanelColors.ColorFreshnessKey;
                if ((this.m_basePanel != null) && (this.m_basePanel.ColorScheme == ColorScheme.Professional))
                {
                    if ((this.m_bUseSystemColors == true) ||
                    (DisplayInformation.IsLunaTheme == false) && (DisplayInformation.IsAeroTheme == false))
                    {
                        if (this.m_dictionaryRGBTable == null)
                        {
                            this.m_dictionaryRGBTable = new Dictionary<KnownColors, Color>(0xd4);
                            InitBaseColors(ref m_dictionaryRGBTable);
                        }
                    }
                    else
                    {
                        if (this.m_dictionaryRGBTable == null)
                        {
                            this.m_dictionaryRGBTable = new Dictionary<KnownColors, Color>(0xd4);
                            InitColors(ref m_dictionaryRGBTable);
                        }
                    }
                }
                else
                {
                    if (this.m_dictionaryRGBTable == null)
                    {
                        this.m_dictionaryRGBTable = new Dictionary<KnownColors, Color>(0xd4);
                        InitCustomColors(ref m_dictionaryRGBTable);
                    }
                }
                return this.m_dictionaryRGBTable;
            }
        }

        #endregion

        #region MethodsPublic
		/// <summary>
		/// Initializes a new instance of the PanelColors class.
		/// </summary>
		public PanelColors()
		{
			this.m_professionalColorTable = new System.Windows.Forms.ProfessionalColorTable();
		}
		/// <summary>
        /// Initialize a new instance of the PanelColors class.
        /// </summary>
        /// <param name="basePanel">Base class for the panel or xpanderpanel control.</param>
        public PanelColors(BasePanel basePanel) : this()
        {
            this.m_basePanel = basePanel;
            Microsoft.Win32.SystemEvents.UserPreferenceChanged += new Microsoft.Win32.UserPreferenceChangedEventHandler(SystemEventsUserPreferenceChanged);
        }
        /// <summary>
        /// Clears the current color table
        /// </summary>
		public void Clear()
        {
            ResetRGBTable();
        }
        #endregion

		#region MethodsProtected
		/// <summary>
		/// Initialize a color Dictionary with defined colors
		/// </summary>
		/// <param name="rgbTable">Dictionary with defined colors</param>
		protected virtual void InitColors(ref Dictionary<KnownColors, Color> rgbTable)
		{
			InitBaseColors(ref rgbTable);
		}
		#endregion

        #region MethodsPrivate

		private void InitBaseColors(ref Dictionary<KnownColors, Color> rgbTable)
		{
            rgbTable[KnownColors.BorderColor] = this.m_professionalColorTable.GripDark;
            rgbTable[KnownColors.InnerBorderColor] = this.m_professionalColorTable.GripLight;
            rgbTable[KnownColors.PanelCaptionCloseIcon] = SystemColors.ControlText;
            rgbTable[KnownColors.PanelCaptionExpandIcon] = SystemColors.ControlText;
            rgbTable[KnownColors.PanelCaptionGradientBegin] = this.m_professionalColorTable.OverflowButtonGradientBegin;
            rgbTable[KnownColors.PanelCaptionGradientEnd] = this.m_professionalColorTable.OverflowButtonGradientEnd;
            rgbTable[KnownColors.PanelCaptionGradientMiddle] = this.m_professionalColorTable.OverflowButtonGradientMiddle;
			rgbTable[KnownColors.PanelContentGradientBegin] = this.m_professionalColorTable.ToolStripContentPanelGradientBegin;
			rgbTable[KnownColors.PanelContentGradientEnd] = this.m_professionalColorTable.ToolStripContentPanelGradientEnd;
			rgbTable[KnownColors.PanelCaptionText] = SystemColors.ActiveCaptionText;
            rgbTable[KnownColors.PanelCollapsedCaptionText] = SystemColors.ControlText;
			rgbTable[KnownColors.XPanderPanelBackColor] = this.m_professionalColorTable.ToolStripContentPanelGradientBegin;
			rgbTable[KnownColors.XPanderPanelCaptionCloseIcon] = SystemColors.ControlText;
			rgbTable[KnownColors.XPanderPanelCaptionExpandIcon] = SystemColors.ControlText;
			rgbTable[KnownColors.XPanderPanelCaptionText] = SystemColors.ControlText;
			rgbTable[KnownColors.XPanderPanelCaptionGradientBegin] = this.m_professionalColorTable.ToolStripGradientBegin;
			rgbTable[KnownColors.XPanderPanelCaptionGradientEnd] = this.m_professionalColorTable.ToolStripGradientEnd;
			rgbTable[KnownColors.XPanderPanelCaptionGradientMiddle] = this.m_professionalColorTable.ToolStripGradientMiddle;
            rgbTable[KnownColors.XPanderPanelFlatCaptionGradientBegin] = this.m_professionalColorTable.ToolStripGradientMiddle;
            rgbTable[KnownColors.XPanderPanelFlatCaptionGradientEnd] = this.m_professionalColorTable.ToolStripGradientBegin;
            rgbTable[KnownColors.XPanderPanelSelectedCaptionBegin] = this.m_professionalColorTable.ButtonCheckedGradientBegin;
            rgbTable[KnownColors.XPanderPanelPressedCaptionBegin] = this.m_professionalColorTable.ButtonPressedGradientBegin;
            rgbTable[KnownColors.XPanderPanelPressedCaptionEnd] = this.m_professionalColorTable.ButtonPressedGradientEnd;
            rgbTable[KnownColors.XPanderPanelPressedCaptionMiddle] = this.m_professionalColorTable.ButtonPressedGradientMiddle;
			rgbTable[KnownColors.XPanderPanelSelectedCaptionEnd] = this.m_professionalColorTable.ButtonCheckedGradientEnd;
			rgbTable[KnownColors.XPanderPanelSelectedCaptionMiddle] = this.m_professionalColorTable.ButtonCheckedGradientMiddle;
			rgbTable[KnownColors.XPanderPanelSelectedCaptionText] = SystemColors.ControlText;
		}

		private void InitCustomColors(ref Dictionary<KnownColors, Color> rgbTable)
		{
            rgbTable[KnownColors.BorderColor] = this.m_basePanel.BorderColor;
            rgbTable[KnownColors.InnerBorderColor] = this.m_basePanel.InnerBorderColor;
            Panel panel = this.m_basePanel as Panel;
			if (panel != null)
			{
                rgbTable[KnownColors.PanelCaptionCloseIcon] = this.m_basePanel.CloseIconForeColor;
                rgbTable[KnownColors.PanelCaptionExpandIcon] = this.m_basePanel.CaptionForeColor;
                rgbTable[KnownColors.PanelCaptionGradientBegin] = this.m_basePanel.ColorCaptionGradientBegin;
				rgbTable[KnownColors.PanelCaptionGradientEnd] = this.m_basePanel.ColorCaptionGradientEnd;
				rgbTable[KnownColors.PanelCaptionGradientMiddle] = this.m_basePanel.ColorCaptionGradientMiddle;
				rgbTable[KnownColors.PanelContentGradientBegin] = panel.ColorContentPanelGradientBegin;
				rgbTable[KnownColors.PanelContentGradientEnd] = panel.ColorContentPanelGradientEnd;
				rgbTable[KnownColors.PanelCaptionText] = panel.CaptionForeColor;
                rgbTable[KnownColors.PanelCollapsedCaptionText] = panel.CollapsedCaptionForeColor;
			}

			XPanderPanel xpanderPanel = this.m_basePanel as XPanderPanel;
			if (xpanderPanel != null)
			{
				rgbTable[KnownColors.XPanderPanelBackColor] = xpanderPanel.BackColor;
                rgbTable[KnownColors.XPanderPanelCaptionCloseIcon] = this.m_basePanel.CloseIconForeColor;
                rgbTable[KnownColors.XPanderPanelCaptionExpandIcon] = this.m_basePanel.CaptionForeColor;
				rgbTable[KnownColors.XPanderPanelCaptionText] = xpanderPanel.CaptionForeColor;
				rgbTable[KnownColors.XPanderPanelCaptionGradientBegin] = xpanderPanel.ColorCaptionGradientBegin;
				rgbTable[KnownColors.XPanderPanelCaptionGradientEnd] = xpanderPanel.ColorCaptionGradientEnd;
				rgbTable[KnownColors.XPanderPanelCaptionGradientMiddle] = xpanderPanel.ColorCaptionGradientMiddle;
                rgbTable[KnownColors.XPanderPanelFlatCaptionGradientBegin] = xpanderPanel.ColorFlatCaptionGradientBegin;
                rgbTable[KnownColors.XPanderPanelFlatCaptionGradientEnd] = xpanderPanel.ColorFlatCaptionGradientEnd;
                rgbTable[KnownColors.XPanderPanelPressedCaptionBegin] = this.m_professionalColorTable.ButtonPressedGradientBegin;
                rgbTable[KnownColors.XPanderPanelPressedCaptionEnd] = this.m_professionalColorTable.ButtonPressedGradientEnd;
                rgbTable[KnownColors.XPanderPanelPressedCaptionMiddle] = this.m_professionalColorTable.ButtonPressedGradientMiddle;
                rgbTable[KnownColors.XPanderPanelSelectedCaptionBegin] = this.m_professionalColorTable.ButtonCheckedGradientBegin;
				rgbTable[KnownColors.XPanderPanelSelectedCaptionEnd] = this.m_professionalColorTable.ButtonCheckedGradientEnd;
				rgbTable[KnownColors.XPanderPanelSelectedCaptionMiddle] = this.m_professionalColorTable.ButtonCheckedGradientMiddle;
				rgbTable[KnownColors.XPanderPanelSelectedCaptionText] = SystemColors.ControlText;
			}
		}

        private void ResetRGBTable()
        {
            if (this.m_dictionaryRGBTable != null)
            {
                this.m_dictionaryRGBTable.Clear();
            }
            this.m_dictionaryRGBTable = null;
        }

        private void SystemEventsUserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {
            m_objColorFreshnessKey = new object();
        }

        #endregion
    }
}
