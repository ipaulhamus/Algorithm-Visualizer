using System.Drawing;

namespace SortVisualizer.Algorithms
{
    public class MergeSort : ISortable
    {
        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {
            
            //"divide and conquer" algorithm, divides the array into subarrays
            async Task Merge(int left, int middle, int right) //We need all three points of the sub-array, we need to know it's size
            {
                int n1 = middle - left + 1; //Getting size of the left sub-array
                int n2 = right - middle; //Getting the size of the right sub-array

                //Initializing our subarrays
                int[] leftArray = new int[n1];
                int[] rightArray = new int[n2];

                //Adding data to our subarrays
                for (int i = 0; i < n1; i++) 
                {
                    leftArray[i] = array[left + i];
                }

                for (int i = 0; i < n2; i++)
                {
                    rightArray[i] = array[middle + 1 + i];
                }


                int leftIndex = 0;
                int rightIndex = 0;

                int mergeIndex = left;

                while (leftIndex < n1 && rightIndex < n2)
                {
                    //The element in the left subarray is smaller than the one in the right array
                    if (leftArray[leftIndex].CompareTo(rightArray[rightIndex]) < 0)
                    {
                        array[mergeIndex] = leftArray[leftIndex];
                        leftIndex++;
                        await Repaint(2);
                    }
                    else //The opposite case, the right subarray is smaller than left
                    {
                        array[mergeIndex] = rightArray[rightIndex];
                        rightIndex++;
                        await Repaint(2);
                    }
                    //Increasing the index of the merged array so we aren't inserting in the same spot over and over
                    mergeIndex++;
                }


                //This is merging back in all the remaining elements that weren't merged back from the sub-arrays in this while loop
                //We know these are in the right order because that is the way merge sort works
                while (leftIndex < n1)
                {
                    array[mergeIndex] = leftArray[leftIndex];
                    leftIndex++;
                    mergeIndex++;
                    await Repaint(2);
                }

                while (rightIndex < n2)
                {
                    array[mergeIndex] = rightArray[rightIndex];
                    rightIndex++;
                    mergeIndex++;
                    await Repaint(2);
                }

            }

            async Task MergeSort(int left, int right)
            {
                if (left >= right) return;

                int middle = (left + right) / 2;

                await MergeSort(left, middle);
                await MergeSort(middle + 1, right); //Recursively breaking down the array to it's base case

                await Merge(left, middle, right); //'middle' is how the method knows how to divide the array in half
            }

            //Performing the merge sort
            await MergeSort(0, array.Length - 1);
            
        }
    }
}
