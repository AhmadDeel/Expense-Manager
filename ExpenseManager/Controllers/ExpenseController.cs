using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Models;
using ExpenseManager.service;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpenseService _ES;
        public ExpenseController(ExpenseService ES)
        {
            _ES = ES;
        }

        public IActionResult Index(string searchString)
        {
            try
            {
                List<ExpenseReport> lstEmployee = new List<ExpenseReport>();
                lstEmployee = _ES.GetAllExpenses().ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    lstEmployee = _ES.GetSearchResult(searchString).ToList();
                }
                return View(lstEmployee);
            }
            catch
            {
                throw;
            }
        }
        public IActionResult Category(string cat)
        {
            if (string.IsNullOrEmpty(cat))
            {
                return View("Index");
            }
            List<ExpenseReport> catlist = new List<ExpenseReport>();
            catlist = _ES.GetCategoryResult(cat).ToList();
            return View("Index", catlist);
        }
        public IActionResult AddEditExpenses(int itemId)
        {
            ExpenseReport model = new ExpenseReport();
            if (itemId > 0)
            {
                model = _ES.GetExpenseData(itemId);
            }
            return PartialView("_expenseForm", model);
        }
        [HttpPost]
        public IActionResult Create(ExpenseReport newExpense)
        {
            if (ModelState.IsValid)
            {
                if (newExpense.ItemId > 0)
                {
                    _ES.UpdateExpense(newExpense);
                }
                else
                {
                    _ES.AddExpense(newExpense);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int ItemId)
        {
            _ES.DeleteExpense(ItemId);
            return RedirectToAction("Index");
        }
        public IActionResult ExpenseSummary()
        {
            return PartialView("_expenseReport");
        }
        public JsonResult GetMonthlyExpense()
        {
            Dictionary<string, decimal> monthlyExpense = _ES.CalculateMonthlyExpense();
            return new JsonResult(monthlyExpense);
        }
        public JsonResult GetWeeklyExpense()
        {
            Dictionary<string, decimal> weeklyExpense = _ES.CalculateWeeklyExpense();
            return new JsonResult(weeklyExpense);
        }
        public JsonResult GetLastWeekExpense()
        {
            Dictionary<string, decimal> weeklyExpense = _ES.CalculateLastWeekExpense();
            return new JsonResult(weeklyExpense);
        }
    }
}