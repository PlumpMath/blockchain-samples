// // // <copyright file="Program.cs" company="OSIsoft, LLC">
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
using System.Text;

namespace OSIsoft.Blockchain
{
    /// <summary>
    ///     Very simple demonstration of a simplified blockchain data structure. Demonstrates the fact that the whole
    ///     blockchain becomes invalid when a single block becomes invalid when it is mutated and not remined.
    ///     Demonstrates a very simple Proof Of Work algorithm.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     The miner being used to mine our blocks
        /// </summary>
        private static Miner miner;

        /// <summary>
        ///     The blockchain itself. Basically an ordered list of linked blocks
        /// </summary>
        private static BlockChain blockChain;


        /// <summary>
        ///     Main application entry point
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            blockChain = new BlockChain();
            miner = new Miner();

            //First we have to create a genesis block. This is the only block without an ancestor.
            //We can pass in the difficulty of the genesis block and all subsequent blocks
            //Try setting the difficulty level higher to make mutations to the chain more difficult to recompute (more tamper proof)
            //In the real world, and with a large chain and a high difficulty this can take many hundreds of thousands of computing hours, making it very expensive to tamper with blocks.
            var genesisBlock = new GenesisBlock(2);
            //Mine the genesis block. This is not strictly necessary as the right nonce could already be hardcoded.
            miner.Mine(genesisBlock);
            //Add the genesis block to the blockchain as the first block
            blockChain.Add(genesisBlock);

            //Create a couple of blocks and add them to the blockchain
            for (var i = 0; i < 10; i++)
                AddBlock("Hello OSIsoft!. I am block #" + i, blockChain.LastBlock);

            //Write out the whole (valid) blockchain
            Console.WriteLine("Valid chain");
            Console.WriteLine(blockChain);

            //Wait for the user to examine the valid blockchain
            Console.WriteLine("Press enter to tamper with the chain");
            Console.ReadLine();

            //Tamper with the data of a specific block. This should invalidate the hash and cause all subsequent blocks to have invalid hashes as well.
            blockChain[3].Data = Encoding.UTF8.GetBytes("I have been tampered with");

            //Write out the whole (now invalid) block. All blocks starting with index 3 should now be invalid, even though their data has not changed (but their hash has, because the previous block's hash changed).
            Console.WriteLine("Invalid chain");
            Console.WriteLine(blockChain);

            //Wait for the user to examine the invalid chain
            Console.WriteLine("Press enter to re-mine the blocks");
            Console.ReadLine();

            for (int i = 3; i < blockChain.Count; i++)
            {
                miner.Mine(blockChain[i]);
            }

            //Print out the re-mined (now valid again) blockchain
            Console.WriteLine(blockChain);
            
            //Wait for the user to examine the now valid blockchain
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

        /// <summary>
        ///     Helper method to add a block to the blockchain
        /// </summary>
        /// <param name="text">The text to appear in the 'data' section of the block</param>
        /// <param name="previousBlock">The </param>
        private static void AddBlock(string text, Block previousBlock)
        {
            //Encode the string
            var data = Encoding.UTF8.GetBytes(text);

            //Mine the new block
            var block = miner.Mine(data, previousBlock);

            //Add the block to the blockchain
            blockChain.Add(block);
        }
    }
}