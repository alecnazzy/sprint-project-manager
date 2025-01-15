using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SprintProjectManager.Models;
using Xunit;

namespace SprintProjectManager.Tests.Models
{
    public class SprintTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Sprint_ValidModel_PassesValidation()
        {
            // arrange
            var sprint = new Sprint
            {
                Id = 1,
                Name = "Sprint 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Goal = "Complete all tasks",
                Status = "In Progress"
            };

            // act
            var validationResults = ValidateModel(sprint);

            // assert
            Assert.Empty(validationResults); 
        }

        [Fact]
        public void Sprint_InvalidName_ReturnsValidationErrors()
        {
            // arrange
            var sprint = new Sprint
            {
                Id = 1,
                Name = "invalid name",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Goal = "Complete all tasks",
                Status = "In Progress"
            };

            // act
            var validationResults = ValidateModel(sprint);

            // assert
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The field Name must match the regular expression"));
        }

        [Fact]
        public void Sprint_StartDate_RequiredValidation()
        {
            // arrange
            var sprint = new Sprint
            {
                Id = 1,
                Name = "Sprint 1",
                EndDate = DateTime.Now.AddDays(10),
                Goal = "Complete all tasks",
                Status = "Planning" 
            };

            // act
            var validationResults = ValidateModel(sprint);

            // assert
            Assert.Contains(validationResults, v =>
                v.ErrorMessage == "The Start Date field is required.");
        }


        [Fact]
        public void Sprint_Status_LengthValidation()
        {
            // arrange
            var sprint = new Sprint
            {
                Id = 1,
                Name = "Sprint 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Goal = "Complete all tasks",
                Status = "Inco"
            };

            // act
            var validationResults = ValidateModel(sprint);

            // assert
            Assert.Contains(validationResults, v =>
                v.ErrorMessage == "The field Status must be a string with a minimum length of 6 and a maximum length of 15.");
        }

    }
}
