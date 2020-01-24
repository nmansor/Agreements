using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
   public enum NotarizerStatusEnum : short
    {
        [Display(Name = "Under Review")]
        PendingReview = 1,

        [Display(Name = "Approved Application")]
        Approved = 2,

        [Display(Name = "Denied Application")]
        Denied = 3,

         [Display(Name = "Issued Stamp")]
        IssuedStamp = 4,

        [Display(Name = "Revoked Stamp")]
        RevokedStamp = 5,
    } 
}
