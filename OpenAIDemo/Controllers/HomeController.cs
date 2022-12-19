using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAIDemo.Core;
using OpenAIDemo.Helper;
using OpenAIDemo.Models;
using RestSharp;
using System.Diagnostics;

namespace OpenAIDemo.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AnswerQuestions()
        {
            return View();
        }
        public async Task<IActionResult> AwesomeChatGPTPrompts()
        {
            string url = "https://github.com/f/awesome-chatgpt-prompts/raw/main/prompts.csv";
            var client = new RestClient(url);
            byte[] response = await client.DownloadDataAsync(new RestRequest("#"));

            var SAVE_PATH = Path.Combine(GlobalContext.SystemConfig.ContentRootPath, "prompts.csv");
            System.IO.File.WriteAllBytes(SAVE_PATH, response);


            List<AwesomeData> dataList = new List<AwesomeData>();
            if (System.IO.File.Exists(SAVE_PATH))
            {
                var dt = DataTable.New.ReadLazy(SAVE_PATH);
                foreach (var row in dt.Rows)
                {
                    AwesomeData data = new AwesomeData();
                    data.act = row["act"].ToString();
                    data.prompt = row["prompt"].ToString();

                    dataList.Add(data);
                }
            }
            return View(dataList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public async Task<IActionResult> SubmitQuestion(QuestionForm param)
        {
            TData<AnswerResponse> obj = new TData<AnswerResponse>();

            if (!string.IsNullOrEmpty(param.prompt))
            {

                param.max_tokens = 250;
                param.model = "text-davinci-003";
                param.temperature = (float)0.7;
                param.top_p = 1;
                param.frequency_penalty = 0;
                param.presence_penalty = 0;


                var url = "https://api.openai.com/v1/completions";
                var jsonResult = await url.WebPostAsync(param);

                if (!string.IsNullOrEmpty(jsonResult))
                {
                    AnswerResponse resp = JsonConvert.DeserializeObject<AnswerResponse>(jsonResult);

                    if (resp != null && resp.choices != null && resp.choices.Any())
                    {
                        obj.ToSuccess(resp);
                    }
                    else
                    {
                        obj.ToError("Failed To Answer");
                    }


                }
                else
                {
                    obj.ToError("Json Result Empty");
                }
            }
            else
            {
                obj.ToError("Question Empty");
            }

            return Json(obj);
        }


        [HttpPost]
        public async Task<IActionResult> GenerateImage(GenerateImageForm param)
        {
            // create a response object
            TData<ImageResponse> obj = new TData<ImageResponse>();
            var resp = new ImageResponse();

            var input = new TextToImageForm();
            input.n = (short)param.quantity;
            input.prompt = param.txt;
            input.size = param.sel;

            //obj.ToSuccess(resp);
            var url = "https://api.openai.com/v1/images/generations";
            var jsonResult = await url.WebPostAsync(input);
            if (!string.IsNullOrEmpty(jsonResult))
            {
                resp = JsonConvert.DeserializeObject<ImageResponse>(jsonResult);
                obj.ToSuccess(resp);
            }
            else
            {
                obj.ToError("Json Result Empty");
            }

            return Json(obj);
        }

    }
}