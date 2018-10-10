using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

using BSE.Windows.Forms.Properties;

namespace BSE.Windows.Forms
{
	/// <summary>
	/// Base class for the panel or xpanderpanel control. 
	/// </summary>
	/// <remarks>
	/// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
	/// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
	/// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
	/// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
	/// REMAINS UNCHANGED.
	/// </remarks>
	/// <copyright>Copyright © 2006-2008 Uwe Eichkorn</copyright>
	[DesignTimeVisibleAttribute(false)]
    public class BasePanel : ScrollableControl, IPanel
	{
		#region Constants

        /// <summary>
        /// padding value for the panel
        /// </summary>
        public const int CaptionSpacing = 6;

		#endregion

        #region Events
		/// <summary>
		/// Occurs when the close icon in the caption of the panel or xpanderpanel is clicked. 
		/// </summary>
		[Description("Occurs when the close icon in the caption of the panel or xpanderpanel is clicked.")]
		public event EventHandler<EventArgs> CloseClick;
		/// <summary>
		/// Occurs when the expand icon in the caption of the panel or xpanderpanel is clicked. 
		/// </summary>
		[Description("Occurs when the expand icon in the caption of the panel or xpanderpanel is clicked.")]
		public event EventHandler<EventArgs> ExpandClick;
        /// <summary>
        /// Occurs when the panel or xpanderpanel expands.
        /// </summary>
        [Description("Occurs when the panel or xpanderpanel expands.")]
        public event EventHandler<XPanderStateChangeEventArgs> PanelExpanding;
        /// <summary>
        /// Occurs when the panel or xpanderpanel collapse.
        /// </summary>
        [Description("Occurs when the panel or xpanderpanel collapse.")]
        public event EventHandler<XPanderStateChangeEventArgs> PanelCollapsing;
        /// <summary>
        /// The PanelStyleChanged event occurs when PanelStyle flags have been changed.
        /// </summary>
        [Description("The PanelStyleChanged event occurs when PanelStyle flags have been changed.")]
        public event EventHandler<PanelStyleChangeEventArgs> PanelStyleChanged;
        /// <summary>
        /// The ColorSchemeChanged event occurs when ColorScheme flags have been changed.
        /// </summary>
        [Description("The ColorSchemeChanged event occurs when ColorScheme flags have been changed.")]
        public event EventHandler<ColorSchemeChangeEventArgs> ColorSchemeChanged;
        /// <summary>
        /// Occurs when the value of the BorderColor property changes.
        /// </summary>
        [Description("Occurs when the value of the BorderColor property changes.")]
        public event EventHandler<EventArgs> BorderColorChanged;
        /// <summary>
        /// Occurs when the value of the InnerBorderColor property changes.
        /// </summary>
        [Description("Occurs when the value of the InnerBorderColor property changes.")]
        public event EventHandler<EventArgs> InnerBorderColorChanged;
        /// <summary>
        /// Occurs when the value of the CaptionForeColor property changes.
        /// </summary>
        [Description("Occurs when the value of the CaptionForeColor property changes.")]
        public event EventHandler<EventArgs> CaptionForeColorChanged;
        /// <summary>
        /// Occurs when the value of the CaptionHeight property changes.
        /// </summary>
        [Description("Occurs when the value of the CaptionHeight property changes.")]
        public event EventHandler<EventArgs> CaptionHeightChanged;
        /// <summary>
        /// Occurs when the value of the CloseIconForeColor property changes.
        /// </summary>
        [Description("Occurs when the value of the CloseIconForeColor property changes.")]
        public event EventHandler<EventArgs> CloseIconForeColorChanged;
        /// <summary>
        /// Occurs when the value of the ColorCaptionGradientBegin property changes.
        /// </summary>
        [Description("Occurs when the value of the ColorCaptionGradientBegin property changes.")]
        public event EventHandler<EventArgs> ColorCaptionGradientBeginChanged;
        /// <summary>
        /// Occurs when the value of the ColorCaptionGradientEndChanged property changes.
        /// </summary>
        [Description("Occurs when the value of the ColorCaptionGradientEndChanged property changes.")]
        public event EventHandler<EventArgs> ColorCaptionGradientEndChanged;
        /// <summary>
        /// Occurs when the value of the ColorCaptionGradientMiddleChanged property changes.
        /// </summary>
        [Description("Occurs when the value of the ColorCaptionGradientMiddleChanged property changes.")]
        public event EventHandler<EventArgs> ColorCaptionGradientMiddleChanged;

        #endregion

        #region FieldsPrivate

        private int m_iCaptionHeight;
        private Font m_captionFont;
        private Rectangle m_imageRectangle;
        private Color m_captionForeColor;
		private Color m_borderColor;
        private System.Drawing.Color m_colorInnerBorder;
		private bool m_bShowBorder;
        private bool m_bExpand;
        private Size m_imageSize;
        private BSE.Windows.Forms.ColorScheme m_eColorScheme;
        private PanelColors m_panelColors;
        private PanelStyle m_ePanelStyle;
        private Image m_image;
        private CaptionState m_captionState;
		private bool m_bShowExpandIcon;
		private bool m_bShowCloseIcon;
        private Color m_colorCloseIcon;
		private System.Drawing.Color m_colorCaptionGradientBegin;
		private System.Drawing.Color m_colorCaptionGradientMiddle;
		private System.Drawing.Color m_colorCaptionGradientEnd;

		#endregion

        #region FieldsProtected
		/// <summary>
		/// The rectangle that contains the expand panel icon
		/// </summary>
        protected Rectangle RectangleExandPanelIcon = Rectangle.Empty;
		/// <summary>
		/// The rectangle that contains the close panel icon
		/// </summary>
        protected Rectangle RectangleClosePanelIcon = Rectangle.Empty;

        #endregion

