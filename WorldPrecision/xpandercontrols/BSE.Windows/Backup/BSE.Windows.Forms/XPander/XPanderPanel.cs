using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel.Design;
using BSE.Windows.Forms.Properties;

namespace BSE.Windows.Forms
{
    #region Class XPanderPanel
	/// <summary>
	/// Used to group collections of controls. 
	/// </summary>
	/// <remarks>
	/// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
	/// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
	/// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
	/// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
	/// REMAINS UNCHANGED.
	/// </remarks>
	/// <copyright>Copyright © 2006-2008 Uwe Eichkorn</copyright>
    [Designer(typeof(XPanderPanelDesigner))]
    [DesignTimeVisible(false)]
	public partial class XPanderPanel : BasePanel
	{
		#region EventsPublic
        /// <summary>
        /// The CaptionStyleChanged event occurs when CaptionStyle flags have been changed.
        /// </summary>
        [Description("The CaptionStyleChanged event occurs when CaptionStyle flags have been changed.")]
        public event EventHandler<EventArgs> CaptionStyleChanged;
        /// <summary>
        /// Occurs when the value of the ColorFlatCaptionGradientBegin property changes.
        /// </summary>
        [Description("Occurs when the value of the ColorFlatCaptionGradientBegin property changes.")]
        public event EventHandler<EventArgs> ColorFlatCaptionGradientBeginChanged;
        /// <summary>
        /// Occurs when the value of the ColorFlatCaptionGradientend property changes.
        /// </summary>
        [Description("Occurs when the value of the ColorFlatCaptionGradientEnd property changes.")]
        public event EventHandler<EventArgs> ColorFlatCaptionGradientEndChanged;
        #endregion
		
		#region Constants
		#endregion

		#region FieldsPrivate
		
		private System.Drawing.Image m_imageChevron;
        private System.Drawing.Image m_imageChevronUp;
        private System.Drawing.Image m_imageChevronDown;
        private Color m_backColor;
        private Color m_colorFlatCaptionGradientBegin;
        private Color m_colorFlatCaptionGradientEnd;
        private System.Drawing.Image m_imageClosePanel;
        private bool m_bIsClosable = true;
        private CaptionStyle m_captionStyle;

		#endregion

