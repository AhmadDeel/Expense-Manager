using ExpenseManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseManager.service
{
    public class ExpenseService
    {
        private readonly ExpenseDBContext _db;
        public ExpenseService(ExpenseDBContext db)
        {
            _db = db;
        }
        public IEnumerable<ExpenseReport> GetAllExpenses()
        {
            try
            {
                return _db.ExpenseReport.ToList();
            }
            catch
            {
                throw;
            }
        }
        // Search Expense List By ItemName
        public IEnumerable<ExpenseReport> GetSearchResult(string searchString)
        {
            List<ExpenseReport> exp = new List<ExpenseReport>();
            try
            {
                exp = GetAllExpenses().ToList();
                return exp.Where(x => x.ItemName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
            }
            catch
            {
                throw;
            }
        }
        // Search Expense List By Categoty
        public IEnumerable<ExpenseReport> GetCategoryResult(string category)
        {
            List<ExpenseReport> exp = new List<ExpenseReport>();
            try
            {
                exp = GetAllExpenses().ToList();
                return exp.Where(x => x.Category.IndexOf(category, StringComparison.OrdinalIgnoreCase) != -1);
            }
            catch
            {
                throw;
            }
        }
        // Add new Expense record       
        public void AddExpense(ExpenseReport expense)
        {
            try
            {
                _db.ExpenseReport.Add(expense);
                _db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        // Update the records of a particluar expense
        public int UpdateExpense(ExpenseReport expense)
        {
            try
            {
                _db.Entry(expense).State = EntityState.Modified;
                _db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        // Get the data for a particular expense
        public ExpenseReport GetExpenseData(int id)
        {
            try
            {
                ExpenseReport expense = _db.ExpenseReport.Find(id);
                return expense;
            }
            catch
            {
                throw;
            }
        }
        // Delete the record of a particular expense
        public void DeleteExpense(int ItemId)
        {
            try
            {
                ExpenseReport temp = _db.ExpenseReport.Find(ItemId);
                _db.ExpenseReport.Remove(temp);
                _db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        // calculate last six months expense
        public Dictionary<string, decimal> CalculateMonthlyExpense()
        {
            //List<ExpenseReport> lstEmployee = new List<ExpenseReport>();
            Dictionary<string, decimal> dictMonthlySum = new Dictionary<string, decimal>();
            decimal foodSum = _db.ExpenseReport.Where
                (cat => cat.Category == "Food" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
                .Select(cat => cat.Amount)
                .Sum();
            decimal shoppingSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Shopping" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal travelSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Travel" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal healthSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Health" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            dictMonthlySum.Add("خوراکی", foodSum);
            dictMonthlySum.Add("خرید", shoppingSum);
            dictMonthlySum.Add("رفت و آمد", travelSum);
            dictMonthlySum.Add("سلامتی", healthSum);
            return dictMonthlySum;
        }
        // calculate last four weeks expense
        public Dictionary<string, decimal> CalculateWeeklyExpense()
        {
            List<ExpenseReport> lstEmployee = new List<ExpenseReport>();
            Dictionary<string, decimal> dictWeeklySum = new Dictionary<string, decimal>();
            decimal foodSum = _db.ExpenseReport.Where
                (cat => cat.Category == "Food" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
                .Select(cat => cat.Amount)
                .Sum();
            decimal shoppingSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Shopping" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal travelSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Travel" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal healthSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Health" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();
            dictWeeklySum.Add("خوراکی", foodSum);
            dictWeeklySum.Add("خرید", shoppingSum);
            dictWeeklySum.Add("رفت و آمد", travelSum);
            dictWeeklySum.Add("سلامتی", healthSum);
            return dictWeeklySum;
        }
        // calculate last week expense
        public Dictionary<string, decimal> CalculateLastWeekExpense()
        {
            List<ExpenseReport> lstEmployee = new List<ExpenseReport>();
            Dictionary<string, decimal> dictWeeklySum = new Dictionary<string, decimal>();
            decimal foodSum = _db.ExpenseReport.Where
                (cat => cat.Category == "Food" && (cat.ExpenseDate > DateTime.Now.AddDays(-7)))
                .Select(cat => cat.Amount)
                .Sum();
            decimal shoppingSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Shopping" && (cat.ExpenseDate > DateTime.Now.AddDays(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal travelSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Travel" && (cat.ExpenseDate > DateTime.Now.AddDays(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal healthSum = _db.ExpenseReport.Where
               (cat => cat.Category == "Health" && (cat.ExpenseDate > DateTime.Now.AddDays(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            dictWeeklySum.Add("خوراکی", foodSum);
            dictWeeklySum.Add("خرید", shoppingSum);
            dictWeeklySum.Add("رفت و آمد", travelSum);
            dictWeeklySum.Add("سلامتی", healthSum);
            return dictWeeklySum;
        }
    }
}
