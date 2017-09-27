using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApplication.data
{
    public class CategoryRepository
    {
        private string _connectionString;
        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var context = new donationApplicationDataContext())
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Category>(c => c.Applications);
                context.LoadOptions = loadOptions;
                return context.Categories.ToList();
            }
        }

        public void AddCategory(Category category)
        {
            using (var context = new donationApplicationDataContext())
            {
                context.Categories.InsertOnSubmit(category);
                context.SubmitChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new donationApplicationDataContext())
            {
                context.ExecuteCommand("UPDATE Categories SET Name = {0} where id = {1}", @category.Name, @category.Id);
                context.SubmitChanges();
            }
        }
    }
}