		#region Properties
		/// <summary>
        /// Gets or sets a value indicating whether the expand icon in a XPanderPanel is visible.
        /// </summary>
		[Description("Gets or sets a value indicating whether the expand icon in a XPanderPanel is visible.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		[Browsable(false)]
		[Category("Appearance")]
		public override bool ShowExpandIcon
		{
			get
			{
				return base.ShowExpandIcon;
			}
			set
			{
				base.ShowExpandIcon = value;
			}
		}
        /// <summary>
        /// Gets or sets a value indicating whether the close icon in a XPanderPanel is visible.
        /// </summary>
        [Description("Gets or sets a value indicating whether the close icon in a XPanderPanel is visible.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(false)]
        [Browsable(false)]
        [Category("Appearance")]
        public override bool ShowCloseIcon
        {
            get
            {
                return base.ShowCloseIcon;
            }
            set
            {
                base.ShowCloseIcon = value;
            }
        }
        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        [Description("Gets or sets the background color of the control.")]
        [Category("Appearance")]
        public new Color BackColor
        {
            get { return this.m_backColor; }
            set
            {
                if (value.Equals(this.m_backColor) == false)
                {
                    this.m_backColor = value;
                    OnBackColorChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the starting color of the gradient used in the flat caption.
        /// </summary>
        [Description("Gets or sets the starting color of the gradient used in the flat caption.")]
        [Category("Appearance")]
        public Color ColorFlatCaptionGradientBegin
        {
            get { return this.m_colorFlatCaptionGradientBegin; }
            set
            {
                if (value.Equals(this.m_colorFlatCaptionGradientBegin) == false)
                {
                    this.m_colorFlatCaptionGradientBegin = value;
                    OnColorFlatCaptionGradientBeginChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the starting color of the gradient used in the flat caption.
        /// </summary>
        [Description("Gets or sets the starting color of the gradient used in the flat caption.")]
        [Category("Appearance")]
        public Color ColorFlatCaptionGradientEnd
        {
            get { return this.m_colorFlatCaptionGradientEnd; }
            set
            {
                if (value.Equals(this.m_colorFlatCaptionGradientEnd) == false)
                {
                    this.m_colorFlatCaptionGradientEnd = value;
                    OnColorFlatCaptionGradientEndChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the style of the caption (not for PanelStyle.Aqua).
        /// </summary>
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public CaptionStyle CaptionStyle
        {
            get { return this.m_captionStyle; }
            set
            {
                if (value.Equals(this.m_captionStyle) == false)
                {
                    this.m_captionStyle = value;
                    OnCaptionStyleChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this XPanderPanel is closable.
        /// </summary>
        [Description("Gets or sets a value indicating whether this XPanderPanel is closable.")]
        [DefaultValue(true)]
        [Category("Appearance")]
        public bool IsClosable
        {
            get { return this.m_bIsClosable; }
            set
            {
                if (value.Equals(this.m_bIsClosable) == false)
                {
                    this.m_bIsClosable = value;
                    this.Invalidate(false);
                }
            }
        }
        /// <summary>
        /// Gets or sets the height and width of the XPanderPanel.
        /// </summary>
        [Browsable(false)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }
		#endregion

		#region MethodsPublic
		/// <summary>
		/// Initializes a new instance of the XPanderPanel class.
		/// </summary>
		public XPanderPanel()
		{
			InitializeComponent();

			this.CaptionForeColor = SystemColors.ControlText;
            this.BackColor = Color.Transparent;
            this.CaptionStyle = CaptionStyle.Normal;
            this.ForeColor = SystemColors.ControlText;
            this.ColorFlatCaptionGradientBegin = ProfessionalColors.ToolStripGradientMiddle;
            this.ColorFlatCaptionGradientEnd = ProfessionalColors.ToolStripGradientBegin;
			this.Height = this.CaptionHeight;
			this.ShowBorder = true;
		}
        /// <summary>
        /// Gets the rectangle that represents the display area of the XPanderPanel.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Rectangle DisplayRectangle
        {
            get
            {
                Padding padding = this.Padding;
                Rectangle displayRectangle = new Rectangle(
                    padding.Left,
                    padding.Top + this.CaptionHeight,
                    this.ClientRectangle.Width - padding.Left - padding.Right,
                    this.ClientRectangle.Height - this.CaptionHeight - padding.Top - padding.Bottom);
                return displayRectangle;
            }
        }
		#endregion

		#region MethodsProtected
		/// <summary>
		/// Paints the background of the control.
		/// </summary>
		/// <param name="pevent">A PaintEventArgs that contains information about the control to paint.</param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);
            base.BackColor = Color.Transparent;
            Color backColor = this.PanelColors.XPanderPanelBackColor;
            if ((backColor != Color.Empty) && backColor != Color.Transparent)
            {
                Rectangle rectangle = new Rectangle(
                    0,
                    this.CaptionHeight,
                    this.ClientRectangle.Width,
                    this.ClientRectangle.Height - this.CaptionHeight);

                using (SolidBrush backgroundBrush = new SolidBrush(backColor))
                {
                    pevent.Graphics.FillRectangle(backgroundBrush, rectangle);
                }
            }
		}
		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
            using (UseAntiAlias antiAlias = new UseAntiAlias(e.Graphics))
            {
                Graphics graphics = e.Graphics;
                using (UseClearTypeGridFit clearTypeGridFit = new UseClearTypeGridFit(graphics))
                {
                    bool bExpand = this.Expand;
                    bool bShowBorder = this.ShowBorder;
                    Color borderColor = this.PanelColors.BorderColor;
                    Rectangle borderRectangle = this.ClientRectangle;
                    
                    switch (this.PanelStyle)
                    {
                        case PanelStyle.None:
                        case PanelStyle.Default:
                            DrawStyleDefault(graphics, bExpand, bShowBorder);
                            CalculatePanelHeights();
                            DrawBorder(graphics, bShowBorder, borderColor, borderRectangle);
                            break;
                        case PanelStyle.Aqua:
                            DrawStyleAqua(graphics, bExpand);
                            CalculatePanelHeights();
                            break;
                        case PanelStyle.Office2007:
                            DrawOffice2007(graphics, bExpand, bShowBorder);
                            CalculatePanelHeights();
                            DrawBorder(graphics, bShowBorder, borderColor, borderRectangle);
                            break;
                    }
                }
            }
		}
		/// <summary>
        /// Raises the PanelExpanding event.
		/// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A XPanderStateChangeEventArgs that contains the event data.</param>
        protected override void OnPanelExpanding(object sender, XPanderStateChangeEventArgs e)
		{
			bool bExpand = e.Expand;
			if (bExpand == true)
			{
				this.Expand = bExpand;
                this.Invalidate(false);
			}
			base.OnPanelExpanding(sender, e);
		}
        /// <summary>
        /// Raises the BackColorChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
            {
                this.PanelColors.Clear();
                this.Invalidate(false);
            }
            base.OnBackColorChanged(e);
        }
        /// <summary>
        /// Raises the CaptionStyleChanged event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnCaptionStyleChanged(object sender, EventArgs e)
        {
            this.Invalidate(this.CaptionRectangle);
            if (this.CaptionStyleChanged != null)
            {
                this.CaptionStyleChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the ColorFlatCaptionGradientBeginChanged event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnColorFlatCaptionGradientBeginChanged(object sender, EventArgs e)
        {
            if ((this.m_captionStyle == CaptionStyle.Flat) && (this.PanelStyle != PanelStyle.Aqua))
            {
                if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
                {
                    this.PanelColors.Clear();
                }
                this.Invalidate(this.CaptionRectangle);
            }
            if (this.ColorFlatCaptionGradientBeginChanged != null)
            {
                this.ColorFlatCaptionGradientBeginChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the ColorFlatCaptionGradientEndChanged event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnColorFlatCaptionGradientEndChanged(object sender, EventArgs e)
        {
            if ((this.m_captionStyle == CaptionStyle.Flat) && (this.PanelStyle != PanelStyle.Aqua))
            {
                if ((this.PanelColors != null) && (this.ColorScheme == ColorScheme.Custom))
                {
                    this.PanelColors.Clear();
                }
                this.Invalidate(this.CaptionRectangle);
            }
            if (this.ColorFlatCaptionGradientEndChanged != null)
            {
                this.ColorFlatCaptionGradientEndChanged(sender, e);
            }
        }
		/// <summary>
        /// Raises the MouseMove event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
            if (this.CaptionRectangle.Contains(e.X, e.Y) == true)
            {
                if (this.State != CaptionState.Hover)
                {
                    this.State = CaptionState.Hover;
                    this.Invalidate(this.CaptionRectangle);
                }
            }
            else
            {
                if (this.State != CaptionState.Normal)
                {
                    this.State = CaptionState.Normal;
                }
                this.Invalidate(this.CaptionRectangle);
            }

            Cursor.Current = Cursors.Default;
            if (this.CaptionRectangle.Contains(e.X, e.Y) == true)
            {
                if ((this.ShowCloseIcon == false) && (this.ShowExpandIcon == false))
                {
                    Cursor.Current = Cursors.Hand;
                }
                else if ((this.ShowCloseIcon == true) && (this.ShowExpandIcon == false))
                {
                    if (this.RectangleClosePanelIcon.Contains(e.X, e.Y) == false)
                    {
                        Cursor.Current = Cursors.Hand;
                    }
                }
                if (this.ShowExpandIcon == true)
                {
                    if (this.RectangleExandPanelIcon.Contains(e.X, e.Y) == true)
                    {
                        Cursor.Current = Cursors.Hand;
                    }
                }
                if ((this.ShowCloseIcon == true) && (this.m_bIsClosable == true))
                {
                    if (this.RectangleClosePanelIcon.Contains(e.X, e.Y) == true)
                    {
                        Cursor.Current = Cursors.Hand;
                    }
                }
            }
		}
        /// <summary>
        /// Raises the MouseLeave event.
        /// </summary>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.State == CaptionState.Hover)
            {
                this.State = CaptionState.Normal;
                this.Invalidate(this.CaptionRectangle);
            }
            base.OnMouseLeave(e);
        }
		/// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains data about the OnMouseDown event.</param>
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
            if (this.CaptionRectangle.Contains(e.X, e.Y) == true)
            {
                if ((this.ShowCloseIcon == false) && (this.ShowExpandIcon == false))
                {
                    OnExpandClick(this, EventArgs.Empty);
                }
                else if ((this.ShowCloseIcon == true) && (this.ShowExpandIcon == false))
                {
                    if (this.RectangleClosePanelIcon.Contains(e.X, e.Y) == false)
                    {
                        OnExpandClick(this, EventArgs.Empty);
                    }
                }
                if (this.ShowExpandIcon == true)
                {
                    if (this.RectangleExandPanelIcon.Contains(e.X, e.Y) == true)
                    {
                        OnExpandClick(this, EventArgs.Empty);
                    }
                }
                if ((this.ShowCloseIcon == true) && (this.m_bIsClosable == true))
                {
                    if (this.RectangleClosePanelIcon.Contains(e.X, e.Y) == true)
                    {
                        OnCloseClick(this, EventArgs.Empty);
                    }
                }
            }
		}
		/// <summary>
		/// Raises the VisibleChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.DesignMode == true)
            {
                return;
            }
            if (this.Visible == false)
            {
                if (this.Expand == true)
                {
                    this.Expand = false;
                    foreach (Control control in this.Parent.Controls)
                    {
                        BSE.Windows.Forms.XPanderPanel xpanderPanel =
                            control as BSE.Windows.Forms.XPanderPanel;

                        if (xpanderPanel != null)
                        {
                            if (xpanderPanel.Visible == true)
                            {
                                xpanderPanel.Expand = true;
                                return;
                            }
                        }
                    }
                }
            }
#if DEBUG
            //System.Diagnostics.Trace.WriteLine("Visibility: " + this.Name + this.Visible);
#endif
            CalculatePanelHeights();
        }

		#endregion

		#region MethodsPrivate

        private void DrawStyleDefault(Graphics graphics, bool bExpand, bool bShowBorder)
		{
            Rectangle captionRectangle = this.CaptionRectangle;
            Color colorGradientBegin = this.PanelColors.XPanderPanelCaptionGradientBegin;
            Color colorGradientMiddle = this.PanelColors.XPanderPanelCaptionGradientMiddle;
            Color colorGradientEnd = this.PanelColors.XPanderPanelCaptionGradientEnd;
            Color colorPressedGradientBegin = this.PanelColors.XPanderPanelPressedCaptionBegin;
            Color colorPressedGradientEnd = this.PanelColors.XPanderPanelPressedCaptionEnd;
            Color colorPressedGradientMiddle = this.PanelColors.XPanderPanelPressedCaptionMiddle;
            Color colorText = this.PanelColors.XPanderPanelCaptionText;
            Color foreColorCloseIcon = this.PanelColors.XPanderPanelCaptionCloseIcon;
            Color foreColorExpandIcon = this.PanelColors.XPanderPanelCaptionExpandIcon;

            if (this.m_imageClosePanel == null)
            {
                this.m_imageClosePanel = Resources.closePanel;
            }
            if (this.m_imageChevronUp == null)
            {
                this.m_imageChevronUp = Resources.ChevronUp;
            }
            if (this.m_imageChevronDown == null)
            {
                this.m_imageChevronDown = Resources.ChevronDown;
            }
            
            this.m_imageChevron = this.m_imageChevronDown;

            if (bExpand == true)
            {
                this.m_imageChevron = this.m_imageChevronUp;
                if (this.ColorScheme == ColorScheme.Professional)
                {
                    colorGradientBegin = this.PanelColors.XPanderPanelSelectedCaptionBegin;
                    colorGradientMiddle = this.PanelColors.XPanderPanelSelectedCaptionMiddle;
                    colorGradientEnd = this.PanelColors.XPanderPanelSelectedCaptionEnd;
                    colorText = this.PanelColors.XPanderPanelSelectedCaptionText;
                    foreColorCloseIcon = colorText;
                    foreColorExpandIcon = colorText;
                }
            }

            if (this.m_captionStyle == CaptionStyle.Normal)
            {
                if ((this.State == CaptionState.Hover) && (bExpand == false))
                {
                    colorGradientBegin = colorPressedGradientBegin;
                    colorGradientEnd = colorPressedGradientEnd;
                    colorGradientMiddle = colorPressedGradientMiddle;
                    colorText = this.PanelColors.XPanderPanelSelectedCaptionText;
                    foreColorCloseIcon = colorText;
                    foreColorExpandIcon = colorText;
                }
                
                RenderDoubleBackgroundGradient(
                    graphics,
                    captionRectangle,
                    colorGradientBegin,
                    colorGradientMiddle,
                    colorGradientEnd,
                    LinearGradientMode.Vertical,
                    false);
            }
            else
            {
                bool bHover = false;

                Color colorFlatGradientBegin = this.PanelColors.XPanderPanelFlatCaptionGradientBegin;
                Color colorFlatGradientEnd = this.PanelColors.XPanderPanelFlatCaptionGradientEnd;
                Color colorInnerBorder = this.PanelColors.InnerBorderColor;
                if (this.State == CaptionState.Hover)
                {
                    colorText = this.PanelColors.XPanderPanelCaptionText;
                    bHover = true;
                }
                using (LinearGradientBrush gradientBrush = GetFlatGradientBackBrush(captionRectangle, colorFlatGradientBegin, colorFlatGradientEnd, bHover))
                {
                    graphics.FillRectangle(gradientBrush, captionRectangle);
                }
                if (bShowBorder == true)
                {
                    Rectangle innerRectangle = captionRectangle;
                    innerRectangle.X -= 1;
                    innerRectangle.Height -= 1;
                    innerRectangle.Offset(2, 2);

                    DrawBorder(graphics, true, colorInnerBorder, innerRectangle);
                }
            }

            DrawImagesAndText(
                graphics,
                captionRectangle,
                CaptionSpacing,
                this.ImageRectangle,
                this.Image,
                this.RightToLeft,
                false,
                this.IsClosable,
                this.ShowCloseIcon,
                this.m_imageClosePanel,
                foreColorCloseIcon,
                ref this.RectangleClosePanelIcon,
                this.ShowExpandIcon,
                this.m_imageChevron,
                foreColorExpandIcon,
                ref this.RectangleExandPanelIcon,
                this.CaptionFont,
                colorText,
                this.Text);

            if (bShowBorder == true)
            {
                using (SolidBrush borderBrush = new SolidBrush(this.PanelColors.BorderColor))
                {
                    using (Pen borderPen = new Pen(borderBrush, 1))
                    {
                        if (this.Top == 0)
                        {
                            graphics.DrawLine(
                                borderPen,
                                this.ClientRectangle.Left,
                                this.Top,
                                this.ClientRectangle.Width,
                                this.Top);
                        }
                        if (bExpand == true)
                        {
                            graphics.DrawLine(
                                borderPen,
                                this.ClientRectangle.Left,
                                this.CaptionHeight,
                                this.ClientRectangle.Width,
                                this.CaptionHeight);
                        }
                    }
                }
            }
		}

        private void DrawStyleAqua(Graphics graphics, bool bExpand)
        {
            Rectangle captionRectangle = this.CaptionRectangle;
            Color colorGradientBegin = this.PanelColors.XPanderPanelCaptionGradientBegin;
            Color colorGradientEnd = this.PanelColors.XPanderPanelCaptionGradientEnd;
            Color colorText = this.PanelColors.XPanderPanelCaptionText;
            Color colorPressedGradientBegin = this.PanelColors.XPanderPanelPressedCaptionMiddle;
            Color colorPressedGradientEnd = this.PanelColors.XPanderPanelPressedCaptionMiddle;
            Color foreColorCloseIcon = this.PanelColors.XPanderPanelCaptionCloseIcon;
            Color foreColorExpandIcon = this.PanelColors.XPanderPanelCaptionExpandIcon;

            if (this.m_imageClosePanel == null)
            {
                this.m_imageClosePanel = Resources.closePanel;
            }
            if (this.m_imageChevronUp == null)
            {
                this.m_imageChevronUp = Resources.ChevronUp;
            }
            if (this.m_imageChevronDown == null)
            {
                this.m_imageChevronDown = Resources.ChevronDown;
            }
            
            this.m_imageChevron = this.m_imageChevronDown;
            // defines the chevron direction
            if (bExpand == true)
            {
                this.m_imageChevron = this.m_imageChevronUp;
            }

            if ((this.State == CaptionState.Hover) && (bExpand == false))
            {
                colorGradientBegin = colorPressedGradientBegin;
                colorGradientEnd = colorPressedGradientEnd;
                colorText = this.PanelColors.XPanderPanelSelectedCaptionText;
                foreColorCloseIcon = colorText;
                foreColorExpandIcon = colorText;
            }

            using (GraphicsPath outerRectangleGraphicsPath = GetBackgroundPath(captionRectangle, 20))
            {
                using (LinearGradientBrush outerLinearGradientBrush = new LinearGradientBrush(
                    captionRectangle,
                    colorGradientBegin,
                    colorGradientEnd,
                    LinearGradientMode.Vertical))
                {
                    graphics.FillPath(outerLinearGradientBrush, outerRectangleGraphicsPath); //draw top bubble
                }
            }
            //
            // Create top water color to give "aqua" effect
            // 
            Rectangle innerRectangle = captionRectangle;
            innerRectangle.Height = 14;

			using (GraphicsPath innerRectangleGraphicsPath = GetPath(innerRectangle, 20))
			{
                using (LinearGradientBrush innerRectangleBrush = new LinearGradientBrush(
                    innerRectangle,
                    Color.FromArgb(255, Color.White),
                    Color.FromArgb(32, Color.White),
                    LinearGradientMode.Vertical))
				{
					//
					// draw shapes
					//
                    graphics.FillPath(innerRectangleBrush, innerRectangleGraphicsPath); //draw top bubble
				}
			}

            DrawImagesAndText(
                graphics,
                captionRectangle,
                CaptionSpacing,
                this.ImageRectangle,
                this.Image,
                this.RightToLeft,
                true,
                this.IsClosable,
                this.ShowCloseIcon,
                this.m_imageClosePanel,
                foreColorCloseIcon,
                ref this.RectangleClosePanelIcon,
                this.ShowExpandIcon,
                this.m_imageChevron,
                foreColorExpandIcon,
                ref this.RectangleExandPanelIcon,
                this.CaptionFont,
                colorText,
                this.Text);
        }

        private void DrawOffice2007(Graphics graphics, bool bExpand, bool bShowBorder)
        {
            Rectangle captionRectangle = this.CaptionRectangle;
            Color colorGradientEnd = this.PanelColors.XPanderPanelCaptionGradientEnd;
            Color colorPressedGradientEnd = this.PanelColors.XPanderPanelPressedCaptionMiddle;
            Color colorText = this.PanelColors.XPanderPanelCaptionText;
            Color foreColorCloseIcon = this.PanelColors.XPanderPanelCaptionCloseIcon;
            Color foreColorExpandIcon = this.PanelColors.XPanderPanelCaptionExpandIcon;

            if (this.m_imageClosePanel == null)
            {
                this.m_imageClosePanel = Resources.closePanel;
            }
            if (this.m_imageChevronUp == null)
            {
                this.m_imageChevronUp = Resources.ChevronUp;
            }
            if (this.m_imageChevronDown == null)
            {
                this.m_imageChevronDown = Resources.ChevronDown;
            }
            
            this.m_imageChevron = this.m_imageChevronDown;
            if (bExpand == true)
            {
                this.m_imageChevron = this.m_imageChevronUp;
                colorGradientEnd = this.PanelColors.XPanderPanelSelectedCaptionMiddle;
                colorText = this.PanelColors.XPanderPanelSelectedCaptionText;
                foreColorCloseIcon = colorText;
                foreColorExpandIcon = colorText;
            }

            if (this.m_captionStyle == CaptionStyle.Normal)
            {
                if ((this.State == CaptionState.Hover) && (bExpand == false))
                {
                    colorGradientEnd = colorPressedGradientEnd;
                    colorText = this.PanelColors.XPanderPanelSelectedCaptionText;
                    foreColorCloseIcon = colorText;
                    foreColorExpandIcon = colorText;
                }
                using (LinearGradientBrush gradientBrush = GetGradientBackBrush(captionRectangle, colorGradientEnd))
                {
                    graphics.FillRectangle(gradientBrush, captionRectangle);
                }
            }
            else
            {
                bool bHover = false;

                Color colorFlatGradientBegin = this.PanelColors.XPanderPanelFlatCaptionGradientBegin;
                Color colorFlatGradientEnd = this.PanelColors.XPanderPanelFlatCaptionGradientEnd;
                Color colorInnerBorder = this.PanelColors.InnerBorderColor;

                if (bExpand == true)
                {
                    colorText = this.PanelColors.XPanderPanelCaptionText;
                }
                
                if (this.State == CaptionState.Hover)
                {
                    colorText = this.PanelColors.XPanderPanelCaptionText;
                    bHover = true;
                }
                using (LinearGradientBrush gradientBrush = GetFlatGradientBackBrush(captionRectangle, colorFlatGradientBegin, colorFlatGradientEnd, bHover))
                {
                    graphics.FillRectangle(gradientBrush, captionRectangle);
                }
                if (bShowBorder == true)
                {
                    Rectangle innerRectangle = captionRectangle;
                    innerRectangle.X -= 1;
                    innerRectangle.Height -= 1;
                    innerRectangle.Offset(2, 2);

                    DrawBorder(graphics, true, colorInnerBorder, innerRectangle);
                }
            }

            DrawImagesAndText(
                graphics,
                captionRectangle,
                CaptionSpacing,
                this.ImageRectangle,
                this.Image,
                this.RightToLeft,
                false,
                this.m_bIsClosable,
                this.ShowCloseIcon,
                this.m_imageClosePanel,
                foreColorCloseIcon,
                ref this.RectangleClosePanelIcon,
                this.ShowExpandIcon,
                this.m_imageChevron,
                foreColorExpandIcon,
                ref this.RectangleExandPanelIcon,
                this.CaptionFont,
                colorText,
                this.Text);

            if (bShowBorder == true)
            {
                using (SolidBrush borderBrush = new SolidBrush(this.PanelColors.BorderColor))
                {
                    using (Pen borderPen = new Pen(borderBrush, 1))
                    {
                        if (this.Top == 0)
                        {
                            graphics.DrawLine(
                                borderPen,
                                this.ClientRectangle.Left,
                                this.Top,
                                this.ClientRectangle.Width,
                                this.Top);
                        }
                        if (bExpand == true)
                        {
                            graphics.DrawLine(
                                borderPen,
                                this.ClientRectangle.Left,
                                this.CaptionHeight,
                                this.ClientRectangle.Width,
                                this.CaptionHeight);
                        }
                    }
                }
            }
        }

        private static void DrawBorder(Graphics graphics, bool bShowBorder, Color borderColor, Rectangle borderRectangle)
		{
            if (bShowBorder == true)
            {
                borderRectangle.Inflate(0, 1);
                borderRectangle.Offset(0, -1);

                ControlPaint.DrawBorder(
                    graphics,
                    borderRectangle,
                    borderColor,
                    ButtonBorderStyle.Solid);
			}
		}

		private void CalculatePanelHeights()
		{
			if (this.Parent == null)
			{
				return;
			}

            int iPanelHeight = this.Parent.Padding.Top;

            foreach (Control control in this.Parent.Controls)
            {
				BSE.Windows.Forms.XPanderPanel xpanderPanel =
					control as BSE.Windows.Forms.XPanderPanel;

                if ((xpanderPanel != null) && (xpanderPanel.Visible == true))
                {
                    iPanelHeight += xpanderPanel.CaptionHeight;
                }
            }

			iPanelHeight += this.Parent.Padding.Bottom;

            foreach (Control control in this.Parent.Controls)
			{
				BSE.Windows.Forms.XPanderPanel xpanderPanel =
					control as BSE.Windows.Forms.XPanderPanel;

                if (xpanderPanel != null)
                {
                    if (xpanderPanel.Expand == true)
                    {
                        xpanderPanel.Height = this.Parent.Height
                            + xpanderPanel.CaptionHeight
                            - iPanelHeight;
                    }
                    else
                    {
                        xpanderPanel.Height = xpanderPanel.CaptionHeight;
                    }
                }
			}

			int iTop = this.Parent.Padding.Top;
			foreach (Control control in this.Parent.Controls)
			{
				BSE.Windows.Forms.XPanderPanel xpanderPanel =
					control as BSE.Windows.Forms.XPanderPanel;

                if ((xpanderPanel != null) && (xpanderPanel.Visible == true))
                {
                    xpanderPanel.Top = iTop;
                    iTop += xpanderPanel.Height;
                }
			}
		}

		#endregion
    }

    #endregion

    #region Class XPanderPanelDesigner
    /// <summary>
    /// Designer class for extending the design mode behavior of a XPanderPanel control
    /// </summary>
	internal class XPanderPanelDesigner : System.Windows.Forms.Design.ScrollableControlDesigner
	{
		#region FieldsPrivate

		private Pen m_borderPen = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
        private System.Windows.Forms.Design.Behavior.Adorner m_adorner;

		#endregion

		#region MethodsPublic
		/// <summary>
        /// Initializes a new instance of the XPanderPanelDesigner class.
		/// </summary>
		public XPanderPanelDesigner()
		{
			this.m_borderPen.DashStyle = DashStyle.Dash;
		}
		/// <summary>
		/// Initializes the designer with the specified component.
		/// </summary>
		/// <param name="component">The IComponent to associate with the designer.</param>
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
            XPanderPanel xpanderPanel = Control as XPanderPanel;
            if (xpanderPanel != null)
            {
                this.m_adorner = new System.Windows.Forms.Design.Behavior.Adorner();
                BehaviorService.Adorners.Add(this.m_adorner);
                this.m_adorner.Glyphs.Add(new XPanderPanelCaptionGlyph(BehaviorService, xpanderPanel));
            }
		}
		#endregion

		#region MethodsProtected
        /// <summary>
        /// Releases the unmanaged resources used by the XPanderPanelDesigner,
        /// and optionally releases the managed resources. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
			try
			{
				if (disposing)
				{
					if (this.m_borderPen != null)
					{
						this.m_borderPen.Dispose();
					}
					if (this.m_adorner != null)
					{
						if (BehaviorService != null)
						{
							BehaviorService.Adorners.Remove(this.m_adorner);
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
        }
		/// <summary>
		/// Receives a call when the control that the designer is managing has painted its surface so the designer can
		/// paint any additional adornments on top of the xpanderpanel.
		/// </summary>
		/// <param name="e">A PaintEventArgs the designer can use to draw on the xpanderpanel.</param>
		protected override void OnPaintAdornments(PaintEventArgs e)
		{
			base.OnPaintAdornments(e);
			e.Graphics.DrawRectangle(
				this.m_borderPen,
				0,
				0,
				this.Control.Width - 2,
				this.Control.Height - 2);
		}
		/// <summary>
		/// Allows a designer to change or remove items from the set of properties that it exposes through a <see cref="TypeDescriptor">TypeDescriptor</see>. 
		/// </summary>
		/// <param name="properties">The properties for the class of the component.</param>
		protected override void PostFilterProperties(IDictionary properties)
		{
			base.PostFilterProperties(properties);
			properties.Remove("AccessibilityObject");
			properties.Remove("AccessibleDefaultActionDescription");
			properties.Remove("AccessibleDescription");
			properties.Remove("AccessibleName");
			properties.Remove("AccessibleRole");
			properties.Remove("AllowDrop");
            properties.Remove("Anchor");
            properties.Remove("AntiAliasing");
			properties.Remove("AutoScroll");
			properties.Remove("AutoScrollMargin");
			properties.Remove("AutoScrollMinSize");
			properties.Remove("BackgroundImage");
			properties.Remove("BackgroundImageLayout");
			properties.Remove("CausesValidation");
			properties.Remove("ContextMenuStrip");
			properties.Remove("Dock");
			properties.Remove("GenerateMember");
			properties.Remove("ImeMode");
            properties.Remove("Location");
			properties.Remove("MaximumSize");
			properties.Remove("MinimumSize");
		}

		#endregion
    }
    #endregion

    #region Class XPanderPanelCaptionGlyph
    /// <summary>
    /// Represents a single user interface (UI) entity managed by an Adorner.
    /// </summary>
    internal class XPanderPanelCaptionGlyph : System.Windows.Forms.Design.Behavior.Glyph
    {
        #region FieldsPrivate

        private XPanderPanel m_xpanderPanel;
        private System.Windows.Forms.Design.Behavior.BehaviorService m_behaviorService;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the bounds of the Glyph.
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                Point edge = this.m_behaviorService.ControlToAdornerWindow(this.m_xpanderPanel);
                Rectangle bounds = new Rectangle(
                    edge.X,
                    edge.Y,
                    this.m_xpanderPanel.Width,
                    this.m_xpanderPanel.CaptionHeight);

                return bounds;
            }
        }
        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initializes a new instance of the CaptionGlyph class.
        /// </summary>
        /// <param name="behaviorService"></param>
        /// <param name="xpanderPanel"></param>
        public XPanderPanelCaptionGlyph(System.Windows.Forms.Design.Behavior.BehaviorService behaviorService, XPanderPanel xpanderPanel)
            :
            base(new XPanderPanelCaptionClickBehavior(xpanderPanel))
        {
            this.m_behaviorService = behaviorService;
            this.m_xpanderPanel = xpanderPanel;
        }
        /// <summary>
        /// Provides hit test logic.
        /// </summary>
        /// <param name="p">A point to hit-test.</param>
        /// <returns>A Cursor if the Glyph is associated with p; otherwise, a null reference</returns>
        public override Cursor GetHitTest(Point p)
        {
            // GetHitTest is called to see if the point is
            // within this glyph.  This gives us a chance to decide
            // what cursor to show.  Returning null from here means
            // the mouse pointer is not currently inside of the glyph.
            // Returning a valid cursor here indicates the pointer is
            // inside the glyph, and also enables our Behavior property
            // as the active behavior.
            if ((this.m_xpanderPanel != null) && (this.m_xpanderPanel.Expand == false) && (Bounds.Contains(p)))
            {
                return Cursors.Hand;
            }

            return null;
        }
        /// <summary>
        /// Provides paint logic.
        /// </summary>
        /// <param name="pe">A PaintEventArgs that contains the event data.</param>
        public override void Paint(PaintEventArgs pe)
        {
        }

        #endregion
    }

    #endregion

    #region Class XPanderPanelCaptionClickBehavior
    /// <summary>
    /// Designer behaviour when the user clicks in the glyph on the XPanderPanel caption
    /// </summary>
    internal class XPanderPanelCaptionClickBehavior : System.Windows.Forms.Design.Behavior.Behavior
    {
        #region FieldsPrivate
        private XPanderPanel m_xpanderPanel;
        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initializes a new instance of the Behavior class.
        /// </summary>
        /// <param name="xpanderPanel">XPanderPanel for this behaviour</param>
        public XPanderPanelCaptionClickBehavior(XPanderPanel xpanderPanel)
        {
            this.m_xpanderPanel = xpanderPanel;
        }
        /// <summary>
        /// Called when any mouse-down message enters the adorner window of the BehaviorService. 
        /// </summary>
        /// <param name="g">A Glyph.</param>
        /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
        /// <param name="mouseLoc">The location at which the click occurred.</param>
        /// <returns>true if the message was handled; otherwise, false. </returns>
		public override bool OnMouseDown(System.Windows.Forms.Design.Behavior.Glyph g, MouseButtons button, Point mouseLoc)
		{
			if ((this.m_xpanderPanel != null) && (this.m_xpanderPanel.Expand == false))
			{
				XPanderPanelList xpanderPanelList = this.m_xpanderPanel.Parent as XPanderPanelList;
				if (xpanderPanelList != null)
				{
					xpanderPanelList.Expand(this.m_xpanderPanel);
					this.m_xpanderPanel.Invalidate(false);
				}
			}
			return true; // indicating we processed this event.
		}
        #endregion
    }

    #endregion
}
