using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXCustomControl.Models;

namespace DXCustomControl.Controllers
{
    public class HomeController : Controller
    {
        List<Department> allDepartment = DepartmentsProvider.GetDepartments();
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView

           // return RedirectToAction("DataBinding");
            return View(NorthwindDataProvider.GetCustomers());    
        }

        public ActionResult GridViewPartialView()
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        }

        public ActionResult DataBinding()
        {
            Session["TreeListState"] = null;
            Session["ShowServiceColumns"] = false;
            return View(allDepartment);
        }
        [HttpPost]
        public ActionResult OnModifyDepartment(Department department)
        {
            
            var updatedDept = allDepartment.SingleOrDefault(e => e.ID == department.ID);
            if(updatedDept == null)
            {
                department.ID = allDepartment.Max(e => e.ID) + 1;
                allDepartment.Add(department);
            }
            else
            {
                updatedDept.Budget = department.Budget;
            }
            return DataBindingPartial();
        }
        [HttpPost]
        public ActionResult DataBinding(bool showServiceColumns)
        {
            Session["ShowServiceColumns"] = showServiceColumns;
            return PartialView("DataBinding", DepartmentsProvider.GetDepartments());
        }
        public ActionResult DataBindingPartial()
        {
            return PartialView("DataBinding", allDepartment);
        }

    }
}