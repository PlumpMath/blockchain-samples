// // // <copyright file="Extensions.cs" company="OSIsoft, LLC">
// // //   Copyright (c) 2017 OSIsoft, LLC.  All rights reserved.
// // //   
// // //   THIS SOFTWARE CONTAINS CONFIDENTIAL INFORMATION AND TRADE SECRETS OF
// // //   OSIsoft, LLC.  USE, DISCLOSURE, OR REPRODUCTION IS PROHIBITED WITHOUT
// // //   THE PRIOR EXPRESS WRITTEN PERMISSION OF OSIsoft, LLC.
// // //   
// // //   RESTRICTED RIGHTS LEGEND
// // //   Use, duplication, or disclosure by the Government is subject to restrictions
// // //   as set forth in subparagraph (c)(1)(ii) of the Rights in Technical Data and
// // //   Computer Software clause at DFARS 252.227.7013
// // //   
// // //   OSIsoft, LLC
// // //   1600 Alvarado Street. San Leandro, CA  94577 USA
// // // </copyright>

using System.Linq;

namespace OSIsoft.Blockchain
{
    /// <summary>
    ///     Extension helper methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     Helper method to print out a hash to a readable string format (hex)
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static string HashToString(this byte[] hash)
        {
            return string.Join("", hash.Select(b => b.ToString("x2")));
        }
    }
}