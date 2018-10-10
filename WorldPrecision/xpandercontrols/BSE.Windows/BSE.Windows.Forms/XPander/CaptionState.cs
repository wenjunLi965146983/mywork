using System;
using System.Collections.Generic;
using System.Text;

namespace BSE.Windows.Forms
{
	/// <summary>
	/// Specifies constants that define the state of the caption of a Panel or XPanderPanel.
	/// </summary>
    /// <remarks>
    /// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
    /// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
    /// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
    /// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
    /// REMAINS UNCHANGED.
    /// </remarks>
    /// <copyright>Copyright © 2008 Uwe Eichkorn</copyright>
    public enum CaptionState
	{
		/// <summary>
		/// The state of a caption in its normal state (none of the other states apply).
		/// </summary>
		Normal,
		/// <summary>
		/// The state of a caption over which a mouse pointer is resting.
		/// </summary>
		Hover
	}
}
