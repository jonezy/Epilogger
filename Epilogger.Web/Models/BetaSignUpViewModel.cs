using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Epilogger.Web.Models {
    public class BetaSignUpViewModel {
        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }
        public string TotalRecordsLabel { get; set; }
        public List<HomepageActivityModel> Activity { get; set; }

        [Required]
        [DataAnnotationsExtensions.Email()]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        public DateTime regDateTime { get; set; }
        public string ipAddress { get; set; }

        public bool Submitted { get; set; }

        public BetaSignUpViewModel()
        {
        }

        public BetaSignUpViewModel(List<HomepageActivityModel> activities, int currentPageIndex, int totalRecords)
        {
            CurrentPageIndex = currentPageIndex;
            TotalRecords = totalRecords;

            Activity = activities;
        }
    }
}