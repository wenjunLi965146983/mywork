using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BSE.Windows.Forms
{
    /// <summary>
    /// Provide Office 2007 Black theme colors
    /// </summary>
    /// <remarks>
    /// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
    /// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
    /// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
    /// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
    /// REMAINS UNCHANGED.
    /// </remarks>
    /// <copyright>Copyright © 2006-2008 Uwe Eichkorn</copyright>
    public class PanelColorsOffice2007Black : PanelColors
    {
		#region FieldsPrivate
		#endregion

		#region Properties
        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initialize a new instance of the PanelColorsOffice2007Black class.
        /// </summary>
        public PanelColorsOffice2007Black()
			: base()
		{
		}
        /// <summary>
        /// Initialize a new instance of the PanelColorsOffice2007Black class.
        /// </summary>
        /// <param name="basePanel">Base class for the panel or xpanderpanel control.</param>
        public PanelColorsOffice2007Black(BasePanel basePanel)
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
            rgbTable[KnownColors.BorderColor] = Color.FromArgb(76, 83, 92);
            rgbTable[KnownColors.InnerBorderColor] = Color.White;
            rgbTable[KnownColors.PanelCaptionCloseIcon] = Color.FromArgb(255, 255, 255);
            rgbTable[KnownColors.PanelCaptionExpandIcon] = Color.FromArgb(101, 104, 112);
            rgbTable[KnownColors.PanelCaptionGradientBegin] = Color.FromArgb(240, 241, 242);
            rgbTable[KnownColors.PanelCaptionGradientEnd] = Color.FromArgb(189, 193, 200);
            rgbTable[KnownColors.PanelCaptionGradientMiddle] = Color.FromArgb(216, 219, 223);
            rgbTable[KnownColors.PanelContentGradientBegin] = Color.FromArgb(240, 241, 242);
            rgbTable[KnownColors.PanelContentGradientEnd] = Color.FromArgb(240, 241, 242);
            rgbTable[KnownColors.PanelCaptionText] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.PanelCollapsedCaptionText] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.XPanderPanelBackColor] = Color.Transparent;
            rgbTable[KnownColors.XPanderPanelCaptionCloseIcon] = Color.FromArgb(255, 255, 255);
            rgbTable[KnownColors.XPanderPanelCaptionExpandIcon] = Color.FromArgb(101, 104, 112);
            rgbTable[KnownColors.XPanderPanelCaptionText] = Color.FromArgb(55, 60, 67);
            rgbTable[KnownColors.XPanderPanelCaptionGradientBegin] = Color.FromArgb(248, 248, 249);
            rgbTable[KnownColors.XPanderPanelCaptionGradientEnd] = Color.FromArgb(199, 203, 209);
            rgbTable[KnownColors.XPanderPanelCaptionGradientMiddle] = Color.FromArgb(219, 222, 226);
            rgbTable[KnownColors.XPanderPanelFlatCaptionGradientBegin] = Color.FromArgb(212, 215, 219);
            rgbTable[KnownColors.XPanderPanelFlatCaptionGradientEnd] = Color.FromArgb(253, 253, 254);
            rgbTable[KnownColors.XPanderPanelPressedCaptionBegin] = Color.FromArgb(255, 252, 222);
            rgbTable[KnownColors.XPanderPanelPressedCaptionEnd] = Color.FromArgb(255, 230, 158);
            rgbTable[KnownColors.XPanderPanelPressedCaptionMiddle] = Color.FromArgb(255, 215, 103);
            rgbTable[KnownColors.XPanderPanelSelectedCaptionBegin] = Color.FromArgb(255, 217, 170);
            rgbTable[KnownColors.XPanderPanelSelectedCaptionEnd] = Color.FromArgb(254, 225, 122);
            rgbTable[KnownColors.XPanderPanelSelectedCaptionMiddle] = Color.FromArgb(255, 171, 63);
            rgbTable[KnownColors.XPanderPanelSelectedCaptionText] = Color.FromArgb(0, 0, 0);
        }

        #endregion

        #region MethodsPrivate
        #endregion
    }
}
