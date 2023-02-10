using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        {
            switch (sortStrategy)
            {
                case null:
                    return Ok(Summaries);
                case 1:
                    Summaries.Sort();
                    return Ok(Summaries);
                case -1:
                    Summaries.Reverse();
                    return Ok(Summaries);
                default:
                    return BadRequest("������������ �������� ��������� sortStrategy");
            }
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("����� ������ ��������!!!!");
            }

            Summaries[index] = name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("�������� � ����� �������� ���");
            }

            Summaries.RemoveAt(index);
            return Ok();
        }

        [HttpGet("{index}")]
        public string Get(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return "�������� � ����� �������� ���";
            }
            
            return Summaries[index];
        }

        [HttpGet("find-by-name")]
        public int Get(string name)
        {
            int result = 0;

            foreach (string n in Summaries)
            {
                if (n == name)
                    result++;
            }

            return result;
        }
    }
}