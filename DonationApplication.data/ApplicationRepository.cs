using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApplication.data
{
    public class ApplicationRepository
    {
        private string _connectionString;
        public ApplicationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddApplication(Application application)
        {
            using (var context = new donationApplicationDataContext())
            {
                context.Applications.InsertOnSubmit(application);
                context.SubmitChanges();
            }
        }

        public IEnumerable<Application> GetApplicationHistory(int userId)
        {
            using (var context = new donationApplicationDataContext())
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Application>(a => a.User);
                loadOptions.LoadWith<Application>(a => a.Category);
                context.LoadOptions = loadOptions;
                return context.Applications.Where(a => a.UserId == userId).ToList();
            }
        }

        public IEnumerable<Application> GetPendingApplications()
        {
            using (var context = new donationApplicationDataContext())
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Application>(a => a.User);
                loadOptions.LoadWith<Application>(a => a.Category);
                context.LoadOptions = loadOptions;
                return context.Applications.Where(a => a.Status == Status.Pending).ToList();
            }
        }

        public void UpdateStatus(int applicationId, bool isApproved)
        {
            using (var context = new donationApplicationDataContext())
            {
                var status = Status.Approved;
                if(!isApproved)
                {
                    status = Status.Rejected;
                }
                context.ExecuteCommand("UPDATE Applications SET Status = {0} where id = {1}", status, @applicationId);
                context.SubmitChanges();
            }
        }

    }
}
