using advent2026.ApiService.Models;
using Microsoft.AspNetCore.Mvc;

namespace advent2026.ApiService.Controllers
{
    [ApiController]
    [Route("api/day/[controller]")]
    public abstract class DayControllerBase : ControllerBase
    {
        public int DayNumber { get; init; }
        protected AdventInput Input { get; init; }

        public DayControllerBase(int dayNumber)
        {
            DayNumber = dayNumber;
            
            // TODO: get the input and toss it into this.Input
            Input = new(DayNumber);
        }
        
        protected abstract Task<PartResponse> DoPartA(bool test);
        protected abstract Task<PartResponse> DoPartB(bool test);

        [HttpGet]
        [Route("partA")]
        public async Task<PartResponse> PartA() => await DoPartA(false);

        [HttpGet]
        [Route("test/partA")]
        public async Task<PartResponse> TestA() => await DoPartA(true);
        
        [HttpGet]
        [Route("partB")]
        public async Task<PartResponse> PartB() => await DoPartB(false);
        
        [HttpGet]
        [Route("test/partB")]
        public async Task<PartResponse> TestB() => await DoPartB(true);

        [HttpGet]
        [Route("")]
        public async Task<AdventResponse> DoEverything()
        {
            var partA = await PartA();
            var partB = await PartB();

            return BuildResponse(partA, partB);
        }

        [HttpGet]
        [Route("test")]
        public async Task<AdventResponse> DoTests()
        {
            var partA = await TestA();
            var partB = await TestB();

            return BuildResponse(partA, partB);
        }
        
        private AdventResponse BuildResponse(PartResponse partA, PartResponse partB)
        {
            var messages = new List<string>();
            var errors = new List<string>();

            if (partA.Messages is not null)
                messages.AddRange(partA.Messages.Select(message => $"[partA] {message}"));
            if (partA.Errors is not null)
                errors.AddRange(partA.Errors.Select(error => $"[partA] {error}"));
            
            if (partB.Messages is not null)
                messages.AddRange(partB.Messages.Select(message => $"[partB] {message}"));
            if (partB.Errors is not null)
                errors.AddRange(partB.Errors.Select(error => $"[partB] {error}"));

            return new()
            {
                Day = DayNumber,
                PartA = partA.Result,
                PartB = partB.Result,
                Messages = messages,
                Errors = errors
            };
        }
    }
}
