using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using RunGroupWebApp.Service;
using RunGroupWebApp.ViewModels;
using SQLitePCL;

namespace RunGroupWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httbContextAccessor;
        private readonly IPhotoService _photoService;
        private void MapUserEdit(AppUser user,EditUserDashboardViewModel editVM,ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.Pace=editVM.Pace;
            user.Milage=editVM.Mileage;
            user.ProfileImageUrl=photoResult.Url.ToString();
            user.City=editVM.City;
            user.State=editVM.State;
        }
        public DashboardController(IDashboardRepository dashboardRepository,
            IHttpContextAccessor httbContextAccessor,IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httbContextAccessor= httbContextAccessor;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId=_httbContextAccessor.HttpContext.User.GetUserId();
            var user=await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                Pace=user.Pace,
                Mileage=user.Milage,
                ProfileImageUrl=user.ProfileImageUrl,
                City=user.City,
                State=user.State
            };
            return View(editUserViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile",editVM);
            }

            var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user,editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could no delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");

            }
        }



    }
}
