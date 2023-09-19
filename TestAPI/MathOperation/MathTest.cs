using NextPermutation.Core;
using NextPermutation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestAPI.MathOperation
{
    public class MathTest
    {
        
        
        [Fact]
        public void FindNextPermutationTest() 
        {
            string data = "1,2,3";
            var math = new MathOperations();
            Response response = new Response{
                Code = 0,
                Message = "Next permutation find.",
                Vector = "1,2,3",
                Next = "1,3,2"
            };
            string respSer = JsonSerializer.Serialize(response);
            Response result = math.NextPermutation(data).Result;
            string resultSer = JsonSerializer.Serialize(response);

            Assert.Equal(respSer, resultSer);

        }


        [Fact]
        public void FindNextPermutationTest2()
        {
            string data = "1,2,3,6,5,4";
            var math = new MathOperations();
            Response response = new Response
            {
                Code = 0,
                Message = "Next permutation find.",
                Vector = "1,2,3,6,5,4",
                Next = "1,2,4,3,5,6"
            };
            string respSer = JsonSerializer.Serialize(response);
            Response result = math.NextPermutation(data).Result;
            string resultSer = JsonSerializer.Serialize(response);

            Assert.Equal(respSer, resultSer);

        }



        [Fact]
        public void FindNextPermutationTestBiggest()
        {
            string data = "3,2,1";
            var math = new MathOperations();
            Response response = new Response
            {
                Code = 0,
                Message = "There is no greater permutation.",
                Vector = "3,2,1",
                Next = ""
            };
            string respSer = JsonSerializer.Serialize(response);
            Response result = math.NextPermutation(data).Result;
            string resultSer = JsonSerializer.Serialize(response);

            Assert.Equal(respSer, resultSer);

        }





    }
}
