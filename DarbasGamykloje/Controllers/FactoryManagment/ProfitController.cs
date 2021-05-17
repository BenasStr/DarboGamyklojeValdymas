using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DarbasGamykloje.Repos;
using DarbasGamykloje.Models;
using DarbasGamykloje.ViewModels.Factory;


namespace DarbasGamykloje.Controllers.FactoryManagment
{
    public class ProfitController : Controller
    {
        FactoryRepository factoryRepository = new FactoryRepository();
        ProductRepository productRepository = new ProductRepository();
        WorkerRepository workerRepository = new WorkerRepository();

        public ActionResult ProfitEvaluationView()
        {
            ProfitEvaluationView profit = new ProfitEvaluationView();
            profit.date = DateTime.Now;
            PopulateSelections(profit);

            return View("../Factory/ProfitEvaluationView", profit);
        }

        public ActionResult StartEvaluation(ProfitEvaluationView profit)
        {
            double productPrice = productRepository.GetProductValue(profit.fk_factoryId);

            int count = factoryRepository.GetCompletedAssigmentsCount(profit.fk_factoryId, profit.date);

            double salary = CalculateSalary(count);

            profit.Profit = CalculateProfit(count, productPrice, salary);

            return View("../Factory/ProfitEvaluationView", profit);
        }

        public void PopulateSelections(ProfitEvaluationView model)
        {
            var factories = factoryRepository.GetAllFactories();

            List<SelectListItem> selectFactoriesList = new List<SelectListItem>();

            foreach (var item in factories)
            {
                selectFactoriesList.Add(new SelectListItem() { Value = Convert.ToString(item.id_Factory), Text = item.id_Factory.ToString() });
            }

            model.FactoryList = selectFactoriesList;
        }

        private double CalculateSalary(int count)
        {
            return count * 10;
        }

        private double CalculateProfit(int count, double item_value, double salary)
        {
            return count * item_value - salary ;
        }
    }
}