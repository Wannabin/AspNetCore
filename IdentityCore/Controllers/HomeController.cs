using IdentityCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCore.Controllers
{
    #region Authorization (권한)
    //Authorization (권한)
    //인증은 통과했는데, 모든 접근 권한을 부여하진 않을 것!

    // 1) Request
    // 2) Routing
    // 3) Authentication 미들웨어
    // 4) MVC 미들웨어
    // - Authorize 필터
    // - Action
    // - View

    // 필터 특성상 Global, Controller, Action등 적용 범위 세부 설정 가능
    // [Authorize] [AllowAnonymous]

    // 권한이 없으면
    // 다음과 같은 IActionResult 생성
    // - ChallengeResult (로그인하지 않음)
    // - ForbidResult (로그인은 했는데, 그냥 권한이 없음)
    // WebAPI 경우 쿠키가 아니라 토큰을 이용, 개념적으로는 비슷
    // -MVC는 ChallengeResult, ForbidResult에 따라 특정 View를 보여준다.
    // - WebAPI는 401, 403번 Status Code를 반환
    
    // Policy
    // - Request가 Authorize(권한을 부여받기)되기 위해 필요한 정책
    // [Authorize("AdminPolicy")]
    // AdminPolicy 정책은 어떻게 만드나?
    // ConfigureServices

    // Qustom Policy
    //

    #endregion

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize("AdminPolicy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
