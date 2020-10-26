namespace CSharp.DS.Algo.Sorting
{
    public partial class Sorting
    {
        public static void ExternalSort(string filePath)
        {
            // file_size = 5GB of file, available_memory = 1GB
            // 1. Divide the file in num_chunks chunks (e.g 5), (file_size / num_chunks) each (1GB)
            // 2. Perform a QuickSort on the individual chunks, and write those chunks to disk
            // 3. For each chunk on disk, take partition_size (150MB) of data and load into memory
            //    Leave some space available in memory. (e.g. 250MB)
            // 4. For each partitioned chunk, use k-way merge (e.g. 5-way merge) to merge the sorted data
            //    up until the available space buffer (250MB) is filled. Once filled, write it into disk.
            //    It is guaranted that the first buffer will be sorted.
            //    - If we reach the end of the partitioned chunk (150MB), flush it and load the next
            //      partition up until all the 1GB chunk is loaded from disk.
        }
    }
}
