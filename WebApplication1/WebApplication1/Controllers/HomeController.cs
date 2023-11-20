using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext context)
        {
            _appDbContext = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Sliders = _appDbContext.Sliders.ToList();


            return View(homeViewModel);
        }



    }
}