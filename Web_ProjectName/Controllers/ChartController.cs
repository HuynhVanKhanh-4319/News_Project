using Microsoft.AspNetCore.Mvc;

namespace Web_ProjectName.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetDataByTeam(string teamId)
        {
            var data = new List<object>();

            if (teamId == "D1")
            {
                data = new List<object>
                {
                    new { name = "D1-C1", value = 1274.05 },
                    new { name = "D1-C2", value = 1267.02 },
                    new { name = "D1-C3", value = 3068.49 },
                    new { name = "D1-C4", value = 2710.16 },
                    new { name = "D1-C5", value = 1987.89 },
                    new { name = "D1-C6", value = 1879.69 },
                    new { name = "D1-C7", value = 4629.67 },
                    new { name = "D1-C8", value = 3783.74 }
                };
            }
            else if (teamId == "D2")
            {
                data = new List<object>
                {
                    new { name = "D2-C1", value = 1500 },
                    new { name = "D2-C2", value = 2000 },
                    new { name = "D2-C3", value = 1800 }
                };
            }

            return Json(data);
        }

        [HttpGet]
        public IActionResult GetDataChartColum(string teamId)
        {
            var fakeData = new
            {
                teams = new[]
                {
                    new {
                        id = "D1",
                        name = "Đội 1",
                        units = new[] {
                            new { name = "D1-C1", chen = 1274.05, day = 100.0, tong = 1374.05 },
                            new { name = "D1-C2", chen = 1267.02, day = 150.0, tong = 1417.02 },
                            new { name = "D1-C3", chen = 3068.49, day = 200.0, tong = 3268.49 },
                            new { name = "D1-C4", chen = 2710.16, day = 250.0, tong = 2960.16 },
                            new { name = "D1-C5", chen = 1987.89, day = 190.0, tong = 2177.89 },
                            new { name = "D1-C6", chen = 1879.69, day = 184.0, tong = 2063.69 },
                            new { name = "D1-C7", chen = 4629.67, day = 256.0, tong = 4885.67 },
                            new { name = "D1-C8", chen = 3783.74, day = 270.0, tong = 4053.74 }
                        }
                    },
                    new {
                        id = "D2",
                        name = "Đội 2",
                        units = new[] {
                            new { name = "D2-C1", chen = 1200.0, day = 100.0, tong = 1300.0 },
                            new { name = "D2-C2", chen = 1500.0, day = 200.0, tong = 1700.0 },
                            new { name = "D2-C3", chen = 1800.0, day = 150.0, tong = 1950.0 },
                            new { name = "D2-C4", chen = 1900.0, day = 150.0, tong = 2050.0 },
                            new { name = "D2-C5", chen = 1700.0, day = 150.0, tong = 1850.0 },
                            new { name = "D2-C6", chen = 1600.0, day = 150.0, tong = 1750.0 }
                        }
                    }
                }
            };

            var teams = ((IEnumerable<dynamic>)fakeData.teams).ToList();

            if (string.IsNullOrEmpty(teamId))
            {
                var result = teams.Select(t => new
                {
                    name = t.name,
                    chen = ((IEnumerable<dynamic>)t.units).Sum(u => (double)u.chen),
                    day = ((IEnumerable<dynamic>)t.units).Sum(u => (double)u.day),
                    tong = ((IEnumerable<dynamic>)t.units).Sum(u => (double)u.tong)
                });

                return Json(result);
            }
            else
            {
                var team = teams.FirstOrDefault(t => t.id == teamId);
                if (team == null)
                    return Json(new { error = "Team not found" });

                return Json(((IEnumerable<dynamic>)team.units).Select(u => new
                {
                    name = u.name,
                    chen = (double)u.chen,
                    day = (double)u.day,
                    tong = (double)u.tong
                }));

            }

        }
    }
}