        #region Properties
        /// <summary>
		/// Gets or sets the style of the panel.
		/// </summary>
		[Description("Style of the panel")]
        [DefaultValue(0)]
		[Category("Appearance")]
		public virtual BSE.Windows.Forms.PanelStyle PanelStyle
		{
            get { return this.m_ePanelStyle; }
            set
            {
                if (value.Equals(this.m_ePanelStyle) == false)
                {
                    this.m_ePanelStyle = value;
                    OnPanelStyleChanged(this,new PanelStyleChangeEventArgs(this.m_ePanelStyle));
                }
            }
		}
		/// <summary>
		/// Gets or sets the image that is displayed on a Panels caption.
		/// </summary>
		[Description("Gets or sets the image that is displayed on a Panels caption.")]
		[Category("Appearance")]
		public Image Image
		{
			get { return this.m_image; }
			set
			{
                if (value != this.m_image)
                {
                    this.m_image = value;
                    this.Invalidate(this.CaptionRectangle);
                }
			}
		}
		/// <summary>
		/// Gets or sets the color schema which is used for the panel.
		/// </summary>
		[Description("ColorScheme of the Panel")]
		[DefaultValue(BSE.Windows.Forms.ColorScheme.Professional)]
		[Browsable(true)]
		[Category("Appearance")]
		public virtual BSE.Windows.Forms.ColorScheme ColorScheme
		{
			get { return this.m_eColorScheme; }
			set
			{
                if (value.Equals(this.m_eColorScheme) == false)
                {
                    this.m_eColorScheme = value;
                    OnColorSchemeChanged(this, new ColorSchemeChangeEventArgs(this.m_eColorScheme));
                }
			}
		}
		/// <summary>
        /// Gets or sets the height of the panels caption.  
		/// </summary>
        [Description("Gets or sets the height of the panels caption."),
		DefaultValue(25),
		Category("Appearance")]
		public int CaptionHeight
		{
			get { return m_iCaptionHeight; }
            set
            {
                if (value < Constants.CaptionMinHeight)
                {
                    throw new InvalidOperationException(
                        string.Format(
                        System.Globalization.CultureInfo.CurrentUICulture,
                        Resources.IDS_InvalidOperationExceptionInteger, value, "CaptionHeight", Constants.CaptionMinHeight));
                }
                this.m_iCaptionHeight = value;
                OnCaptionHeightChanged(this, EventArgs.Empty);
            }
		}
		/// <summary>
		/// Gets or sets the font of the text displayed on the caption.
		/// </summary>
		[Description("Gets or sets the font of the text displayed on the caption.")]
		[DefaultValue(typeof(Font),"Microsoft Sans Serif; 8,25pt; style=Bold")]
        [Category("Appearance")]
		public Font CaptionFont
		{
			get { return this.m_captionFont; }
            set
            {
                if (value != null)
                {
                    if (value.Equals(this.m_captionFont) == false)
                    {
                        this.m_captionFont = value;
                        this.Invalidate(this.CaptionRectangle);
                    }
                }
            }
		}
		/// <summary>
		/// The foreground color of this component, which is used to display the caption text.
		/// </summary>
		[Description("The foreground color of this component, which is used to display the caption text.")]
		[DefaultValue(typeof(SystemColors), "System.Drawing.SystemColors.ActiveCaptionText")]
		[Category("Appearance")]
		public virtual Color CaptionForeColor
		{
			get { return this.m_captionForeColor; }
            set
            {
                if (value.Equals(this.m_captionForeColor) == false)
                {
                    this.m_captionForeColor = value;
                    OnCaptionForeColorChanged(this, EventArgs.Empty);
                }
            }
		}
        /// <summary>
        /// Gets or sets the foreground color of the close icon in a Panel or XPanderPanel.
        /// </summary>
        [Description("Gets or sets the foreground color of the close icon in a Panel or XPanderPanel.")]
        [DefaultValue(typeof(Color), "System.Drawing.Color.Empty")]
        [Category("Appearance")]
        public virtual Color CloseIconForeColor
        {
            get { return this.m_colorCloseIcon; }
            set
            {
                if (value.Equals(this.m_colorCloseIcon) == false)
                {
                    this.m_colorCloseIcon = value;
                    OnCloseIconForeColorChanged(this, EventArgs.Empty);
                }
            }
        }
		/// <summary>
		/// Gets or sets the starting color of the gradient used in the caption
		/// </summary>
		[Description("Gets or sets the starting color of the gradient used in the caption")]
		[Category("Appearance")]
		public Color ColorCaptionGradientBegin
		{
			get { return this.m_colorCaptionGradientBegin; }
			set
			{
                if (value.Equals(this.m_colorCaptionGradientBegin) == false)
                {
                    this.m_colorCaptionGradientBegin = value;
                    OnColorCaptionGradientBeginChanged(this, EventArgs.Empty);
                }
			}
		}
		/// <summary>
		/// Gets or sets the middle color of the gradient used in the caption
		/// </summary>
		[Description("Gets or sets the middle color of the gradient used in the caption")]
		[Category("Appearance")]
		public Color ColorCaptionGradientMiddle
		{
			get { return this.m_colorCaptionGradientMiddle; }
			set
			{
                if (value.Equals(this.m_colorCaptionGradientMiddle) == false)
                {
                    this.m_colorCaptionGradientMiddle = value;
                    OnColorCaptionGradientMiddleChanged(this, EventArgs.Empty);
                }
			}
		}
		/// <summary>
		/// Gets or sets the end color of the gradient used in the caption
		/// </summary>
		[Description("Gets or sets the end color of the gradient used in the caption")]
		[Category("Appearance")]
		public Color ColorCaptionGradientEnd
		{
			get { return this.m_colorCaptionGradientEnd; }
			set
			{
                if (value.Equals(this.m_colorCaptionGradientEnd) == false)
                {
                    this.m_colorCaptionGradientEnd = value;
                    OnColorCaptionGradientEndChanged(this, EventArgs.Empty);
                }
			}
		}
		/// <summary>
		/// Gets or sets the border color of the panel.
		/// </summary>
		[Description("Gets or sets the border color of the panel.")]
		[Category("Appearance")]
		public Color BorderColor
		{
			get { return this.m_borderColor; }
			set
			{
				if (value.Equals(this.m_borderColor) == false)
				{
					this.m_borderColor = value;
                    OnBorderColorChanged(this, EventArgs.Empty);
				}
			}
		}
        /// <summary>
        /// Gets or sets the inner border color of the panel.
        /// </summary>
        [Description("Gets or sets the inner border color of the panel.")]
        [Category("Appearance")]
        public Color InnerBorderColor
        {
            get { return this.m_colorInnerBorder; }
            set
            {
                if (value.Equals(this.m_colorInnerBorder) == false)
                {
                    this.m_colorInnerBorder = value;
                    OnInnerBorderColorChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Expands the panel or xpanderpanel.
        /// </summary>
        [Description("Expand the panel or xpanderpanel")]
        [DefaultValue(false)]
        [Category("Appearance")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public virtual bool Expand
        {
            get { return this.m_bExpand; }
            set
            {
                if (value.Equals(this.m_bExpand) == false)
                {
                    this.m_bExpand = value;
                    if (this.m_bExpand == true)
                    {
                        OnPanelExpanding(this, new XPanderStateChangeEventArgs(this.m_bExpand));
                    }
                    else
                    {
                        OnPanelCollapsing(this, new XPanderStateChangeEventArgs(this.m_bExpand));
                    }
                }
            }
        }
		/// <summary>
		/// Gets or sets a value indicating whether the control shows a border.
		/// </summary>
		[Description("Gets or sets a value indicating whether the control shows a border")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(true)]
		[Browsable(false)]
		[Category("Appearance")]
		public virtual bool ShowBorder
		{
			get { return this.m_bShowBorder; }
			set
			{
                if (value.Equals(this.m_bShowBorder) == false)
                {
                    this.m_bShowBorder = value;
                    this.Invalidate(false);
                }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the expand icon in a Panel or XPanderPanel is visible.
		/// </summary>
		[Description("Gets or sets a value indicating whether the expand icon in a Panel or XPanderPanel is visible.")]
		[DefaultValue(false)]
		[Category("Appearance")]
		public virtual bool ShowExpandIcon
		{
			get { return this.m_bShowExpandIcon; }
			set
			{
				if (value.Equals(this.m_bShowExpandIcon) == false)
				{
					this.m_bShowExpandIcon = value;
					this.Invalidate(false);
				}
			}
		}
        /// <summary>
		/// Gets or sets a value indicating whether the close icon in a Panel or XPanderPanel is visible.
        /// </summary>
        [Description("Gets or sets a value indicating whether the close icon in a Panel or XPanderPanel is visible.")]
        [DefaultValue(false)]
        [Category("Appearance")]
        public virtual bool ShowCloseIcon
        {
            get { return this.m_bShowCloseIcon; }
            set
            {
                if (value.Equals(this.m_bShowCloseIcon) == false)
                {
                    this.m_bShowCloseIcon = value;
                    this.Invalidate(false);
                }
            }
        }
        /// <summary>
        /// Gets the panelcolors table.
        /// </summary>
        protected PanelColors PanelColors
        {
            get { return m_panelColors; }
        }
        /// <summary>
        /// Gets or sets constants that define the state of the caption of a Panel or XPanderPanel.
        /// </summary>
        internal CaptionState State
        {
            get { return this.m_captionState; }
            set { this.m_captionState = value; }
        }
        internal Size ImageSize
        {
            get { return this.m_imageSize; }
        }
        internal Rectangle CaptionRectangle
        {
            get { return new Rectangle(0,0,this.ClientRectangle.Width,this.CaptionHeight); }
        }
        internal Rectangle ImageRectangle
		{
			get
			{
                if (this.m_imageRectangle == Rectangle.Empty)
                {
                    this.m_imageRectangle = new Rectangle(
                        CaptionSpacing,
                        this.CaptionHeight,
                        this.m_imageSize.Width,
                        this.m_imageSize.Height);
                }
                return this.m_imageRectangle;
			}
		}
		#endregion

		#region MethodsPublic
        /// <summary>
        /// Sets the PanelProperties for the Panel
        /// </summary>
        /// <param name="panelColors">The PanelColors table</param>
        public void SetPanelProperties(PanelColors panelColors)
        {
            if (panelColors == null)
            {
                throw new ArgumentException(
                    string.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Resources.IDS_ArgumentException,
                    "panelColors"));
            }
            this.m_panelColors = panelColors;
            this.ColorScheme = ColorScheme.Professional;
            this.Invalidate(true);
        }
		#endregion

		#region MethodsProtected
		/// <summary>
		/// Initializes a new instance of the BasePanel class.
		/// </summary>
		protected BasePanel()
		{
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.SetStyle(ControlStyles.ContainerControl, true);
            this.CaptionFont = new Font(SystemFonts.CaptionFont.FontFamily, SystemFonts.CaptionFont.SizeInPoints - 1.0F, FontStyle.Bold);
			this.CaptionForeColor = SystemColors.ActiveCaptionText;
            this.CloseIconForeColor = SystemColors.ActiveCaptionText;
            this.CaptionHeight = 25;
            this.BorderColor = ProfessionalColors.ToolStripBorder;
            this.InnerBorderColor = Color.White;
            this.ColorCaptionGradientBegin = ProfessionalColors.OverflowButtonGradientBegin;
            this.ColorCaptionGradientMiddle = ProfessionalColors.OverflowButtonGradientMiddle;
            this.ColorCaptionGradientEnd = ProfessionalColors.OverflowButtonGradientEnd;
            this.PanelStyle = PanelStyle.Default;
            this.m_panelColors = new PanelColors(this);
            this.m_imageSize = new Size(16, 16);
            this.m_imageRectangle = Rectangle.Empty;
		}
		/// <summary>
		/// Raises the TextChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            this.Invalidate(this.CaptionRectangle);
            base.OnTextChanged(e);
        }
        /// <summary>
        /// Raises the ColorScheme changed event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnColorSchemeChanged(object sender, ColorSchemeChangeEventArgs e)
        {
            this.PanelColors.Clear();
            this.Invalidate(false);
            if (this.ColorSchemeChanged != null)
            {
                this.ColorSchemeChanged(sender, e);
            }
        }
		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((this.ShowExpandIcon == true) && (this.RectangleExandPanelIcon.Contains(e.X, e.Y) == true))
			{
				OnExpandClick(this, EventArgs.Empty);
			}
			if ((this.ShowCloseIcon == true) && (this.RectangleClosePanelIcon.Contains(e.X, e.Y) == true))
			{
				OnCloseClick(this, EventArgs.Empty);
			}
			base.OnMouseDown(e);
		}
		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if ((this.ShowExpandIcon == true) && (this.RectangleExandPanelIcon.Contains(e.X, e.Y) == true))
			{
				Cursor.Current = Cursors.Hand;
			}
			else if ((this.ShowCloseIcon == true) && (this.RectangleClosePanelIcon.Contains(e.X, e.Y) == true))
			{
				Cursor.Current = Cursors.Hand;
			}
			else
			{
				Cursor.Current = Cursors.Default;
			}
			base.OnMouseMove(e);
		}
        /// <summary>
        /// Raises the PanelExpanding event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A XPanderStateChangeEventArgs that contains the event data.</param>
        protected virtual void OnPanelExpanding(object sender, XPanderStateChangeEventArgs e)
        {
            if (this.PanelExpanding != null)
            {
                this.PanelExpanding(sender, e);
            }
        }
        /// <summary>
        /// Raises the PanelCollapsing event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A XPanderStateChangeEventArgs that contains the event data.</param>
        protected virtual void OnPanelCollapsing(object sender, XPanderStateChangeEventArgs e)
        {
            if (this.PanelCollapsing != null)
            {
                this.PanelCollapsing(sender, e);
            }
        }
        /// <summary>
        /// Raises the PanelStyle changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A PanelStyleChangeEventArgs that contains the event data.</param>
        protected virtual void OnPanelStyleChanged(object sender, PanelStyleChangeEventArgs e)
        {
            PanelStyle panelStyle = e.PanelStyle;
            switch (panelStyle)
            {
                case PanelStyle.Default:
                case PanelStyle.Aqua:
                    m_panelColors = new PanelColors(this);
                    break;
                case PanelStyle.Office2007:
                    m_panelColors = new PanelColorsOffice2007(this);
                    break;
            }
            Invalidate(true);
            if (this.PanelStyleChanged != null)
            {
                this.PanelStyleChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the BorderColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
		protected virtual void OnBorderColorChanged(object sender, EventArgs e)
        {
            if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
            {
                this.PanelColors.Clear();
                this.Invalidate(false);
            }
            if (this.BorderColorChanged != null)
            {
                this.BorderColorChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the InnerBorderColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnInnerBorderColorChanged(object sender, EventArgs e)
        {
            if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
            {
                this.PanelColors.Clear();
                this.Invalidate(false);
            }
            if (this.InnerBorderColorChanged != null)
            {
                this.InnerBorderColorChanged(sender, e);
            }
        }
		/// <summary>
		/// Raises the CloseClick event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected virtual void OnCloseClick(object sender, EventArgs e)
		{
			if (this.CloseClick != null)
			{
				this.CloseClick(sender, e);
			}
		}
		/// <summary>
		/// Raises the ExpandClick event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected virtual void OnExpandClick(object sender, EventArgs e)
		{
			this.Invalidate(false);
			if (this.ExpandClick != null)
			{
				this.ExpandClick(sender, e);
			}
		}
        /// <summary>
        /// Raises the CaptionForeColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnCaptionForeColorChanged(object sender, EventArgs e)
        {
            if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
            {
                this.PanelColors.Clear();
                this.Invalidate(this.CaptionRectangle);
            }
            if (this.CaptionForeColorChanged != null)
            {
                this.CaptionForeColorChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the CaptionHeight changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnCaptionHeightChanged(object sender, EventArgs e)
        {
            OnLayout(new LayoutEventArgs(this,null));
            this.Invalidate(false);
            if (this.CaptionHeightChanged != null)
            {
                this.CaptionHeightChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the CloseIconForeColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnCloseIconForeColorChanged(object sender, EventArgs e)
        {
            if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
            {
                this.PanelColors.Clear();
                this.Invalidate(this.RectangleClosePanelIcon);
            }
            if (this.CloseIconForeColorChanged != null)
            {
                this.CloseIconForeColorChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the ColorCaptionGradientBegin changed event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnColorCaptionGradientBeginChanged(object sender, EventArgs e)
        {
            if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
            {
                this.PanelColors.Clear();
                this.Invalidate(this.CaptionRectangle);
            }
            if (this.ColorCaptionGradientBeginChanged != null)
            {
                this.ColorCaptionGradientBeginChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the ColorCaptionGradientEnd changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnColorCaptionGradientEndChanged(object sender, EventArgs e)
        {
            if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
            {
                this.PanelColors.Clear();
                this.Invalidate(this.CaptionRectangle);
            }
            if (this.ColorCaptionGradientEndChanged != null)
            {
                this.ColorCaptionGradientEndChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the ColorCaptionGradientMiddle changed event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnColorCaptionGradientMiddleChanged(object sender, EventArgs e)
        {
            if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
            {
                this.PanelColors.Clear();
                this.Invalidate(this.CaptionRectangle);
            }
            if (this.ColorCaptionGradientMiddleChanged != null)
            {
                this.ColorCaptionGradientMiddleChanged(sender, e);
            }
        }
		/// <summary>
		/// Draws the specified text string on the specified caption surface; within the specified bounds; and in the specified font, color. 
		/// </summary>
		/// <param name="graphics">The Graphics to draw on.</param>
		/// <param name="layoutRectangle">Rectangle structure that specifies the location of the drawn text.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <param name="fontColor">The color of the string</param>
		/// <param name="strText">String to draw.</param>
        /// <param name="rightToLeft">Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</param>
		/// <param name="stringAlignment">The alignment of a text string relative to its layout rectangle.</param>
        protected static void DrawString(
            Graphics graphics,
            Rectangle layoutRectangle,
            Font font,
            Color fontColor,
            string strText,
            RightToLeft rightToLeft,
            StringAlignment stringAlignment)
		{
            if (graphics == null)
            {
                throw new ArgumentException(
                    string.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Resources.IDS_ArgumentException,
                    typeof(Graphics).Name));
            }

            using (SolidBrush stringBrush = new SolidBrush(fontColor))
			{
				using (StringFormat stringFormat = new StringFormat())
				{
                    if (rightToLeft == RightToLeft.Yes)
                    {
                        stringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.DirectionRightToLeft;
                    }
                    else
                    {
                        stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                    }
					stringFormat.Trimming = StringTrimming.EllipsisCharacter;
					stringFormat.LineAlignment = StringAlignment.Center;
                    stringFormat.Alignment = stringAlignment;
					graphics.DrawString(strText, font, stringBrush, layoutRectangle, stringFormat);
				}
			}
		}
		/// <summary>
		/// Draws the Chevron Image at the specified location.
		/// </summary>
		/// <param name="graphics">The Graphics to draw on.</param>
		/// <param name="imgChevron">Chevron image to draw.</param>
        /// <param name="imageRectangle">A Rectangle structure that specifies the bounds of the linear gradient.</param>
        /// <param name="foreColorImage">the foreground color of this image</param>
        /// <param name="bDrawBackground">Gets or sets a value indicating whether the background of the close and expand images is drawn</param>
        protected static void DrawChevron(Graphics graphics, Image imgChevron, Rectangle imageRectangle, Color foreColorImage, bool bDrawBackground, int chevronPositionY)
		{
			if (graphics == null)
			{
                throw new ArgumentException(
                    string.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Resources.IDS_ArgumentException,
                    typeof(Graphics).Name));
			}
			if (imgChevron == null)
			{
                throw new ArgumentException(
                    string.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Resources.IDS_ArgumentException,
                    typeof(Image).Name));
			}

			int chevronPositionX = imageRectangle.Left;

            Rectangle rectangleChevron = new Rectangle(
                chevronPositionX + (imageRectangle.Width / 3) - 2,
				chevronPositionY + 3,
			    9,
			    9);

			using (System.Drawing.Imaging.ImageAttributes imageAttribute = new System.Drawing.Imaging.ImageAttributes())
			{
				imageAttribute.SetColorKey(Color.Magenta, Color.Magenta);
				System.Drawing.Imaging.ColorMap colorMap = new System.Drawing.Imaging.ColorMap();
                colorMap.OldColor = Color.FromArgb(0, 60, 166);
                colorMap.NewColor = foreColorImage;
				imageAttribute.SetRemapTable(new System.Drawing.Imaging.ColorMap[] { colorMap });

                if (bDrawBackground == true)
                {
                    using (LinearGradientBrush brushInnerCircle = new LinearGradientBrush(
                        new Rectangle(chevronPositionX, chevronPositionY, 16, 16),
                        Color.FromArgb(128, Color.White),
                        Color.FromArgb(0, Color.White),
                        LinearGradientMode.Vertical))
                    {
                        graphics.FillPath(brushInnerCircle, GetPath(new Rectangle(chevronPositionX, chevronPositionY, 15, 15), 5));
                    }
                }
				graphics.DrawImage(imgChevron, rectangleChevron, 0, 0, 9, 9, GraphicsUnit.Pixel, imageAttribute);
			}
		}
		/// <summary>
		/// Draws the specified Image at the specified location. 
		/// </summary>
		/// <param name="graphics">The Graphics to draw on.</param>
		/// <param name="image">Image to draw.</param>
		/// <param name="imageRectangle">Rectangle structure that specifies the location and size of the drawn image.</param>
		protected static void DrawImage(Graphics graphics, Image image, Rectangle imageRectangle)
		{
            if (graphics == null)
            {
                throw new ArgumentNullException(
                    string.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Resources.IDS_ArgumentException,
                    typeof(Graphics).Name));
            }
            if (image != null)
			{
				graphics.DrawImage(image, imageRectangle);
			}
		}
		/// <summary>
        /// Draws the text and image objects at the specified location. 
		/// </summary>
		/// <param name="graphics">The Graphics to draw on.</param>
        /// <param name="captionRectangle">The drawing rectangle on a panel's caption.</param>
		/// <param name="iSpacing">The spacing on a panel's caption</param>
		/// <param name="imageRectangle">The rectangle of an image displayed on a panel's caption.</param>
		/// <param name="image">The image that is displayed on a panel's caption.</param>
		/// <param name="rightToLeft">A value indicating whether control's elements are aligned to support locales using right-to-left fonts.</param>
		/// <param name="fontCaption">The font of the text displayed on a panel's caption.</param>
		/// <param name="captionForeColor">The foreground color of the text displayed on a panel's caption.</param>
		/// <param name="strCaptionText">The text which is associated with this caption.</param>
		protected static void DrawImagesAndText(
			Graphics graphics,
			Rectangle captionRectangle,
			int iSpacing,
			Rectangle imageRectangle,
			Image image,
			RightToLeft rightToLeft,
			Font fontCaption,
			Color captionForeColor,
			string strCaptionText)
		{
			//DrawImages
            int iTextPositionX1 = iSpacing;
			int iTextPositionX2 = captionRectangle.Right - iSpacing;

            imageRectangle.Y = (captionRectangle.Height - imageRectangle.Height) / 2;
			
			if (rightToLeft == RightToLeft.No)
			{
				if (image != null)
				{
					DrawImage(graphics, image, imageRectangle);
					iTextPositionX1 += imageRectangle.Width + iSpacing;
				}
			}
			//
            // Draw Caption text
            //
            Rectangle textRectangle = captionRectangle;
            textRectangle.X = iTextPositionX1;
            textRectangle.Width -= iTextPositionX1 + iSpacing;
			if (rightToLeft == RightToLeft.Yes)
			{
				if (image != null)
				{
					Rectangle imageRectangleRight = imageRectangle;
					imageRectangleRight.X = iTextPositionX2 - imageRectangle.Width;
					DrawImage(graphics, image, imageRectangleRight);
					iTextPositionX2 = imageRectangleRight.X - iSpacing;
				}
			}
			textRectangle.Width = iTextPositionX2 - iTextPositionX1;
			DrawString(graphics, textRectangle, fontCaption, captionForeColor, strCaptionText, rightToLeft, StringAlignment.Near);

		}
        /// <summary>
        /// Draws the text and image objects at the specified location. 
        /// </summary>
        /// <param name="graphics">The Graphics to draw on.</param>
        /// <param name="captionRectangle">The drawing rectangle on a panel's caption.</param>
        /// <param name="iSpacing">The spacing on a panel's caption</param>
        /// <param name="imageRectangle">The rectangle of an image displayed on a panel's caption.</param>
        /// <param name="image">The image that is displayed on a panel's caption.</param>
        /// <param name="rightToLeft">A value indicating whether control's elements are aligned to support locales using right-to-left fonts.</param>
        /// <param name="bDrawBackground">A value indicating whether the background of the images is displayed</param>
        /// <param name="bIsClosable">A value indicating whether the xpanderpanel is closable</param>
        /// <param name="bShowCloseIcon">A value indicating whether the close image is displayed</param>
		/// <param name="imageClosePanel">The close image that is displayed on a panel's caption.</param>
        /// <param name="foreColorCloseIcon">The foreground color of the close image that is displayed on a panel's caption.</param>
		/// <param name="rectangleImageClosePanel">The rectangle of the close image that is displayed on a panel's caption.</param>
        /// <param name="bShowExpandIcon">A value indicating whether the expand image is displayed</param>
		/// <param name="imageExandPanel">The expand image that is displayed on a panel's caption.</param>
        /// <param name="foreColorExpandIcon">The foreground color of the expand image displayed by this caption.</param>
        /// <param name="rectangleImageExandPanel">the rectangle of the expand image displayed by this caption.</param>
		/// <param name="fontCaption">The font of the text displayed on a panel's caption.</param>
		/// <param name="captionForeColor">The foreground color of the text displayed on a panel's caption.</param>
        /// <param name="strCaptionText">The text which is associated with this caption.</param>
        protected static void DrawImagesAndText(
            Graphics graphics,
            Rectangle captionRectangle,
            int iSpacing,
            Rectangle imageRectangle,
            Image image,
            RightToLeft rightToLeft,
            bool bDrawBackground,
            bool bIsClosable,
            bool bShowCloseIcon,
            Image imageClosePanel,
            Color foreColorCloseIcon,
            ref Rectangle rectangleImageClosePanel,
            bool bShowExpandIcon,
            Image imageExandPanel,
            Color foreColorExpandIcon,
            ref Rectangle rectangleImageExandPanel,
            Font fontCaption,
            Color captionForeColor,
            string strCaptionText)
        {
            //DrawImages
            int iTextPositionX1 = iSpacing;
            int iTextPositionX2 = captionRectangle.Right - iSpacing;

            imageRectangle.Y = (captionRectangle.Height - imageRectangle.Height) / 2;

            if (rightToLeft == RightToLeft.No)
            {
                if (image != null)
                {
                    DrawImage(graphics, image, imageRectangle);
                    iTextPositionX1 += imageRectangle.Width + iSpacing;
                    iTextPositionX2 -= iTextPositionX1;
                }
            }
            else
            {
                if ((bShowCloseIcon == true) && (imageClosePanel != null))
                {
                    rectangleImageClosePanel = imageRectangle;
                    rectangleImageClosePanel.X = imageRectangle.X;
                    if (bIsClosable == true)
                    {
                        DrawChevron(graphics, imageClosePanel, rectangleImageClosePanel, foreColorCloseIcon, bDrawBackground, imageRectangle.Y);
                    }
                    iTextPositionX1 = rectangleImageClosePanel.X + rectangleImageClosePanel.Width;
                }
                if ((bShowExpandIcon == true) && (imageExandPanel != null))
                {
                    rectangleImageExandPanel = imageRectangle;
                    rectangleImageExandPanel.X = imageRectangle.X;
                    if ((bShowCloseIcon == true) && (imageClosePanel != null))
                    {
                        rectangleImageExandPanel.X = iTextPositionX1 + (iSpacing / 2);
                    }
                    DrawChevron(graphics, imageExandPanel, rectangleImageExandPanel, foreColorExpandIcon, bDrawBackground, imageRectangle.Y);
                    iTextPositionX1 = rectangleImageExandPanel.X + rectangleImageExandPanel.Width;
                }
            }
            //
            // Draw Caption text
            //
            Rectangle textRectangle = captionRectangle;
            textRectangle.X = iTextPositionX1;
            textRectangle.Width -= iTextPositionX1 + iSpacing;
            if (rightToLeft == RightToLeft.No)
            {
                if ((bShowCloseIcon == true) && (imageClosePanel != null))
                {
                    rectangleImageClosePanel = imageRectangle;
                    rectangleImageClosePanel.X = captionRectangle.Right - iSpacing - imageRectangle.Width;
                    if (bIsClosable == true)
                    {
                        DrawChevron(graphics, imageClosePanel, rectangleImageClosePanel, foreColorCloseIcon, bDrawBackground, imageRectangle.Y);
                    }
                    iTextPositionX2 = rectangleImageClosePanel.X;
                }
                if ((bShowExpandIcon == true) && (imageExandPanel != null))
                {
                    rectangleImageExandPanel = imageRectangle;
                    rectangleImageExandPanel.X = captionRectangle.Right - iSpacing - imageRectangle.Width;
                    if ((bShowCloseIcon == true) && (imageClosePanel != null))
                    {
                        rectangleImageExandPanel.X = iTextPositionX2 - (iSpacing / 2) - imageRectangle.Width;
                    }
                    DrawChevron(graphics, imageExandPanel, rectangleImageExandPanel, foreColorExpandIcon, bDrawBackground, imageRectangle.Y);
                    iTextPositionX2 = rectangleImageExandPanel.X;
                }
                if ((bShowCloseIcon == true)
                    && (imageClosePanel != null)
                    && (bShowExpandIcon == true)
                    && (imageExandPanel != null))
                {
                    iTextPositionX2 -= iSpacing;
                }
            }
            else
            {
                if (image != null)
                {
                    Rectangle imageRectangleRight = imageRectangle;
                    imageRectangleRight.X = iTextPositionX2 - imageRectangle.Width;
                    DrawImage(graphics, image, imageRectangleRight);
                    iTextPositionX2 = imageRectangleRight.X - iSpacing;
                }
            }
            textRectangle.Width = iTextPositionX2 - iTextPositionX1;
            DrawString(graphics, textRectangle, fontCaption, captionForeColor, strCaptionText, rightToLeft, StringAlignment.Near);
        }
        /// <summary>
        /// Draws the text and image objects at the specified location.
        /// </summary>
        /// <param name="graphics">The Graphics to draw on.</param>
        /// <param name="panelStyle">The style of the panel.</param>
        /// <param name="dockStyle">Specifies the position and manner in which a control is docked.</param>
        /// <param name="iSpacing">The spacing on a panel's caption</param>
        /// <param name="captionRectangle">The rectangle of the panel's caption bar.</param>
        /// <param name="panelRectangle">The rectangle that represents the client area of the panel.</param>
        /// <param name="imageRectangle">The rectangle of an image displayed on a panel's caption.</param>
        /// <param name="image">The image that is displayed on a panel's caption.</param>
        /// <param name="rightToLeft">A value indicating whether control's elements are aligned to support locales using right-to-left fonts.</param>
        /// <param name="bShowCloseIcon">A value indicating whether the close image is displayed</param>
        /// <param name="imageClosePanel">The close image that is displayed on a panel's caption.</param>
        /// <param name="foreColorCloseIcon">The foreground color of the close image that is displayed on a panel's caption.</param>
        /// <param name="rectangleImageClosePanel">The rectangle of the close image that is displayed on a panel's caption.</param>
        /// <param name="bShowExpandIcon">A value indicating whether the expand image is displayed</param>
        /// <param name="bIsExpanded">A value indicating whether the panel is expanded or collapsed</param>
        /// <param name="imageExandPanel">The expand image that is displayed on a panel's caption.</param>
        /// <param name="foreColorExpandPanel">The foreground color of the expand image displayed by this caption.</param>
        /// <param name="rectangleImageExandPanel"></param>
        /// <param name="fontCaption">The font of the text displayed on a panel's caption.</param>
        /// <param name="captionForeColor">The foreground color of the text displayed on a panel's caption.</param>
        /// <param name="collapsedForeColor"></param>
        /// <param name="strCaptionText">The text which is associated with this caption.</param>
        protected static void DrawImagesAndText(
            Graphics graphics, 
            PanelStyle panelStyle,
			DockStyle dockStyle,
            int iSpacing,
            Rectangle captionRectangle,
			Rectangle panelRectangle,
            Rectangle imageRectangle,
			Image image,
			RightToLeft rightToLeft,
            bool bShowCloseIcon,
            Image imageClosePanel,
            Color foreColorCloseIcon,
            ref Rectangle rectangleImageClosePanel,
            bool bShowExpandIcon,
            bool bIsExpanded, 
            Image imageExandPanel, 
            Color foreColorExpandPanel, 
            ref Rectangle rectangleImageExandPanel,
			Font fontCaption,
            Color captionForeColor, 
            Color collapsedForeColor,
			string strCaptionText)
        {
			bool bDrawBackground = false;
			if (panelStyle == PanelStyle.Aqua)
			{
				bDrawBackground = true;
			}

			switch (dockStyle)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    if (bIsExpanded == true)
                    {
                        DrawImagesAndText(
                        graphics,
                        captionRectangle,
                        iSpacing,
                        imageRectangle,
                        image,
                        rightToLeft,
                        bDrawBackground,
                        true,
                        bShowCloseIcon,
                        imageClosePanel,
                        foreColorCloseIcon,
                        ref rectangleImageClosePanel,
                        bShowExpandIcon,
                        imageExandPanel,
                        foreColorExpandPanel,
                        ref rectangleImageExandPanel,
                        fontCaption,
                        captionForeColor,
                        strCaptionText);
                    }
                    else
                    {
                        rectangleImageClosePanel = Rectangle.Empty;
                        DrawVerticalImagesAndText(
                            graphics,
							captionRectangle,
							panelRectangle,
                            imageRectangle,
							dockStyle,
							image,
							rightToLeft,
                            bDrawBackground,
                            imageExandPanel,
                            foreColorExpandPanel,
                            ref rectangleImageExandPanel,
							fontCaption,
                            collapsedForeColor,
							strCaptionText);
                    }
                    break;
                case DockStyle.Top:
                case DockStyle.Bottom:
                    DrawImagesAndText(
                        graphics,
                        captionRectangle,
                        iSpacing,
                        imageRectangle,
                        image,
                        rightToLeft,
                        bDrawBackground,
                        true,
                        bShowCloseIcon,
                        imageClosePanel,
                        foreColorCloseIcon,
                        ref rectangleImageClosePanel,
                        bShowExpandIcon,
                        imageExandPanel,
                        foreColorExpandPanel,
                        ref rectangleImageExandPanel,
                        fontCaption,
                        captionForeColor,
                        strCaptionText);
                    break;
            }
        }
		/// <summary>
		/// Renders the background of the caption bar
		/// </summary>
		/// <param name="graphics">The Graphics to draw on.</param>
		/// <param name="bounds">Rectangle structure that specifies the location of the caption bar.</param>
		/// <param name="beginColor">The starting color of the gradient used on the caption bar</param>
		/// <param name="middleColor">The middle color of the gradient used on the caption bar</param>
		/// <param name="endColor">The end color of the gradient used on the caption bar</param>
		/// <param name="linearGradientMode">Specifies the type of fill a Pen object uses to fill lines.</param>
		/// <param name="flipHorizontal"></param>
		protected static void RenderDoubleBackgroundGradient(Graphics graphics, Rectangle bounds, Color beginColor, Color middleColor, Color endColor, LinearGradientMode linearGradientMode, bool flipHorizontal)
		{
			RenderDoubleBackgroundGradient(
				graphics,
				bounds,
				beginColor,
				middleColor,
				endColor,
				12,
				12,
				linearGradientMode,
				flipHorizontal);
		}
		/// <summary>
		/// Renders the panels background
		/// </summary>
		/// <param name="graphics">The Graphics to draw on.</param>
		/// <param name="bounds">Rectangle structure that specifies the backgrounds location.</param>
		/// <param name="beginColor">The starting color of the gradient used on the panels background</param>
		/// <param name="endColor">The end color of the gradient used on the panels background</param>
		/// <param name="linearGradientMode">Specifies the type of fill a Pen object uses to fill lines.</param>
		protected static void RenderBackgroundGradient(Graphics graphics, Rectangle bounds, Color beginColor, Color endColor, LinearGradientMode linearGradientMode)
		{
            if (graphics == null)
            {
                throw new ArgumentException(
                    string.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Resources.IDS_ArgumentException,
                    typeof(Graphics).Name));
            }
            if (IsZeroWidthOrHeight(bounds))
            {
                return;
            }
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, beginColor, endColor, linearGradientMode))
			{
                graphics.FillRectangle(linearGradientBrush, bounds);
			}
		}
		/// <summary>
		/// Gets a GraphicsPath. 
		/// </summary>
		/// <param name="bounds">Rectangle structure that specifies the backgrounds location.</param>
        /// <param name="radius">The radius in the graphics path</param>
		/// <returns>the specified graphics path</returns>
        protected static GraphicsPath GetPath(Rectangle bounds, int radius)
		{
			int x = bounds.X;
			int y = bounds.Y;
			int width = bounds.Width;
			int height = bounds.Height;
			GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(x, y, radius, radius, 180, 90);				                    //Upper left corner
            graphicsPath.AddArc(x + width - radius, y, radius, radius, 270, 90);			    //Upper right corner
            graphicsPath.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);//Lower right corner
            graphicsPath.AddArc(x, y + height - radius, radius, radius, 90, 90);			                    //Lower left corner
			graphicsPath.CloseFigure();
			return graphicsPath;
		}
		/// <summary>
		/// Gets a GraphicsPath.
		/// </summary>
		/// <param name="bounds">Rectangle structure that specifies the backgrounds location.</param>
        /// <param name="radius">The radius in the graphics path</param>
		/// <returns>the specified graphics path</returns>
        protected static GraphicsPath GetBackgroundPath(Rectangle bounds, int radius)
		{
			int x = bounds.X;
			int y = bounds.Y;
			int width = bounds.Width;
			int height = bounds.Height;
			GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddLine(x, y + height, x, y - radius);                 //Left Line
            graphicsPath.AddArc(x, y, radius, radius, 180, 90);                 //Upper left corner
            graphicsPath.AddArc(x + width - radius, y, radius, radius, 270, 90);//Upper right corner
            graphicsPath.AddLine(x + width, y + radius, x + width, y + height); //Right Line
			graphicsPath.CloseFigure();
			return graphicsPath;
		}
        /// <summary>
        /// Gets the linear GradientBackBrush. 
        /// </summary>
        /// <param name="bounds">Rectangle structure that specifies the bounds of the linear gradient.</param>
        /// <param name="backColor">A Color structure that represents the ending color for the gradient.</param>
        /// <returns></returns>
        protected static LinearGradientBrush GetGradientBackBrush(Rectangle bounds, Color backColor)
        {
            if (IsZeroWidthOrHeight(bounds))
            {
                return null;
            }
            LinearGradientBrush linearGradientBrush = linearGradientBrush = new LinearGradientBrush(bounds, Color.White, backColor, LinearGradientMode.Vertical);
            if (linearGradientBrush != null)
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0.0F, 0.2F, 0.3F, 0.4F, 0.8F, 1.0F };
                blend.Factors = new float[] { 0.3F, 0.4F, 0.5F, 1.0F, 0.8F, 0.7F };
                linearGradientBrush.Blend = blend;
            }
            return linearGradientBrush;
        }
        /// <summary>
        /// Gets the linear GradientBackBrush for flat XPanderPanel captions.
        /// </summary>
        /// <param name="bounds">Rectangle structure that specifies the bounds of the linear gradient.</param>
        /// <param name="backColor">A Color structure that represents the color for the gradient.</param>
        /// <param name="bInverseGradient">A value indicating whether the gradient inverts</param>
        /// <returns></returns>
        protected static LinearGradientBrush GetFlatGradientBackBrush(Rectangle bounds, Color colorGradientBegin, Color colorGradientEnd, bool bHover)
        {
            LinearGradientBrush linearGradientBrush = null;
            Blend blend = new Blend();
                blend.Positions = new float[] { 0.0F, 0.2F, 0.3F, 0.4F, 0.5F, 0.6F, 0.7F, 0.8F, 1.0F };
            if (bHover == false)
            {
                blend.Factors = new float[] { 0.0F, 0.0F, 0.2F, 0.4F, 0.6F, 0.4F, 0.2F, 0.0F, 0.0F };
            }
            else
            {
                blend.Factors = new float[] { 0.4F, 0.5F, 0.6F, 0.8F, 1.0F, 0.8F, 0.6F, 0.5F, 0.4F };
            }
            linearGradientBrush = linearGradientBrush = new LinearGradientBrush(bounds, colorGradientBegin, colorGradientEnd, LinearGradientMode.Horizontal);
            if (linearGradientBrush != null)
            {
                linearGradientBrush.Blend = blend;
            }
            return linearGradientBrush;
        }
        /// <summary>
        /// Checks if the rectangle width or height is equal to 0.
        /// </summary>
        /// <param name="rectangle">the rectangle to check</param>
        /// <returns>true if the with or height of the rectangle is 0 else false</returns>
        protected static bool IsZeroWidthOrHeight(Rectangle rectangle)
		{
			if (rectangle.Width != 0)
			{
				return (rectangle.Height == 0);
			}
			return true;
		}
		#endregion

		#region MethodsPrivate

		private static void RenderDoubleBackgroundGradient(Graphics graphics, Rectangle bounds, Color beginColor, Color middleColor, Color endColor, int firstGradientWidth, int secondGradientWidth, LinearGradientMode mode, bool flipHorizontal)
		{
			if ((bounds.Width != 0) && (bounds.Height != 0))
			{
				Rectangle rectangle1 = bounds;
				Rectangle rectangle2 = bounds;
				bool flag1 = true;
				if (mode == LinearGradientMode.Horizontal)
				{
					if (flipHorizontal)
					{
						Color color1 = endColor;
						endColor = beginColor;
						beginColor = color1;
					}
					rectangle2.Width = firstGradientWidth;
					rectangle1.Width = secondGradientWidth + 1;
					rectangle1.X = bounds.Right - rectangle1.Width;
					flag1 = bounds.Width > (firstGradientWidth + secondGradientWidth);
				}
				else
				{
					rectangle2.Height = firstGradientWidth;
					rectangle1.Height = secondGradientWidth + 1;
					rectangle1.Y = bounds.Bottom - rectangle1.Height;
					flag1 = bounds.Height > (firstGradientWidth + secondGradientWidth);
				}
				if (flag1)
				{
					using (Brush brush1 = new SolidBrush(middleColor))
					{
						graphics.FillRectangle(brush1, bounds);
					}
					using (Brush brush2 = new LinearGradientBrush(rectangle2, beginColor, middleColor, mode))
					{
						graphics.FillRectangle(brush2, rectangle2);
					}
					using (LinearGradientBrush brush3 = new LinearGradientBrush(rectangle1, middleColor, endColor, mode))
					{
						if (mode == LinearGradientMode.Horizontal)
						{
							rectangle1.X++;
							rectangle1.Width--;
						}
						else
						{
							rectangle1.Y++;
							rectangle1.Height--;
						}
						graphics.FillRectangle(brush3, rectangle1);
						return;
					}
				}
				using (Brush brush4 = new LinearGradientBrush(bounds, beginColor, endColor, mode))
				{
					graphics.FillRectangle(brush4, bounds);
				}
			}
		}

        private static void DrawVerticalImagesAndText(
            Graphics graphics,
			Rectangle captionRectangle,
			Rectangle panelRectangle,
            Rectangle imageRectangle,
			DockStyle dockStyle,
			Image image,
			RightToLeft rightToLeft,
            bool bDrawBackground,
            Image imageExandPanel,
            Color foreColorExpandPanel,
            ref Rectangle rectangleImageExandPanel,
			Font captionFont,
			Color collapsedCaptionForeColor,
			string strCaptionText)
        {
            imageRectangle.Y = (captionRectangle.Height - imageRectangle.Height) / 2;
            
            if (imageExandPanel != null)
            {
                rectangleImageExandPanel = imageRectangle;
				rectangleImageExandPanel.X = (panelRectangle.Width - imageRectangle.Width) / 2;
                DrawChevron(graphics, imageExandPanel, rectangleImageExandPanel, foreColorExpandPanel, bDrawBackground, imageRectangle.Y);
            }

            int iTextPositionY1 = CaptionSpacing;
			int iTextPositionY2 = panelRectangle.Height - CaptionSpacing;

			if (image != null)
            {
                imageRectangle.Y = iTextPositionY2 - imageRectangle.Height;
				imageRectangle.X = (panelRectangle.Width - imageRectangle.Width) / 2;
				DrawImage(graphics, image, imageRectangle);
                iTextPositionY1 += imageRectangle.Height + CaptionSpacing;
            }

			iTextPositionY2 -= captionRectangle.Height + iTextPositionY1;
            
            Rectangle textRectangle = new Rectangle(
                iTextPositionY1,
				panelRectangle.Y,
                iTextPositionY2,
				captionRectangle.Height);

            using (SolidBrush textBrush = new SolidBrush(collapsedCaptionForeColor))
            {
				if (dockStyle == DockStyle.Left)
                {
					graphics.TranslateTransform(0, panelRectangle.Height);
                    graphics.RotateTransform(-90);

                    DrawString(
                        graphics,
                        textRectangle,
						captionFont,
                        collapsedCaptionForeColor,
						strCaptionText,
						rightToLeft,
                        StringAlignment.Center);

                    graphics.ResetTransform();
                }
				if (dockStyle == DockStyle.Right)
                {
					graphics.TranslateTransform(panelRectangle.Width, 0);
                    graphics.RotateTransform(90);

                    DrawString(
                        graphics,
                        textRectangle,
						captionFont,
                        collapsedCaptionForeColor,
						strCaptionText,
						rightToLeft,
                        StringAlignment.Center);

                    graphics.ResetTransform();
                }
            }
        }
		#endregion
	}
}
