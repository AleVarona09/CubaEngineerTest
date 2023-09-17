namespace NextPermutation.Models
{
    public class Response
    {
        public int Code { get; set; } // 0 succes | -1 error

        public string Message { get; set; } // response message

        public string Vector { get; set; } // original vector

        public string Next { get; set; } // next permutation

    }
}
