using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tally.Entities;

namespace Tally.Models
{
    public class PollViewModel
    {
        public Question Question { get; set; }
        [DisplayName("Answer")]
        public Answer[] Answers { get; set; }
    }
}