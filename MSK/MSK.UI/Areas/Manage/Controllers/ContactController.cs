using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.ContactModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            this._contactService = contactService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var contacts = await _contactService.GetAll(null, null);
            if (contacts is null)
            {
                return NotFound();
            }
            List<ContactIndexDto> contactIndexDtos = new List<ContactIndexDto>();
            foreach (var contact in contacts)
            {
                ContactIndexDto contactIndexDto = _mapper.Map<ContactIndexDto>(contact);
                contactIndexDtos.Add(contactIndexDto);
            }
            PaginatedList<ContactIndexDto> PaginatedConttactIndexDtos = PaginatedList<ContactIndexDto>.Create
                (contactIndexDtos.AsQueryable(), page, 50);

            return View(PaginatedConttactIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateDto contactCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(contactCreateDto);
            }
            try
            {
                await _contactService.CreateAsync(contactCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var contact = await _contactService.GetById(id);
            if (contact is null)
            {
                return NotFound();
            }
            ContactUpdateDto contactUpdateDto = _mapper.Map<ContactUpdateDto>(contact);
            return View(contactUpdateDto);
        }

        public async Task<IActionResult> Update(ContactUpdateDto contactUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(contactUpdateDto);
            }
            try
            {
                await _contactService.UpdateAsync(contactUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Contact");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _contactService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Contact");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _contactService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Contact");
        }
    }
}
