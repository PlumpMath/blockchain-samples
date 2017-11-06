// // // <copyright file="Miner.cs" company="OSIsoft, LLC">
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
    ///     Simplified miner.
    /// </summary>
    public class Miner
    {
        /// <summary>
        ///     Will create a new block using the specified data and the specified ancestor block. Will mine the block.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lastBlock"></param>
        /// <returns></returns>
        public Block Mine(byte[] data, Block lastBlock)
        {
            var block = new Block(lastBlock)
            {
                BlockNumber = lastBlock.BlockNumber + 1,
                Data = data,
                Difficulty = lastBlock.Difficulty
            };

            this.Mine(block);
            return block;
        }

        /// <summary>
        ///     Mine a block. Will increment the nonce of the block until the block is valid (the desired difficulty level has been
        ///     reached)
        /// </summary>
        /// <param name="block"></param>
        public void Mine(Block block)
        {
            do
            {
                block.Nonce++;
            } while (!block.IsValid());
        }
    }
}