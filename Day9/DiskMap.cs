using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    internal class DiskMap
    {
        private int[] blocks;
        private long checkSum = 0;
        public DiskMap(string formatString)
        {
            ReadFormatString(formatString);
            DefragDisk();
        }

        public long CheckSum => checkSum;

        private void DefragDisk()
        {
            // count how many blank (-1) blocks there are
            int blankCount = blocks.Count(i => i == -1);
            // work backwards and find the last non (-1) block in the array
            for (int i = blocks.Length - 1; i >= 0; i--)
            {
                if (blocks[i] != -1)
                {
                    // find the first -1 block in the array
                    for (int j = 0; j < i; j++)
                    {
                        if (blocks[j] == -1)
                        {
                            blocks[j] = blocks[i];
                            blocks[i] = -1;
                            break;
                        }
                    }
                }
                //if there are no more -1 blocks in the array, break
                if (!blocks.Take(i).Any(j => j == -1))
                {
                    break;
                }
            }

            // calculate the checksum
            for (int i = 0; i < blocks.Length - blankCount; i++)
            {
                checkSum += blocks[i] * i;
            }
        }

        private void ReadFormatString(string formatString)
        {
            int blockCount = formatString.Select(i => int.Parse(i.ToString())).Sum(i => i);
            blocks = new int[blockCount];
            int blockIndex = 0;
            int fileIndex = 0;
            for (int i = 0; i < formatString.Length; i++)
            {
                int blockLength = int.Parse(formatString[i].ToString());
                for (int j = 0; j < blockLength; j++)
                {
                    blocks[blockIndex++] = (i % 2 == 0) ? fileIndex : -1;
                }
                if (i % 2 == 0)
                {
                    fileIndex++;
                }
            }
        }
    }
}
