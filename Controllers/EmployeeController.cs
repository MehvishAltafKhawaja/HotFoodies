using Microsoft.AspNetCore.Mvc;
using ASPNETMVC.Models;


namespace ASPNETMVC.Controllers;

public class EmployeeController : Controller
{
    private readonly AjaxDropDownContext context;

    public EmployeeController(AjaxDropDownContext context)
    {
        this.context = context;
    }

    public IActionResult EmployeeList()
    {
        var emp = context.Employees.ToList();

        return View(emp);
    }
    
    [HttpGet]
    public IActionResult EmployeeCreate()
    {   List<State> states = context.States.ToList();
        ViewBag.States = states;
        return View();
    }



[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult EmployeeCreate(Employee employee)
{
    var districtExists = context.Districts.Any(x =>
        x.DistrictId == employee.DistrictId &&
        x.StateId == employee.StateId);

    if (!districtExists)
    {
        ModelState.AddModelError(
            "DistrictId",
            "Please select a valid district."
        );
    }

    if (ModelState.IsValid)
    {
        context.Employees.Add(employee);
        context.SaveChanges();

        return RedirectToAction("EmployeeList");
    }

    ViewBag.States = context.States.ToList();

    return View(employee);
}



    [HttpGet]
    public JsonResult GetDistrict(int id)
    {
         var districts = context.Districts
                               .Where(x => x.StateId == id)
                               .Select(x => new
                               {
                                   DistrictId = x.DistrictId,
                                   DistrictName = x.DistrictName
                               })
                               .ToList();

        return Json(districts);
    }
}