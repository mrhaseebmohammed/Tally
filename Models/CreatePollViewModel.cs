using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tally.Models
{
    public class CreatePollViewModel
    {
        [Required]
        [StringLength(200,ErrorMessage = "Question cannot be longer than 200 characters.")]
        [DisplayName("Question")]
        public string Question { get; set; }
        public AnswerText[] Answers { get; set; }

    }
    
    public class AnswerText
    {
        [Required]
        [StringLength(200, ErrorMessage = "Answer cannot be longer than 200 characters.")]
        [DisplayName("Answer")]
        public string Value { get; set; }
    }
}