using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Buffers;

namespace SortVisualizer.Algorithms
{
    public class InsertionSort : ISortable
    {
        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {
            //This sort takes each value out one at a time and compares it with the rest of the array, inserting it in the correct spot

            for (int i = 1; i < array.Length; i++) //"i" is the value to be inserted
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                int temp = array[i]; 
                int j = i - 1;

                for(; j >= 0 && temp < array[j]; j--) //Comparing the value of array[i] (out value to be inserted) and inserting it in the correct spot
                {
                    array[j + 1] = array[j];
                }

                array[j + 1] = temp;
                await Repaint(10);
            }
        }
    }
}
