using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BSE.Windows.Forms
{
    /// <summary>
    /// Provide Office 2007 silver theme colors
    /// </summary>
    /// <remarks>
    /// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
    /// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
    /// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
    /// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
    /// REMAINS UNCHANGED.
    /// </remarks>
    /// <copyright>Copyright © 2006-2008 Uwe Eichkorn</copyright>
    public class PanelColorsOffice2007Silver : PanelColors
    {
		#region FieldsPrivate
		#endregion

		#region Properties
        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initialize a new instance of the PanelColorsOffice2007Silver class.
        /// </summary>
        public PanelColorsOffice2007Silver()
			: base()
		{
		}
        /// <summary>
        /// Initialize a new instance of the PanelColorsOffice2007Silver class.
        /// </summary>
        /// <param name="basePanel">Base class for the panel or xpanderpanel control.</param>
        public PanelColorsOffice2007Silver(BasePanel basePanel)
            : base(basePanel)
        {
        }

        #endregion

        #region MethodsProtected
        /// <summary>
        /// Initialize a color Dictionary with defined colors
        /// </summary>
        /// <param name="rgbTable">Dictionary with defined colors</param>
        protected override void InitColors(ref Dictionary<PanelColors.KnownColors, Color> rgbTable)
        {
            rgbTable[KnownColors.BorderColor] = Color.FromArgb(111, 112, 116);
            rgbTable[KnownColors.InnerBorderColor] = Color.White;
            rgbTable[KnownColors.PanelCaptionCloseIcon] = Color.FromArgb(75, 79, 85);
            rgbTable[KnownColors.PanelCaptionExpandIcon] = Color.FromArgb(101, 104, 112);
            rgbTable[KnownColors.PanelCaptionGradientBegin] = Color.FromArgb(248, 248, 248);
            rgbTable[KnownColors.PanelCaptionGradientEnd] = Color.FromArgb(199, 203, 209);
            rgbTable[KnownColors.PanelCaptionGradientMiddle] = Color.FromArgb(218, 219, 231);
            rgbTable[KnownColors.PanelContentGradientBegin] = Color.FromArgb(240, 241, 242);
            rgbTable[KnownColors.PanelContentGradientEnd] = Color.FromArgb(240, 241, 242);
            rgbTable[KnownColors.PanelCaptionText] = Color.FromArgb(21, 66, 139);
            rgbTable[KnownColors.PanelCollapsedCaptionText] = Color.FromArgb(21, 66, 139);
            rgbTable[KnownColors.XPanderPanelBackColor] = Color.Transparent;
            rgbTable[KnownColors.XPanderPanelCaptionCloseIcon] = Color.FromArgb(75, 79, 85);
            rgbTable[KnownColors.XPanderPanelCaptionExpandIcon] = Color.FromArgb(101, 104, 112);
            rgbTable[KnownColors.XPanderPanelCaptionText] = Color.FromArgb(76, 83, 92);
            rgbTable[KnownColors.XPanderPanelCaptionGradientBegin] = Color.FromArgb(248, 248, 248);
            rgbTable[KnownColors.XPanderPanelCaptionGradientEnd] = Color.FromArgb(199, 203, 209);
            rgbTable[KnownColors.XPanderPanelCaptionGradientMiddle] = Color.FromArgb(218, 219, 231);
            rgbTable[KnownColors.XPanderPanelFlatCaptionGradientBegin] = Color.FromArgb(213, 219, 231);
            rgbTable[KnownColors.XPanderPanelFlatCaptionGradientEnd] = Color.FromArgb(253, 253, 254);
            rgbTable[KnownColors.XPanderPanelPressedCaptionBegin] = Color.FromArgb(255, 252, 222);
            rgbTable[KnownColors.XPanderPanelPressedCaptionEnd] = Color.FromArgb(255, 230, 158);
            rgbTable[KnownColors.XPanderPanelPressedCaptionMiddle] = Color.FromArgb(255, 215, 103);
            rgbTable[KnownColors.XPanderPanelSelectedCaptionBegin] = Color.FromArgb(255, 217, 170);
            rgbTable[KnownColors.XPanderPanelSelectedCaptionEnd] = Color.FromArgb(254, 225, 122);
            rgbTable[KnownColors.XPanderPanelSelectedCaptionMiddle] = Color.FromArgb(255, 171, 63);
            rgbTable[KnownColors.XPanderPanelSelectedCaptionText] = Color.Black;
        }

        #endregion

        #region MethodsPrivate
        #endregion
    }
}
