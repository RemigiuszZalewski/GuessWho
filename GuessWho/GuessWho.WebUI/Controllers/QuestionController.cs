using GuessWho.Domain.Dtos;
using GuessWho.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessWho.WebUI.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetQuestions(int questionsCount, string language)
        {
            return Ok(await _questionService.GetQuestionsAsync(questionsCount, language));
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestions([FromBody] List<QuestionDto> questionDtos)
        {
            await _questionService.AddQuestionsAsync(questionDtos);
            return Ok();
        }
    }
}
