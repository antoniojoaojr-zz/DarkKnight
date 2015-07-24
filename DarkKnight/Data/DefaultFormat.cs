using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkKnight.Data
{
    /// <summary>
    /// Formats that are used internally by DarkKnight
    /// </summary>
    public enum DefaultFormat
    {
        /// <summary>
        /// Define no format packet
        /// </summary>
        NoFormatSelected = 0x0fff,

        /// <summary>
        /// Defines when a package is invalid
        /// </summary>
        InvalidPackage = 0x1fff,

        /// <summary>
        /// New radius information stream
        /// </summary>
        JRadiusStream = 0x2fff,

        /// <summary>
        /// New view information stream
        /// </summary>
        JViewStream = 0x3fff,

        /// <summary>
        /// New target information stream
        /// </summary>
        JTargetStream = 0x4fff,

        /// <summary>
        /// Exit target information
        /// </summary>
        JExitTargetStream = 0x5fff
    }
}
