using GymManagmetBLL.Service.Interfasces;
using GymManagmetBLL.ViewModels.MemberViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmetPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public ActionResult Index()
        {
            var mamber = _memberService.GetAllMembers();

            return View();
        }
        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of member cant be 0 or megative";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberDetails(id);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not found";
                return RedirectToAction(nameof(Index));
            }
            return View(member);

        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of member cant be 0 or megative";
                return RedirectToAction(nameof(Index));
            }
           

            var healthRecord = _memberService.GetMemberHealthRecordDetails(id);
            if (healthRecord is null)
            {
                TempData["ErrorMessage"] = " Health Record Not found";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateMember(CreateMemberViewModel createMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Data and Missing Field");
                return View(nameof(Create), createMember);
            }
            bool result = _memberService.CreateMember(createMember);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Create Member";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
