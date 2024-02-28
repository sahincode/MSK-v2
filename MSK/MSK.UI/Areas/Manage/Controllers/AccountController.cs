using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MSK.Business.DTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.UI.ViewModels;

namespace MSK.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IAccountService accountService ,IUserService userService ,IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelDto adminLoginViewModel)
        {
            if (!ModelState.IsValid) return View(adminLoginViewModel);

            try
            {
                await _accountService.Login(adminLoginViewModel);
            }
            catch (InvalidUserCredentialException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("index", "home");
        }


        //public async Task<IActionResult> CreateAdmin()
        //{
        //    User admin = new User()
        //    {
        //        FullName = "sahin ismayilov",
        //        UserName = "sakodiyo",
        //        Email = "sakode@gmail.com"
        //    };

        //    var result = await _userManager.CreateAsync(admin, "Sahin6134@");
        //    await _userManager.AddToRoleAsync(admin, "SuperAdmin");

        //    return Ok(result);
        //}
        public async Task<IActionResult> CreateRoles()
        {
            //IdentityRole role1 = new IdentityRole("Admin");
            //IdentityRole role2 = new IdentityRole("User");
            IdentityRole role3 = new IdentityRole("Voter");

            //await _roleManager.CreateAsync(role1);
            //await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role3);

            return Ok("Roles is Created");
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto ,string cpage)
        {
            var currentPath = HttpContext.Request.Path;
            if (!ModelState.IsValid)
            {
                return Redirect(cpage);
            }
            try
            {
                await _accountService.UpdateUser(updateUserDto);
            }
            catch (NullEntityException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return Redirect(cpage);
            }
            catch(InvalidUserCredentialException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return Redirect(cpage);
            }
            return Redirect(cpage);

        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Roles()
        {
            List<UserWithRoleDto> userWithRoleViewModels = new List<UserWithRoleDto>();
            UserWithRoleDto userWithRoleViewModel = null;
            
            var users =  await _userService.GetAll(null, null);
            if (users is  null)
            {
                return NotFound();
            }
            foreach(var user in users)
            {
                userWithRoleViewModel = new UserWithRoleDto();
                userWithRoleViewModel = _mapper.Map(user, userWithRoleViewModel);

                var roles = await  _userManager.GetRolesAsync(user);
                foreach(var role in roles)
                {
                    var roleOrigin = await _roleManager.FindByNameAsync(role);
                    RoleViewDto roleViewModel = new RoleViewDto
                    {
                        Name = roleOrigin.Name,
                        Id = roleOrigin.Id
                    };
                    userWithRoleViewModel?.Roles.Add(roleViewModel);
                    
                }
                userWithRoleViewModels.Add(userWithRoleViewModel);

            }

            return View(userWithRoleViewModels);
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ToggleRole( string roleId , string userId)
        {
            try
            {
                await _accountService.ToggleRole(roleId, userId);
                return RedirectToAction("roles", "account");


            }
            catch (NullEntityException ex)
            {
                return NotFound();
            }catch(NotChangedRoleException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return RedirectToAction("roles", "account");
            }
            
        }
    }
}
