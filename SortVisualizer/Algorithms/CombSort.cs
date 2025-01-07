namespace SortVisualizer.Algorithms
{
    public class CombSort : ISortable
    {
        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {
            //Comb Sort: A sort that is an improved version of bubbble sort with the goal of removing small values at the beginning of the list with "combs" 
            //Each comb has a specified gap, which iterates until the gap is 1, where a regular bubble sort is performed.

            int combGap = array.Length;

            int shrinkFactor = 13; //The shrink factor represents how much the gap in combs decreases with each iteration
                                   //It is beleived that a value around 1.3-1.4 is most efficient
                                   //Here we use 13 so we can dodge having to change data types
            bool arraySorted = false;

            while (arraySorted == false || combGap > 1)
            {
                cancellationToken.ThrowIfCancellationRequested();

                //Shrinking the gap in our comb by the shrink factor
                combGap = (combGap * 10) / shrinkFactor; //We multiply by 10 so we don't have to use doubles

                arraySorted = true; //Similar to a bubble sort, we're checking if sorted

                if(combGap < 1) //Setting gap to 1 if it is less than 1
                {
                    combGap = 1;
                }

                for (int i = 0; i < array.Length - combGap; i++)
                {
                    if (array[i] > array[i + combGap]) //Comparing the element at the end of the comb gap to the beginning, if the beginning of the comb is more than the end, we swap
                    {
                        int temp = array[i];
                        array[i] = array[i + combGap];
                        array[i + combGap] = temp;
                        arraySorted = false; //Again, similar to bubble sort, we iterate again since the array isn't sorted
                        await Repaint(1);
                    }
                }

                if (arraySorted) //Early exit
                {
                    return;
                }
            }
        }
    }
}
