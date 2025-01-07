using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SortVisualizer.Algorithms
{
    public class Quicksort : ISortable
    {

        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {

            async Task<int> PartitionAsync(int leftPointer, int rightPointer) //We are splitting the array into partitions to be sorted
            {
                int pivotIndex = rightPointer;
                int pivot = array[pivotIndex];

                rightPointer--; //Pivot should not be included in the partition

                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    while (array[leftPointer].CompareTo(pivot) < 0)
                    {
                        leftPointer++; //Move the left pointer until we find greater than or equal to pivot value
                    }

                    while (rightPointer >= 0 && array[rightPointer].CompareTo(pivot) > 0)
                    {
                        rightPointer--; //Right pointer will decrement when comparing items less than 0
                    }

                    if (leftPointer >= rightPointer) break;

                    int temp = array[leftPointer];
                    array[leftPointer] = array[rightPointer];
                    array[rightPointer] = temp;

                    await Repaint(5);

                    leftPointer++; //Moving left pointer since we don't need to compare it to it's own position, optimization
                }

                array[pivotIndex] = array[leftPointer];
                array[leftPointer] = pivot;

                await Repaint(5);

                return leftPointer;
            }

            async Task Quicksorting(int leftIndex, int rightIndex) //Performing recursive paritions to sort the array
                //Very fast in terms of steps
            {
                if (rightIndex - leftIndex <= 0) return;

                int pivotIndex = await PartitionAsync(leftIndex, rightIndex);

                await Quicksorting(leftIndex, pivotIndex - 1);

                await Quicksorting(pivotIndex + 1, rightIndex);
            }

            await Quicksorting(0, array.Length - 1);
        }
    }
}
