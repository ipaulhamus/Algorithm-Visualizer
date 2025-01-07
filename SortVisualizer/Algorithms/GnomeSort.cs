namespace SortVisualizer.Algorithms
{
    public class GnomeSort : ISortable
    {
        public async Task SortAsync(int[] array, Func<int?, Task> Repaint, CancellationToken cancellationToken)
        {
            //Gnome Sort: A sort created to mimic the actions of a garden gnome sorting pots
            //This sort is similar to a insertion sort, but with worse performance due to not using a nested loop

            int gnomePosition = 0;

            while (gnomePosition < array.Length) //Iterating until the gnome is at the end of the array
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (gnomePosition == array.Length)
                {
                    //Our gnome has completed it's mission, great job gnome
                    return;
                }
                if(gnomePosition == 0) //A contraint is that the gnome must move forward if it is at index 0
                {
                    gnomePosition++;
                }
                else if (array[gnomePosition] >= array[gnomePosition - 1]) //We skip a pot if it's already sorted
                                                                           //We use the index before the pot as a check and swap to ensure we don't go out of bounds at the end
                {
                    gnomePosition++;
                }
                else
                {
                    //If the value at the gnome's position is more than the value before, we perform a swap
                    int temp = array[gnomePosition];
                    array[gnomePosition] = array[gnomePosition - 1];
                    array[gnomePosition - 1] = temp;
                    gnomePosition--; 
                    await Repaint(2);
                }
                //Once the array is sorted or the gnome reaches the end, the sort is complete
            }
        }
    }
}

