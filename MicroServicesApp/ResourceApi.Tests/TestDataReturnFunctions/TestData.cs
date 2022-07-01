using ResourceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourceApi.Tests.TestDataReturnFunctions
{
    public static class TestData
    {
        public static async Task<IEnumerable<TestModel>> ReturnTests(TestModel model)
        {
            return new List<TestModel>() { model };
        }
        public static async Task<TestModel> ReturnTest(TestModel model,int id)
        {   if(model.Id==id)
                return model;
            return null;
        }

        public static async Task<bool> CheckModelValidation(TestModel model)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results);
        }
    }
}
