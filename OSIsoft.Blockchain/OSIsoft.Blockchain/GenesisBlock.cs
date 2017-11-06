// // // <copyright file="GenesisBlock.cs" company="OSIsoft, LLC">
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

namespace OSIsoft.Blockchain
{
    /// <summary>
    ///     Helper class for a genesis block. Sets the blocknumber to 0 and will set the desired difficulty level for the
    ///     descendant blocks.
    /// </summary>
    public class GenesisBlock : Block
    {
        public GenesisBlock(int difficulty, byte[] data = null)
            : base(null)
        {
            this.BlockNumber = 0;
            this.Nonce = 0;
            this.Data = data ?? new byte[0];
            this.Difficulty = difficulty;
        }
    }
}