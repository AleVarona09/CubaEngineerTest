using NextPermutation.Models;

namespace NextPermutation.Core
{
    public interface IOperations
    {
        Task<Response> NextPermutation(string vector);
        void ReverseVector(int[] vec, int star, int end);

    }
}
