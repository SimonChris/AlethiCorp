using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.ViewModels
{
    public class HakaExam
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must answer the question.")]
        [DataType(DataType.MultilineText)]
        public string AnswerOne { get; set; }

        [Required(ErrorMessage = "I'm surprised you don't know the answer to this question.")]
        [DataType(DataType.MultilineText)]
        public string AnswerTwo { get; set; }

        [Required(ErrorMessage = "Come on, this one is easy.")]
        [DataType(DataType.MultilineText)]
        public string AnswerThree { get; set; }
    }
}