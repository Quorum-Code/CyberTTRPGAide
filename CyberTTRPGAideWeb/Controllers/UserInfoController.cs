using CyberTTRPGAideWeb.Data;
using CyberTTRPGAideWeb.Models;
using CyberTTRPGAideWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberTTRPGAideWeb.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UserInfoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserInfoViewModel viewModel) 
        {
            var userInfo = new UserInfo
            {
                Username = viewModel.Username,
                Email = viewModel.Email,
            };

            await dbContext.UserInfos.AddAsync(userInfo);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var userInfos = await dbContext.UserInfos.ToListAsync();

            return View(userInfos);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var userInfo = await dbContext.UserInfos.FindAsync(id);

            return View(userInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserInfo viewModel) 
        {
            var userInfo = await dbContext.UserInfos.FindAsync(viewModel.Id);

            if (userInfo is not null) 
            {
                userInfo.Username = viewModel.Username;
                userInfo.Email = viewModel.Email;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "UserInfo");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserInfo viewModel) 
        {
            var userInfo = await dbContext.UserInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (userInfo is not null) 
            {
                dbContext.UserInfos.Remove(userInfo);
                await dbContext.SaveChangesAsync();
            }

			return RedirectToAction("List", "UserInfo");
		}
    }
}
