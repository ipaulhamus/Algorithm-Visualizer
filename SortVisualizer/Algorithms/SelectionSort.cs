namespace SortVisualizer.Algorithms
{
    public class SelectionSort : ISortable
    {
        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {
            //This sort will iterate through the array, looking for the smallest index, then performing a swap to put the smallest at the beginning
            for (int i = 0; i < array.Length - 1; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                int minIndex = i;

                for (int j = i + 1; j < array.Length; j++) 
                {
                    //We're iterating through the array, looking for the smallest index
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if(minIndex != i)
                {
                    //Swapping the minimum index with the first unsorted element
                    int temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                    await Repaint(10);
                }
            }
        }
    }
}
