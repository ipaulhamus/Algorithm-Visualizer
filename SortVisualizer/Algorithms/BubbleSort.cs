using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace SortVisualizer.Algorithms
{
    public class BubbleSort : ISortable
    {
        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {
            //Creating a bool to tell whether the array is sorted and giving an index to the point the array is sorted
            //We will pass through and perform swaps until an iteration has no swaps to be performed, very slow

            bool sorted = false;
            int unsortedUntilIndex = array.Length - 1;

            while (sorted == false) //Loops until sorted returns true
            {
                cancellationToken.ThrowIfCancellationRequested();
                sorted = true;

                for(int i = 0; i < unsortedUntilIndex; i++)
                {
                    if (array[i] > array[i + 1]) //If the element after the element are checking through is smaller,
                        //Then we perform a swap and set sorted to false
                    {
                        //Swap
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        sorted = false;
                        await Repaint(1);
                    }
                }
                unsortedUntilIndex--; 
            }
        }
    }
}
