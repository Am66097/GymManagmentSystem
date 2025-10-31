using AutoMapper;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.MemberShipsViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShipService _memberShipService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepository;
        private readonly IMemberShipRepository _memberShipRepository;

        public MemberShipController(IMemberShipService memberShipService,IUnitOfWork unitOfWork,IMapper mapper,IPlanRepository planRepository,IMemberShipRepository memberShipRepository)
        {
            _memberShipService = memberShipService;
            _unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._planRepository = planRepository;
            this._memberShipRepository = memberShipRepository;
        }

        #region Get All MemberShips Action
        public ActionResult Index()
        {
            var memberships = _memberShipService.GetAll();

            var membershipsVM = _mapper.Map<IEnumerable<MemberShipViewModel>>(memberships);

            return View(membershipsVM);
        }


        #endregion

        #region Create MemberShips Action
        [HttpGet]
        public IActionResult Create()
        {
            var model = new MemberShipCreateViewModel
            {
                Members = _unitOfWork.MemberRepository.GetAll()
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }),
                Plans = _unitOfWork.PlanRepository.GetAll()
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MemberShipCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Members = _unitOfWork.MemberRepository.GetAll()
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name });
                model.Plans = _unitOfWork.PlanRepository.GetAll()
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
                return View(model);
            }

            var selectedPlan = _unitOfWork.PlanRepository.GetById(model.PlanId);
            if (selectedPlan == null)
            {
                ModelState.AddModelError("", "Selected plan not found.");
                return View(model);
            }

            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(selectedPlan.DurationInDays);

            var membership = new MemberShip
            {
                MemberId = model.MemberId,
                PlanId = model.PlanId,
                UpdatedAt = startDate,
                EndDate = endDate,
                CreatedAt = DateTime.Now
            };

            _unitOfWork.MemberShipRepository.Add(membership);
            _unitOfWork.SaveChanges();

            TempData["SuccessMessage"] = "Membership created successfully!";
            return RedirectToAction("Index");
        }

        #endregion

        #region Cancel Action

        #endregion
    }
}
