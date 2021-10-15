using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IroojBackend.Models;
using IroojBackend.socket;

namespace IroojBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [Route("/grad")]
        public IActionResult Grad(string language, string code, long questionNumber)
        {
            var (timeLimit, memoryLimit, testCaseCount) = DBModel.GetQuestionInfo(questionNumber);
            SocketMain.socket.WriteGradingInfo(timeLimit, memoryLimit, testCaseCount, language, code);
            return Ok("Finished, Recommend: Post type");
        }

        [HttpPost]
        [Route("/grad")]
        public IActionResult Grad()
        {
            var questionNumber = long.Parse(Request.Form["question_number"].ToString());
            var language = Request.Form["language"].ToString();
            var code = Request.Form["code"].ToString();
            var (timeLimit, memoryLimit, testCaseCount) = DBModel.GetQuestionInfo(questionNumber);
            SocketMain.socket.WriteGradingInfo(timeLimit, memoryLimit, testCaseCount, language, code);
            return Ok("Finished");
        }
        
        [HttpGet]
        [Route("/judgeresult")]
        public IActionResult GetResult(long judgeNumber) 
            => Ok(DBModel.GetGradData(judgeNumber));

        [HttpGet]
        [Route("/problem")]
        public IActionResult GetProblemData(long problemNumber)
            => Ok(DBModel.GetProblemData(problemNumber));
    }
}