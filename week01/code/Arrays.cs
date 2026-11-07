public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN:
        // 1. Create a new array of doubles that has 'length' elements.
        // 2. Start a loop with an index i that goes from 0 up to length - 1.
        // 3. For each position i, the multiple is number * (i + 1), because the
        //    first multiple is number * 1, the second is number * 2, and so on.
        // 4. Store that value in the array at index i.
        // 5. When the loop finishes, return the array.

        double[] multiples = new double[length];
        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN:
        // 1. Rotating right by 'amount' means the last 'amount' items move to the
        //    front of the list, and everything else shifts to the right.
        // 2. Work out where the tail starts: startIndex = data.Count - amount.
        // 3. Use GetRange(startIndex, amount) to copy the last 'amount' items
        //    into a new list called tail.
        // 4. Use RemoveRange(startIndex, amount) to delete those items from the
        //    end of the original list.
        // 5. Use InsertRange(0, tail) to put the copied items at the front of
        //    the original list.
        // 6. The original list is changed in place, so nothing is returned.
        //    (If amount equals data.Count, startIndex is 0, so the whole list
        //    is moved to the front again and the list is unchanged, which is correct.)

        int startIndex = data.Count - amount;
        List<int> tail = data.GetRange(startIndex, amount);
        data.RemoveRange(startIndex, amount);
        data.InsertRange(0, tail);
    }
}