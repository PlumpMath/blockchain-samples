// // // <copyright file="BlockChain.cs" company="OSIsoft, LLC">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSIsoft.Blockchain
{
    /// <summary>
    ///     Ordered list of blocks.
    /// </summary>
    public class BlockChain : List<Block>
    {
        /// <summary>
        ///     Retrieve the first (genesis) block in the chain
        /// </summary>
        public Block GenesisBlock => this.First();

        /// <summary>
        ///     Retrieve the last block
        /// </summary>
        public Block LastBlock => this.Last();

        /// <summary>
        ///     Checks if the whole chain is valid
        /// </summary>
        public bool IsValid => this.LastBlock.IsValid();

        /// <summary>
        ///     Prints out the status of all blocks in the chain
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(new string('-', Console.WindowWidth - 1));
            foreach (var kv in this)
                builder.AppendLine(kv.ToString());
            builder.AppendLine(new string('-', Console.WindowWidth - 1));
            builder.AppendLine($"Is Valid: {this.IsValid}");


            return builder.ToString();
        }
    }
}