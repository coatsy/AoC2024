using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    internal class DiskMap
    {
        private int[] blocks, blocks2;
        private long defrag1CheckSum = 0, defrag2CheckSum = 0;
        private readonly Dictionary<int, FileLocation> fileBlocks = [];
        private readonly List<FileLocation> blankBlocks = [];
        public DiskMap(string formatString)
        {
            ReadFormatString(formatString);
            DefragDisk1();
            DefragDisk2();
        }

        public long Defrag1CheckSum => defrag1CheckSum;
        public long Defrag2CheckSum => defrag2CheckSum;

        private void DefragDisk2()
        {
            // work backwards through the fileBlocks and move them if possible
            foreach (var fileId in fileBlocks.Keys.OrderByDescending(k=>k))
            {
                // now, find the first blank block that can fit the file
                var file = fileBlocks[fileId];
                var blankBlock = blankBlocks
                    .Where(b=>b.FileOffset <= file.FileOffset)
                    .OrderBy(b=>b.FileOffset)
                    .FirstOrDefault(b => b.FileLength >= file.FileLength);
                if (blankBlock != null)
                {
                    // move the file to the blank block
                    var originalFileOffset = file.FileOffset;
                    file.FileOffset = blankBlock.FileOffset;
                    if (file.FileLength < blankBlock.FileLength)
                    {
                        // split the blank block
                        blankBlock.FileOffset += file.FileLength;
                        blankBlock.FileLength -= file.FileLength;
                        blankBlocks.Add(new FileLocation { 
                            FileId = -1, 
                            FileOffset = originalFileOffset, 
                            FileLength = file.FileLength 
                        });
                    }
                    else
                    {
                        // move the blank block
                        blankBlock.FileOffset = originalFileOffset;
                    }

                }
            }

            // calculate the checksum
            var allBlocks = fileBlocks.Values.Concat(blankBlocks).OrderBy(a=>a.FileOffset);
            foreach (var block in allBlocks)
            {
                for (int i = block.FileOffset; i < block.FileOffset + block.FileLength; i++)
                {
                    blocks2[i] = block.FileId;
                    defrag2CheckSum += block.FileId == -1 ? 0 : block.FileId * i;
                }
            }
        }


        private void DefragDisk1()
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
                defrag1CheckSum += blocks[i] * i;
            }
        }

        private void ReadFormatString(string formatString)
        {
            int blockCount = formatString.Select(i => int.Parse(i.ToString())).Sum(i => i);
            blocks = new int[blockCount];
            blocks2 = new int[blockCount];
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
                    fileBlocks[fileIndex] = new FileLocation {
                        FileId = fileIndex, FileLength = blockLength, FileOffset = blockIndex - blockLength };
                    fileIndex++;
                }
                else
                {
                    if (blockLength > 0)
                    {
                        blankBlocks.Add(new FileLocation
                        {
                            FileId = -1,
                            FileLength = blockLength,
                            FileOffset = blockIndex - blockLength
                        });
                    }
                }
            }
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks2[i] = blocks[i];
            }
        }
    }

    internal class FileLocation
    {
        public int FileId { get; set; }
        public int FileOffset { get; set; }
        public int FileLength { get; set; }
    }
}




