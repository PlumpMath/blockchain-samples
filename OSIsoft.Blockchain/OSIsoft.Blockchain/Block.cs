// // // <copyright file="Block.cs" company="OSIsoft, LLC">
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
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OSIsoft.Blockchain
{
    /// <summary>
    ///     Represents a block in the blockchain. Contains data, a reference to the previous block and can compute the hash for
    ///     this block
    /// </summary>
    public class Block
    {
        /// <summary>
        ///     Used to create SHA256 hashes
        /// </summary>
        private static readonly SHA256Managed sha256 = new SHA256Managed();

        /// <summary>
        ///     Constructor. Can only create a new block with an ancestor (except for the genesis block)
        /// </summary>
        /// <param name="previousBlock"></param>
        public Block(Block previousBlock)
        {
            this.PreviousBlock = previousBlock;
        }

        /// <summary>
        ///     Reference to the previous (ancestor) block
        /// </summary>
        public Block PreviousBlock { get; }

        /// <summary>
        ///     The sequence of this block in the chain
        /// </summary>
        public int BlockNumber { get; set; }

        /// <summary>
        ///     The nonce. A value that can be changed by the miner to get a sha256 hash that reprents the difficulty level for
        ///     this block
        /// </summary>
        public int Nonce { get; set; }

        /// <summary>
        ///     The difficulty level for this block. The sha256 hash for this block has to lead with n 0 bytes
        ///     (note, when converted to string is represented by 00 (1 byte = max 265 values (0 to 255) = 00 to FF), so a hash
        ///     with difficulty = 1 will look like 00........ and with difficulty level 2 will look like 0000....
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        ///     The data for this block
        /// </summary>
        public byte[] Data { get; set; } = {0x00};

        /// <summary>
        ///     A reference to the sha256 hash of the previous block. The genesis block does not have a previous block so the hash
        ///     = 0x0
        /// </summary>
        public byte[] PreviousBlockHash => this.PreviousBlock?.Hash ?? new byte[0];

        /// <summary>
        ///     The sha256 hash for this block
        /// </summary>
        public byte[] Hash => this.ComputeHash();

        /// <summary>
        ///     Print out the contents and information for this block
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine($"Block #: \t {this.BlockNumber}");
            builder.AppendLine($"Nonce: \t\t {this.Nonce}");
            builder.AppendLine($"Data: \t\t {Encoding.UTF8.GetString(this.Data)}");
            builder.AppendLine(
                $"Prev hash: \t {(this.PreviousBlockHash != null ? this.PreviousBlockHash.HashToString() : string.Empty)}");
            builder.AppendLine($"Hash: \t\t {this.Hash.HashToString()}");
            builder.AppendLine($"Is valid: \t {this.IsValid()}");
            builder.AppendLine();

            return builder.ToString();
        }


        /// <summary>
        ///     Gets the 'internal contents' of this block that is used to compute the hash.
        /// </summary>
        /// <returns>byte array containing the previous block hash + the nonce + the data</returns>
        public byte[] GetInternalContents()
        {
            //The contents are [ previousblockhash (byte[32] + nonce (int = byte[4]) + data (byte[n]) ], [ 32 bytes + 4 bytes + data.length]
            //Create the new byte array with the specified length
            var contents = new byte[32 + 4 + this.Data.Length];
            //Copy the hash of the previous block in the new array
            this.PreviousBlockHash.CopyTo(contents, 0);
            //Convert the nonce to a byte array
            var nonce = BitConverter.GetBytes(this.Nonce);
            //Copy the nonce to the new array
            nonce.CopyTo(contents, 32);
            //Copy the data to the new array
            this.Data.CopyTo(contents, 36);

            return contents;
        }

        public byte[] ComputeHash()
        {
            //Get the internal contents for this block ([prevousblockhash+nonce+data]
            var contents = this.GetInternalContents();
            //Calculate the hash
            var hash = sha256.ComputeHash(contents);

            return hash;
        }

        public bool IsValid()
        {
            //Check if the calculated hash conforms to the difficulty level. It should start with n number of 0 bytes
            return this.Hash.Take(this.Difficulty).All(b => b == new byte());
        }
    }
}