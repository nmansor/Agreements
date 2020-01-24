using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
   public  enum DocumentStatusEnum : short
    {
        //[Display(Name = "Working Draft")]
        //Draf = 1,
        [Display(Name = "Open")]
        Open = 1,
        [Display(Name = "Completed, Done")]
        Completed = 2

    }
}

