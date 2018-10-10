using System;

namespace BSE.Windows.Forms
{
    /// <summary>
    /// Contains information about the style of the panels or xpanderpanels
    /// </summary>
    /// <remarks>
    /// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
    /// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
    /// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
    /// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
    /// REMAINS UNCHANGED.
    /// </remarks>
    /// <copyright>Copyright © 2007-2008 Uwe Eichkorn</copyright>
    public enum PanelStyle
	{
        /// <summary>
        /// Draws the panels caption in the default office2003 style.
        /// </summary>
        Default,
        /// <summary>
        /// Draws the caption of a panel in the aqua style.
        /// </summary>
        Aqua,
        /// <summary>
        /// Draws the panels caption in the office 2007 style.
        /// </summary>
        Office2007,
        /// <summary>
        /// Hides the caption (only for using in the panel control).
        /// </summary>
        None
	}
}
