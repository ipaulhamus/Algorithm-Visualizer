namespace SortVisualizer.Algorithms
{
    public class PancakeSort : ISortable
    {
        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {
            //Pancake Sort: A sort that performs 'flips' of subarrays (pancakes) rather than simply swapping values
            //Finds the maximum value similar to Selection sort, but instead uses subarrays that are flipped until the maximum value is in the right spot

            async Task FlipPancake(int[] array, int flipIndex)
            {
                //This function will flip the array from the start of the array to the end point given
                int firstElement = 0;
                //Repeating this step until flip is complete
                while (firstElement < flipIndex)
                {
                    //Performing swaps until our pancake is flipped
                    int temp = array[firstElement];
                    array[firstElement] = array[flipIndex];
                    array[flipIndex] = temp;
                    await Repaint(1);

                    //Incrementing the first element index and decrementing the flip index
                    firstElement++;
                    flipIndex--;
                }
            }

            int LargestElement(int subArrayEnd) //Finding the largest element in the current subarray, the one we need to swap
            {
                int largest = 0;

                for (int i = 0; i < subArrayEnd; i++)
                {
                    int current = array[i];
                    int largestIndex = array[largest];

                    if (current > largestIndex)
                    {
                        largest = i;
                    }
                }
                return largest;
            }

            async Task SortArray() //The pancake sort itself
            {
                for (int subArrayEnd = array.Length; subArrayEnd > 1; --subArrayEnd)
                {


                    int maxIndex = LargestElement(subArrayEnd); //Current maximum index is the maximum element in the subarray

                    if (maxIndex != subArrayEnd - 1) //If the maximum value isn't the end of the subarray, then we flip
                    {
                        await FlipPancake(array, maxIndex);
                        await FlipPancake(array, subArrayEnd - 1);
                    }
                }
            }

            await SortArray(); //Sorts the array
        }
    }
}
