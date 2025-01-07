using System.Globalization;

namespace SortVisualizer.Algorithms
{
    public class OddEvenSort : ISortable

    {
        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {
            //OddEven: Similar to a bubble sort, but this sort iterates through odd numbers first, then iterates through even numbers afterwards

            bool arraySorted = false;
            int lastElement = array.Length - 1;

            while (arraySorted != true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                arraySorted = true;

                for (int i = 1; i < lastElement; i++) //Iterating through the odd elements first, performing a swap and setting sorted to false when neccessary
                {
                    //We need to start i as "1" here
                    if (array[i] > array[i + 1])
                    {
                        if (i % 2 == 1) //Checking if the given number is odd, if it is, we will perform the swap
                        {
                            int temp = array[i];
                            array[i] = array[i + 1];
                            array[i + 1] = temp;
                            arraySorted = false;
                            await Repaint(1);
                        }
                    }
                }
                for (int j = 0; j < lastElement; j++) //Iterating through the even elements second
                {
                    if (array[j] > array[j + 1])
                    {
                        if (j % 2 == 0) //Checking if the number is even, if it is, we will perform the swap
                        {
                            int temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                            arraySorted = false;
                            await Repaint(1);
                        }
                    }
                }

                //Early exit
                if (arraySorted)
                {
                    return;
                }
            }
        }
    }
}
