using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ExpenseManager.Models
{
    public class ExpenseReport
    {
        [Key]
        [Display(Name = "شماره")]
        public int ItemId { get; set; }
        [Required]
        [Display(Name = "عنوان")]
        public string ItemName { get; set; }
        [Required]
        
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "مبلغ")]
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "تاریخ")]
        public DateTime ExpenseDate { get; set; }
        [Required]
        [Display(Name = "دسته بندی")]
        public string Category { get; set; }
    }
}