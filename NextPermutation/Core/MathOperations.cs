using NextPermutation.Models;

namespace NextPermutation.Core
{
    public class MathOperations : IOperations
    {
        public async Task<Response> NextPermutation(string vec)
        {
            Response response = new Response();
            int[] vector = Array.ConvertAll(vec.Split(','), int.Parse);

            int n = vector.Length;

            if (n < 101)
            {
                int i = n - 2, j;

                //find pivot from the end
                bool find = false;
                while (!find && i >= 0)
                {
                    if (vector[i] < vector[i + 1])
                    {
                        find = true;
                    }
                    else
                    {
                        i--;
                    }
                }

                //no next permutation, reverse the vector
                if (!find)
                {
                    ReverseVector(vector, 0, n - 1);
                }
                else //find the next one of pivot
                {
                    j = n - 1;
                    bool flag = false;
                    while (!flag && j > i)
                    {
                        if (vector[j] > vector[i])
                        {
                            int temp = vector[i];
                            vector[i] = vector[j];
                            vector[j] = temp;

                            flag = true;
                        }
                        else
                        {
                            j--;
                        }
                    }

                    // Minimise the suffix part
                    ReverseVector(vector, i + 1, n - 1);

                    response.Code = 0;
                    response.Message = "Next permutation find.";
                    response.Vector = vec;
                    response.Next = vector;
                }
            }
            else
            {
                response.Code = -1;
                response.Message = "The vector must have a max of 100 values.";
                response.Vector = vec;
                response.Next = new int[0];
            }

            return response;
        }

        public void ReverseVector(int[] vec, int star, int end)
        {
            int i = star;
            int j = end;
            while (i < j)
            {
                var temp = vec[i];
                vec[i] = vec[j];
                vec[j] = temp;
                i++;
                j--;
            }
        }
    }
}
