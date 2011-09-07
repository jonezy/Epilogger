
using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class DashboardProfileViewModel {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public List<DashboardEventViewModel> Events { get; set; }
    }
}